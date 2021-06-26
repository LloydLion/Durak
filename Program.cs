using System;
using System.Collections.Generic;
using System.Linq;

namespace Durak
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new UIEngine(new TitleWindow());

            while(ui.Update()) { }


            // var game = new Game(4);

            // game.Players[0].Name = "1";
            // game.Players[1].Name = "2";
            // game.Players[2].Name = "3";
            // game.Players[3].Name = "4";

            // game.Start();

            // while(!game.IsEnded)
            // {
            //     foreach (var player in game.Players)
            //     {
            //         Console.Write(player.Name + ": ");
            //         foreach (var card in player.Hand)
            //             Console.Write(card.ToString() + "\t");
            //         Console.WriteLine();
            //     }

            //     Console.WriteLine(new string('-', 30));
            //     Console.WriteLine("Trump Mast: " + game.TrumpMast);
            //     Console.WriteLine("Left: " + game.Desk.Cards.Count);


            //     var batch = game.DoTurn();
            //     var turnPlayer = batch.Turn.TurningPlayer;
            //     Console.WriteLine("Turning Player: " + turnPlayer.Name);

            //     batch.PlaceCard(turnPlayer, turnPlayer.Hand[0]);
                
            //     foreach (var card in turnPlayer.Hand.ToList())
            //     {
            //         if(batch.CanPlaceCard(turnPlayer, card)) batch.PlaceCard(turnPlayer, card);
            //     }

            //     foreach (var pair in batch.Turn.Field.Cards)
            //         foreach(var card in batch.Turn.UnderPlayer.Hand)
            //             if(batch.CanBeatCard(pair, card))
            //             {
            //                 batch.BeatCard(pair, card);
            //                 break;
            //             }

            //     if(batch.Turn.Field.Cards.Any(s => !s.SuperCard.HasValue))
            //     {
            //         batch.TakeCards();
            //     }


            //     foreach(var item in batch.Turn.Field.Cards)
            //     {
            //         var main = item.MainCard;
            //         Console.Write($"{main}");
            //         if(item.SuperCard.HasValue) Console.Write($" <- {item.SuperCard}");
            //         else Console.Write(" =/");
            //         Console.WriteLine();
            //     }

            //     Console.WriteLine(new string('-', 30));

            //     foreach (var player in game.Players)
            //     {
            //         Console.Write(player.Name + ": ");
            //         foreach (var card in player.Hand)
            //             Console.Write("" + card.Mast.ToString()[0] + card.Denomination + "\t");
            //         Console.WriteLine();
            //     }

            //     batch.Close();
            //     game.EndTurn(batch);

            //     Console.WriteLine(new string('=', 30));
            // }

            // Console.WriteLine("Game End");
            // foreach (var record in game.GetResults().Records)
            // {
            //     Console.Write(record.Place + ": ");
            //     foreach (var player in record.Players)
            //     {
            //         Console.Write(player.Name + "|");
            //     }

            //     Console.WriteLine();
            // }
        }
    }
}
