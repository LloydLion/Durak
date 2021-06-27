using System;
using System.Threading;
using System.Linq;

namespace Durak
{
    class UnderTurnWindow : IConsoleWindow
    { 
        private WindowState state;
        private Game game;
        private Batch batch;
        private IPlayerRecord player;


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
            Console.WriteLine("===== Durak game =====");
            Console.WriteLine("Press Esc to exit");
            Console.WriteLine("Select card and beat");
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.CurrentTurn.TurningPlayer.Name
                + " -> " + game.CurrentTurn.UnderPlayer.Name);
            Console.WriteLine("Cards left: " + game.Desk.Cards.Count);
            Console.WriteLine("Trump mast: " + game.TrumpMast);
            Console.WriteLine("Selected player: " + player.Name);
            Console.WriteLine();
            Console.WriteLine("--- Field ---");

            var j = 0;
            foreach(var item in batch.Turn.Field.Cards)
            {
                var main = item.MainCard;
                Console.Write($"[{j + 1}]: " + main);
                if(item.SuperCard.HasValue) Console.Write($" <- {item.SuperCard}");
                else Console.Write(" <- ***");
                Console.WriteLine();
                j++;
            }

            Console.WriteLine();
            Console.WriteLine("--- Cards ---");

            j = 0;
            foreach(var card in player.Hand)
            {
                Console.WriteLine($"[{j + 1}]: {card.ToString()}");
                j++;
            }
        }

        public void Update(ConsoleKeyInfo key)
        {
            if(int.TryParse(key.KeyChar.ToString(), out int res))
            {
                if(res <= 0 || res > player.Hand.Count) return;
                
                var card = player.Hand.ElementAt(res - 1);

                Console.WriteLine();
                Console.WriteLine("=========");
                Console.WriteLine($"Card selected: [{res}] {card}");
                Console.WriteLine($"Select card to beat");
                Console.WriteLine("=========");

                var yek = Console.ReadKey(true);
                if(int.TryParse(yek.KeyChar.ToString(), out int ser))
                {
                    if(ser <= 0 || ser > batch.Turn.Field.Cards.Count) return;
                    
                    var pair = batch.Turn.Field.Cards.ElementAt(ser - 1);

                    if(batch.CanBeatCard(pair, card)) batch.BeatCard(pair, card);
                    else
                    {
                        Console.WriteLine("Invalid card");
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