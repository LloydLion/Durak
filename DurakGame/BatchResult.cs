namespace DurakGame
{
    public struct BatchResult
    {
        public BatchResult(bool isCardsTaken)
        {
            IsCardsTaken = isCardsTaken;
        }


        public bool IsCardsTaken { get; }
    }
}