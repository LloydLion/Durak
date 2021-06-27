using System.Collections.Generic;
using System;

namespace DurakGame
{
    public class Player
    {
        public IReadOnlyCollection<Card> HandCards => (IReadOnlyCollection<Card>)Hand;

        internal IList<Card> Hand { get; } = new List<Card>();
        
        public string Name { get; internal set; }

        internal int Index { get; set; }

        public bool IsFinished { get; internal set; }


        public void SetName(string newName)
        {
            if(Name != null) throw new InvalidOperationException("Name already setted");
            else Name = newName;
        }
    }
}