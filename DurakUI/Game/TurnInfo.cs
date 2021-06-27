namespace Durak
{
    struct TurnInfo
    {
        public TurnInfo(Turn turn)
        {
            TurningPlayer = turn.TurningPlayer;
            UnderPlayer = turn.UnderPlayer;
            Field = turn.Field;
        }


        public IPlayerRecord TurningPlayer { get; }

        public IPlayerRecord UnderPlayer { get; }

        public IReadOnlyField Field { get; }
    }
}