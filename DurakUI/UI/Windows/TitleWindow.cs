using System;

namespace Durak
{
    class TitleWindow : IConsoleWindow
    {
        private WindowState state;


        public WindowState State => state;

        public WindowContext Context { get; set; }


        public void OnClose()
        {

        }

        public void Show()
        {
            Console.WriteLine("============================");
            Console.WriteLine();
            Console.WriteLine("Welcome to Durak game");
            Console.WriteLine();
            Console.WriteLine("============================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to start game");
        }

        public void Update(ConsoleKeyInfo key)
        {
            if(key.Key == ConsoleKey.Enter)
            {
                state.IsGoClosing = true;
                Context.SetNextWindow(new StartGameWindow());
            }
        }
    }
}