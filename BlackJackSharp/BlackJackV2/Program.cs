using BlackJackV2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlackJack
{
    class Program
    {



        private static readonly BlackJackGame blackJackGame = new BlackJackGame();

        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to Blackjack\n");
            Console.WriteLine("How Many players?\n(1-7):");
            var amountOfPlayers = ReadInt();

            SetPlayers(amountOfPlayers);

            blackJackGame.GameFinished = (winners, playAgain) =>
            {
                if (playAgain)
                {
                    blackJackGame.ResetGame();
                    blackJackGame.Start();
                }
            };

            blackJackGame.Start();

          

        }


     


        private static void SetPlayers(int playerCount)
        {
            var players = new List<Player>();

            for (int i = 0; i < playerCount; i++)
            {
               
                    Console.WriteLine($"What is player{i + 1} name?");
                    var playerName = Console.ReadLine();
                    players.Add(new Player
                    {
                        Name = playerName,
                        Hand = new()
                    });
                    Console.WriteLine($"\nWelcome {playerName}\n");
                
            }

            blackJackGame.SetPlayers(players);
        }

        private static bool PlayAgain()
        {
            Console.WriteLine("You have lost, do you want to play again?\nYes, No (y/n)");
            var input = Console.ReadLine() ?? string.Empty;

            if (input.ToLower() == "y" || input.ToLower() == "yes")
            {
                blackJackGame.ResetGame();
                return true;
            }
            else if (input.ToLower() == "no" || input.ToLower() == "n")
            {
                Console.WriteLine("Quitting");

                return false;
            }
            return false;

        }





      
        private static int ReadInt()
        {
            bool isInt = false;
            int value = 0;
            while(!isInt)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    isInt = int.TryParse(input, out value);
                    if (!isInt)
                    {
                        Console.WriteLine($"{input} is not a number\n");
                    }
                }
            }
            return value;
        }

    }
}
