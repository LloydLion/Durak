namespace DurakUI
{
    public struct Turn
    {
        public Turn(Player turningPlayer, Player underPlayer)
        {
            Field = new Field();
            UnderPlayer = underPlayer;
            TurningPlayer = turningPlayer;
        }


        public Field Field { get; }

        public Player UnderPlayer { get; }

        public Player TurningPlayer { get; }
    }
}