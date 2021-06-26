namespace Durak
{
    struct RankRecord
    {
        public RankRecord(Player[] player)
        {
            Players = player;
            Place = 0;
        }


        public Player[] Players { get; }
        public int Place { get; set; }
    }
}