using System;
using System.Collections.Generic;
using System.Linq;

namespace Durak
{
    class Batch
    {
        public Mast Trump { get; }

        public bool IsClosed { get; private set; } = false;

        public TurnInfo Turn => new TurnInfo(turn);


        private PlayersCollection players;
        private bool isCardsTaken;
        private Turn turn;


        public Batch(Turn turn, Mast trump, PlayersCollection players)
        {
            this.turn = turn;
            Trump = trump;
            this.players = players;
        }


        public CardPair PlaceCard(IPlayerRecord record, Card card)
        {
            ThrowIfEnd();

            var player = players.GetPlayer(record);
            if(player.Hand.Contains(card) == false)
                throw new ArgumentException(nameof(card), "Player don't have this card");

            if(CanPlaceCard(record, card))
            {
                var ret = new CardPair() { MainCard = card };
                player.Hand.Remove(card);
                turn.Field.Cards.Add(ret);
                return ret;
            }
            else throw new InvalidOperationException("Can't place this card by game rules");
        }

        public void BeatCard(CardPair beat, Card card)
        {
            ThrowIfEnd();
            if(CanBeatCard(beat, card) == false)
                throw new ArgumentException(nameof(card), "Can't beat pair by this card");
            else
            {
                turn.UnderPlayer.Hand.Remove(card);
                beat.SuperCard = card;
            }
        }

        public bool CanBeatCard(CardPair beat, Card card)
        {
            if(turn.UnderPlayer.Hand.Contains(card) == false)
                throw new ArgumentException(nameof(card), "Player don't have this card");

            if(beat.SuperCard.HasValue) return false;

            if(beat.MainCard.Mast == Trump || card.Mast != Trump)
            {
                if(beat.MainCard.Mast != card.Mast) return false;
                if(beat.MainCard.CompareTo(card) != -1) return false;
            }

            return true;
        }

        public void TakeCards()
        {
            ThrowIfEnd();
            foreach(var pair in turn.Field.Cards)
            {
                turn.UnderPlayer.Hand.Add(pair.MainCard);

                if(pair.SuperCard.HasValue)
                    turn.UnderPlayer.Hand.Add(pair.SuperCard.Value);
            }

            isCardsTaken = true;
        }

        public void Close()
        {
            if(IsClosed) throw new InvalidOperationException("Batch has already closed");
            if(!CanClose()) throw new InvalidOperationException("Can't close the batch");
            //if(isCardsTaken != true) isCardsTaken = false;
            IsClosed = true;
        }

        public bool CanClose()
        {
            return !IsClosed && (isCardsTaken ||
                (turn.Field.Cards.Count != 0 && turn.Field.Cards.All(s => s.SuperCard.HasValue)));
        }

        public bool CanPlaceCard(IPlayerRecord placer, Card card)
        {
            var cards = turn.Field.GetAllCards();
            if(cards.Length == 0 && placer == turn.TurningPlayer) return true;
            else return cards.Any(s => s.Denomination == card.Denomination);
        }

        public BatchResult GetResult()
        {
            return new BatchResult(isCardsTaken);
        }

        private void ThrowIfEnd()
        {
            if(IsClosed) throw new InvalidOperationException("Batch is closed");
        }
    }
}