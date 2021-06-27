using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakUI
{
    public class Batch
    {
        public Mast Trump { get; }

        public bool IsClosed { get; private set; } = false;

        public Turn Turn => turn;


        private bool isCardsTaken;
        private readonly Turn turn;


        internal Batch(Turn turn, Mast trump, PlayersCollection _)
        {
            this.turn = turn;
            Trump = trump;
        }


        public CardPair PlaceCard(Player player, Card card)
        {
            ThrowIfEnd();

            if(player.Hand.Contains(card) == false)
                throw new ArgumentException("Player don't have this card", nameof(card));

            if(CanPlaceCard(player, card))
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
                throw new ArgumentException("Can't beat pair by this card", nameof(card));
            else
            {
                turn.UnderPlayer.Hand.Remove(card);
                beat.SuperCard = card;
            }
        }

        public bool CanBeatCard(CardPair beat, Card card)
        {
            if(turn.UnderPlayer.Hand.Contains(card) == false)
                throw new ArgumentException("Player don't have this card", nameof(card));

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

        public bool CanPlaceCard(Player placer, Card card)
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