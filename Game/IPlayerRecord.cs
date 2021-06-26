using System.Collections.Generic;

namespace Durak
{
    interface IPlayerRecord
    {
        string Name { get; }

        IReadOnlyCollection<Card> Hand { get; }

        bool IsFinished { get; }


        void SetName(string newName);
    }
}