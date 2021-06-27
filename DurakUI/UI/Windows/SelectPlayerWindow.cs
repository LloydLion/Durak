using DurakGame;
using System;
using System.Linq;

namespace DurakUI.Windows
{
    class SelectPlayerWindow : IConsoleWindow
    {
        private WindowState state;
        private readonly Game game;
        private Batch batch;


        public SelectPlayerWindow(Game game)
        {
            this.game = game;
            batch = game.DoTurn();
        }


        public WindowState State => state;

        public WindowContext Context { get; set; }


        public void OnClose()
        {

        }

        public void Show()
        {
            Context.SetDefaultWindow(this);
            if(batch.IsClosed)
            {
                game.EndTurn(batch);
                if(game.IsEnded)
                {
                    state.IsGoClosing = true;
                    Context.SetDefaultWindow(null);
                    Context.SetNextWindow(new FinishWindow(game));
                    return;
                }
                else batch = game.DoTurn();
            }

            Console.WriteLine("===== Durak game =====");
            Console.WriteLine("Turn: " + game.CurrentTurn.TurningPlayer.Name
                + " -> " + game.CurrentTurn.UnderPlayer.Name);
            Console.WriteLine("Cards left: " + game.Desk.AvailableCards.Count);
            Console.WriteLine("Trump mast: " + game.TrumpMast);
            Console.WriteLine();
            Console.WriteLine("--- Select Player ---");

            for (int i = 0; i < game.Players.Count; i++)
            {
                var player = game.Players.ElementAt(i);

                if(player.IsFinished) continue;

                Console.Write("" + (i + 1) + ": " + player.Name);

                if(game.CurrentTurn.TurningPlayer == player) Console.Write(" [T]");
                if(game.CurrentTurn.UnderPlayer == player) Console.Write(" [U]");

                Console.WriteLine();
            }
        }

        public void Update(ConsoleKeyInfo key)
        {

            if(int.TryParse(key.KeyChar.ToString(), out int res))
            {
                if(res <= 0 || res > game.Players.Count) return;
                
                state.IsGoClosing = true;

                var player = game.Players.ElementAt(res - 1);
                if(player == game.CurrentTurn.UnderPlayer) 
                    Context.SetNextWindow(new UnderTurnWindow(game, batch));
                else Context.SetNextWindow(new TurnWindow(game, batch, player));
            }

            if(key.Key == ConsoleKey.Spacebar && batch.CanClose()) batch.Close();

            if(batch.IsClosed)
            {
                game.EndTurn(batch);
                if(game.IsEnded)
                {
                    state.IsGoClosing = true;
                    Context.SetDefaultWindow(null);
                    Context.SetNextWindow(new FinishWindow(game));
                    return;
                }
                else batch = game.DoTurn();
            }
        }
    }
}