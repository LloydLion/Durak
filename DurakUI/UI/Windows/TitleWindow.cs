using System;
using LC = DurakUI.LocaleDictinary;

namespace DurakUI.Windows
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
            Console.WriteLine(LC.Default["Welcome to Durak game"]);
            Console.WriteLine();
            Console.WriteLine("============================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(LC.Default["Press enter to countine"]);
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