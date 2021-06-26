using System.Collections.Generic;
using System;

namespace Durak
{
    class Player : IPlayerRecord
    {
        IReadOnlyCollection<Card> IPlayerRecord.Hand => (IReadOnlyCollection<Card>)Hand;

        public IList<Card> Hand { get; } = new List<Card>();
        
        public string Name { get; set; }

        public int Index { get; set; }

        public bool IsFinished { get; set; }


        void IPlayerRecord.SetName(string newName)
        {
            if(Name != null) throw new InvalidOperationException("Name already setted");
            else Name = newName;
        }
    }
}