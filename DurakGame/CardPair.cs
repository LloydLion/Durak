namespace DurakGame
{
    public class CardPair
    {
        public Card MainCard { get; internal set; }

        public Card? SuperCard { get; internal set; }


        public bool HasCard(Card card) =>
            MainCard == card || (SuperCard.HasValue && SuperCard.Value == card);

        public Card[] GetCards()
        {
            if(SuperCard.HasValue) return new Card[] { MainCard, SuperCard.Value };
            else return new Card[] { MainCard };
        }
    }
}