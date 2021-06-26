namespace Durak
{
    class WindowContext
    {
        public IConsoleWindow NextWindow { get; private set; }

        public IConsoleWindow DefaultWindow { get; private set; }


        public void SetNextWindow(IConsoleWindow window)
        {
            NextWindow = window;
        }

        public void SetDefaultWindow(IConsoleWindow window)
        {
            DefaultWindow = window;
        }
    }
}