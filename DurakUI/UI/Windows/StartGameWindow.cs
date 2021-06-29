using DurakGame;
using System;
using System.Linq;
using LC = DurakUI.LocaleDictinary;

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
            Console.WriteLine(LC.Default["Enter players count title"]);
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

                    Console.WriteLine(LC.Default["Setted players count"] + " : " + res);
                    Console.WriteLine(LC.Default["Press enter to countine"]);
                    Console.WriteLine("====================");
                    
                }
            }
            else
            {
                if(key.Key == ConsoleKey.Enter)
                {
                    for (int i = 0; i < game.Players.Count; i++)
                    {
                        Console.Write(LC.Default["Enter name for player"] + $" #{i + 1} - ");
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