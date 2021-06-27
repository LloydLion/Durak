using DurakGame;
using System;
using System.Linq;

namespace DurakUI.Windows 
{
    class StartGameWindow : IConsoleWindow
    {
        private WindowState state;
        private Game game;


        public WindowState State => state;

        public WindowContext Context { get; set; }


        public void OnClose()
        {
            
        }

        public void Show()
        {
            Console.WriteLine("====================");
            Console.WriteLine("Enter players count (<10)");
            Console.WriteLine("====================");
        }

        public void Update(ConsoleKeyInfo key)
        {
            if(game == null)
            {
                if(int.TryParse(key.KeyChar.ToString(), out int res))
                {
                    if(res <= 0) return;
                    
                    game = new Game(res);

                    Console.WriteLine("Setted players count : " + res);
                    Console.WriteLine("Press enter to countine");
                    Console.WriteLine("====================");
                    
                }
            }
            else
            {
                if(key.Key == ConsoleKey.Enter)
                {
                    for (int i = 0; i < game.Players.Count; i++)
                    {
                        Console.Write($"Enter name for player #{i + 1} - ");
                        var name = Console.ReadLine();
                        game.Players.ElementAt(i).SetName(name);
                    }

                    game.Start();
                    state.IsGoClosing = true;
                    Context.SetNextWindow(new SelectPlayerWindow(game));
                }
            }
        }
    }
}