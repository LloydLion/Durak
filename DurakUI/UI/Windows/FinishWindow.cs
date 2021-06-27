using DurakGame;
using System;
using System.Linq;

namespace DurakUI.Windows
{
    class FinishWindow : IConsoleWindow
    {        
        private WindowState state;
        private readonly Game game;


        public FinishWindow(Game game)
        {
            this.game = game;
        }


        public WindowState State => state;

        public WindowContext Context { get; set; }


        public void OnClose()
        {

        }

        public void Show()
        {
            Console.WriteLine("=== Game end ===");
            foreach (var record in game.GetResults().Records)
            {
                Console.Write(record.Place + ": ");

                var last = record.Players.Last();
                foreach (var player in record.Players)
                {
                    Console.Write(player.Name + (player == last ? "" : ", "));
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to countine");
        }

        public void Update(ConsoleKeyInfo key)
        {
            state.IsGoClosing = true;           
        }
    }
}