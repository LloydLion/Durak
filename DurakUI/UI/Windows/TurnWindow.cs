using DurakGame;
using System;
using System.Linq;

namespace DurakUI.Windows
{
    class TurnWindow : IConsoleWindow
    { 
        private WindowState state;
        private readonly Game game;
        private readonly Batch batch;
        private readonly Player player;


        public TurnWindow(Game game, Batch batch, Player player)
        {
            this.game = game;
            this.batch = batch;
            this.player = player;
        }


        public WindowState State => state;

        public WindowContext Context { get; set; }



        public void OnClose()
        {

        }

        public void Show()
        {
            Console.WriteLine("===== Durak game =====");
            Console.WriteLine("Press Esc to exit");
            Console.WriteLine("Select card and turn");
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.CurrentTurn.TurningPlayer.Name
                + " -> " + game.CurrentTurn.UnderPlayer.Name);
            Console.WriteLine("Cards left: " + game.Desk.AvailableCards.Count);
            Console.WriteLine("Trump mast: " + game.TrumpMast);
            Console.WriteLine("Selected player: " + player.Name);
            Console.WriteLine();
            Console.WriteLine("--- Field ---");

            foreach(var item in batch.Turn.Field.CardPairs)
            {
                var main = item.MainCard;
                Console.Write(main);
                if(item.SuperCard.HasValue) Console.Write($" <- {item.SuperCard}");
                else Console.Write(" <- ***");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("--- Cards ---");
            
            int j = 0;
            foreach(var card in player.HandCards)
            {
                var canPlace = batch.CanPlaceCard(player, card) ? "***" : "";
                Console.WriteLine($"[{j + 1}]: {card} {canPlace}");
                j++;
            }
        }

        public void Update(ConsoleKeyInfo key)
        {
            if(int.TryParse(key.KeyChar.ToString(), out int res))
            {
                if(res <= 0 || res > player.HandCards.Count) return;
                
                var card = player.HandCards.ElementAt(res - 1);
                if(batch.CanPlaceCard(player, card))
                {
                    batch.PlaceCard(player, card);
                    Console.Clear();
                    Show();
                }

                return;
            }

            if(key.Key == ConsoleKey.Escape)
            {
                state.IsGoClosing = true;
                return;
            }
        }
    }
}