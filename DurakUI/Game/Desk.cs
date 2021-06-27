using System;
using System.Collections.Generic;

namespace Durak
{
    class Desk
    {
        public List<Card> Cards { get; } = new List<Card>();


        public Card? PopCard()
        {
            if(Cards.Count == 0) return null;
            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }

        public void Generate()
        {
            List<int> generated = new List<int>();
            var random = new Random();
            
            while(generated.Count < sorted.Length)
            {
                int newCard = 0;
                do newCard = random.Next(sorted.Length);
                while(generated.Contains(newCard));

                generated.Add(newCard);
                Cards.Add(sorted[newCard]);
            }
        }

        private static Card[] sorted = new Card[]
        {

        };

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