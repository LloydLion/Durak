using System;
using System.Threading;
using System.Linq;
using DurakGame;
using LC = DurakUI.LocaleDictinary;

namespace DurakUI.Windows
{
    class UnderTurnWindow : IConsoleWindow
    { 
        private WindowState state;
        private readonly Game game;
        private readonly Batch batch;
        private readonly Player player;


        public UnderTurnWindow(Game game, Batch batch)
        {
            this.game = game;
            this.batch = batch;
            this.player = batch.Turn.UnderPlayer;
        }


        public WindowState State => state;

        public WindowContext Context { get; set; }



        public void OnClose()
        {

        }

        public void Show()
        {
            Console.WriteLine(LC.Default["Durak game title"]);
            Console.WriteLine(LC.Default["Press Esc to exit"]);
            Console.WriteLine(LC.Default["Select card and beat"]);
            Console.WriteLine();
            Console.WriteLine(LC.Default["Turn"] + ": " + game.CurrentTurn.TurningPlayer.Name
                 + " -> " + game.CurrentTurn.UnderPlayer.Name);
            Console.WriteLine(LC.Default["Cards left"] + ": " + game.Desk.AvailableCards.Count);
            Console.WriteLine(LC.Default["Trump mast"] + ": " + game.TrumpMast);
            Console.WriteLine(LC.Default["Selected player"] + ": " + player.Name);
            Console.WriteLine();
            Console.WriteLine(LC.Default["Field header"]);

            var j = 0;
            foreach(var item in batch.Turn.Field.CardPairs)
            {
                var main = item.MainCard;
                Console.Write($"[{j + 1}]: " + main);
                if(item.SuperCard.HasValue) Console.Write($" <- {item.SuperCard}");
                else Console.Write(" <- ***");
                Console.WriteLine();
                j++;
            }

            Console.WriteLine();
            Console.WriteLine(LC.Default["Cards header"]);

            j = 0;
            foreach(var card in player.HandCards)
            {
                Console.WriteLine($"[{j + 1}]: {card}");
                j++;
            }
        }

        public void Update(ConsoleKeyInfo key)
        {
            if (int.TryParse(key.KeyChar.ToString(), out int res))
            {
                if (res <= 0 || res > player.HandCards.Count) return;

                var card = player.HandCards.ElementAt(res - 1);

                Console.WriteLine();
                Console.WriteLine("=========");
                Console.WriteLine(LC.Default["Card selected"] + $": [{res}] {card}");
                Console.WriteLine(LC.Default["Select card to beat"]);
                Console.WriteLine("=========");

                var yek = Console.ReadKey(true);
                if(int.TryParse(yek.KeyChar.ToString(), out int ser))
                {
                    if(ser <= 0 || ser > batch.Turn.Field.CardPairs.Count) return;
                    
                    var pair = batch.Turn.Field.CardPairs.ElementAt(ser - 1);

                    if(batch.CanBeatCard(pair, card)) batch.BeatCard(pair, card);
                    else
                    {
                        Console.WriteLine(LC.Default["Invalid card"]);
                        Thread.Sleep(1000);
                    }

                    Console.Clear();
                    Show();
                }

                return;
            }

            if(key.Key == ConsoleKey.Escape)
            {
                state.IsGoClosing = true;
                return;
            }

            if(key.Key == ConsoleKey.Delete)
            {
                batch.TakeCards();
                batch.Close();
                state.IsGoClosing = true;
                return;
            }
        }
    }
}