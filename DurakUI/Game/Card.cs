using System;

namespace Durak
{
    struct Card : IComparable<Card>
    {
        public Mast Mast { get; set; }

        public int Denomination { get; set; }


        public Card(Mast mast, int denomination)
        {
            Denomination = denomination;
            Mast = mast;
        }


        public int CompareTo(Card other) => Denomination.CompareTo(other.Denomination);

        public static bool operator==(Card card1, Card card2) =>
            card1.Denomination == card2.Denomination && card1.Mast == card2.Mast;

        public static bool operator!=(Card card1, Card card2) => !(card1 == card2);

        public override bool Equals(object obj) => obj is Card && (Card)obj == this;

        public override int GetHashCode() => HashCode.Combine(Denomination, Mast);

        public override string ToString() => Mast.ToString()[0] + Denomination.ToString();
    }
}