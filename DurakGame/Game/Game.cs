using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakUI
{
    public class Game
    {
        public IReadOnlyCollection<Player> Players =>
            players.ToArray();

        public Turn CurrentTurn => currentTurn;

        public Desk Desk => desk;

        public Mast TrumpMast => trumpMast;

        public bool IsEnded => players.Where(s => !s.IsFinished).Count() <= 1;


        private readonly PlayersCollection players;
        private Mast trumpMast;
        private Turn currentTurn = new(null, null);
        private Desk desk = null;
        private readonly RankTable rankTable = new();
        private Batch lastBatch;


        public Game(int playersCount)
        {
            players = new PlayersCollection(playersCount);
            for (int i = 0; i < playersCount; i++) players[i] = new Player();
        }


        public void Start()
        {
            desk = new Desk();
            desk.Generate();

            foreach (var player in players) GiveCards(player);

            var random = new Random();
            trumpMast = (Mast)random.Next(4);
        }

        public Player GetFirstTurning()
        {
            if(currentTurn.TurningPlayer != null)
                throw new InvalidOperationException("Can't get first player in late game");

            var sel = players.Select((s) => 
            new
            {
                Player = s,
                MinTrump = s.Hand.Where(s => s.Mast == trumpMast)
                    .Min(s => s.Denomination, new Card(trumpMast, 0))
            });

            var min = sel.Min(s => s.MinTrump.Denomination);
            
            return min.Player;
        }

        public Batch DoTurn()
        {
            if(currentTurn.TurningPlayer == null)
            {
                var first = GetFirstTurning();
                currentTurn = new Turn(first, players[first.Index + 1]);
            }
            else
            {
                if(lastBatch.GetResult().IsCardsTaken) MoveTurnSelection();
                MoveTurnSelection();
            }

            return lastBatch = new Batch(currentTurn, TrumpMast, players);
        }

        public void EndTurn(Batch batch)
        {
            if(batch.IsClosed == false) throw new ArgumentException("Batch isn't closed", nameof(batch));
            GiveCards(currentTurn.TurningPlayer);

            foreach (var player in players.Except(new Player[] 
            { currentTurn.UnderPlayer, currentTurn.TurningPlayer }))
                GiveCards(player);

            GiveCards(currentTurn.UnderPlayer);

            KickFinishedPlayers();

            if(IsEnded)
            {
                var nf = players.Where(s => !s.IsFinished).ToArray();
                foreach (var player in nf)
                    player.IsFinished = true;
                rankTable.AddRecord(new RankRecord(nf));
            }
        }

        public RankTable GetResults()
        {
            return rankTable;
        }

        private void GiveCards(Player player, out bool hasSkips)
        {
            hasSkips = false;
            var startCount = player.Hand.Count;
            for (int i = 0; i < 6 - startCount; i++)
            {
                var card = desk.PopCard();
                if(card.HasValue) player.Hand.Add(card.Value);
                else hasSkips = true;
            }
        }

        private Player[] KickFinishedPlayers()
        {
            List<Player> tmp = new();
            foreach (var player in players)
                if(player.Hand.Count == 0 && !player.IsFinished)
                {
                    tmp.Add(player);
                    player.IsFinished = true;
                }

            if(tmp.Any())
                rankTable.AddRecord(new RankRecord(tmp.ToArray()));

            return tmp.ToArray();
        }

        private void MoveTurnSelection()
        {
            ThrowIfEnded();

            var tp = currentTurn.UnderPlayer;
            while(tp.IsFinished) tp = players[tp.Index +1];

            var up = players[tp.Index + 1];
            while(up.IsFinished) up = players[up.Index +1];

            currentTurn = new Turn(tp, up);
        }

        private void GiveCards(Player player) => GiveCards(player, out _);

        private void ThrowIfEnded()
        {
            if(IsEnded) throw new InvalidOperationException("Game is ended");
        }
    }
}