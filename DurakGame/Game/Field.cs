using System.Collections.Generic;

namespace DurakUI
{
    public class Field
    {
        internal List<CardPair> Cards { get; } = new List<CardPair>();

        public IReadOnlyCollection<CardPair> CardPairs => Cards;


        public Card[] GetAllCards()
        {
            List<Card> cards = new();

            foreach (var pair in Cards)
                cards.AddRange(pair.GetCards());

            return cards.ToArray();
        }
    }
}