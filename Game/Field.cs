using System.Collections.Generic;

namespace Durak
{
    class Field : IReadOnlyField
    {
        public List<CardPair> Cards { get; } = new List<CardPair>();

        IReadOnlyCollection<CardPair> IReadOnlyField.Cards => Cards;


        public Card[] GetAllCards()
        {
            List<Card> cards = new List<Card>();

            foreach (var pair in Cards)
                cards.AddRange(pair.GetCards());

            return cards.ToArray();
        }
    }
}