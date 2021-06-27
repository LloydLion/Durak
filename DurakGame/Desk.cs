using System;
using System.Collections.Generic;

namespace DurakGame
{
    public class Desk
    {
        internal List<Card> Cards { get; } = new List<Card>();

        public IReadOnlyCollection<Card> AvailableCards => Cards;


        public Card? PopCard()
        {
            if(Cards.Count == 0) return null;
            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        public void Generate()
        {
            List<int> generated = new();
            var random = new Random();
            
            while(generated.Count < sorted.Length)
            {
				int newCard;
				do newCard = random.Next(sorted.Length);
                while(generated.Contains(newCard));

                generated.Add(newCard);
                Cards.Add(sorted[newCard]);
            }
        }

        private static readonly Card[] sorted;

        static Desk()
        {
            var sl = new List<Card>();
            
            for(int i = 1; i <= 13; i++)
                for (int m = 0; m < 4; m++)
                    sl.Add(new Card((Mast)m, i));

            sorted = sl.ToArray();
        }
    }
}