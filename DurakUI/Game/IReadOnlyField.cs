using System.Collections.Generic;

namespace Durak
{
    interface IReadOnlyField
    {
        IReadOnlyCollection<CardPair> Cards { get; }
    }
}