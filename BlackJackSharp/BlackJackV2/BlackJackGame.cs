using BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2
{
    public class BlackJackGame
    {

        private Deck Deck = new();
        public List<Player> Players { get; private set; } = new();
        private int Rounds = 0;

        public Action<List<Player>, bool> GameFinished { get; set; } 



        public void Start()
        {

            Deck.Shuffle();

            if(Players.Count == 0)
            {
                Console.WriteLine("Mangler spillere!");
                return;
            }




             FirstDeal();


            for (int i = 0; i < Players.Count; i++)
            {
               Players[i] = PlayerTurn(Players[i]);
            }

            GatherResults();

        }


        private void FirstDeal()
        {
            Console.WriteLine("Starting first deal!");
            //handle first round

            int dealRound = 1;

            while (dealRound <= 2)
            {
                dealRound++;


                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].Name != "Dealer")
                    {
                        var card = Deck.Draw();
                        Console.WriteLine($"{Players[i].Name} received {card.Suit} {card.Rank}");
                        Players[i].AddCard(card);
                    }

                }
                Console.WriteLine();

                var dealer = Players[Players.IndexOf(Players.FirstOrDefault(s => s.Name == "Dealer"))];
                var dealerCard = Deck.Draw();
                dealer.AddCard(dealerCard);

                Console.WriteLine($"{dealer.Name} received {dealerCard.Suit} {dealerCard.Rank}\n");
            }

            foreach (var player in Players)
            {
                Console.WriteLine($"{player.Name} hand value: {player.HandValue}");
                if (player.HandValue == 21)
                {
                    Console.WriteLine($"{player.Name} got blackjack!");
                }
            }

            Console.WriteLine("\n");
        }


        private Player PlayerTurn(Player player)
        {
            string playerResponse = string.Empty;
            bool playerPlaying = true;

            if (player.HandValue > 21)
            {
                return player;
            }

           

            Console.WriteLine($"{player.Name} turn!");

            while (playerPlaying)
            {
                Console.WriteLine($"Current hand value is {player.HandValue}");
                Console.WriteLine("Stand, Hit");

                if (player.Name == "Dealer")
                {

                    if (player.Name == "Dealer")
                    {
                        int dealerTotal = player.HandValue;
                        if (dealerTotal < 17)
                        {
                            playerResponse = "hit";
                        }
                        else
                        {
                            playerResponse = "stand";
                        }
                    }

                    Console.WriteLine(playerResponse);
                }
                else
                {
                    playerResponse = Console.ReadLine();
                }


                if (playerResponse.ToLower() == "hit" || playerResponse.ToLower() == "h")
                {
                    var card = Deck.Cards.Dequeue();
                    player.AddCard(card);

                    Console.WriteLine($"Hit with {card.Suit} {card.Rank}. Total is {player.HandValue}");

                    if (player.HandValue > 21)
                    {
                        Console.WriteLine($"{player.Name} is bust!\n");
                        player.IsBust = true;
                        playerPlaying = false;

                    }
                    else if (player.HandValue == 21 && player.Hand.Count == 2)
                    {
                        Console.WriteLine($"{player.Name} got Blackjack!");
                        playerPlaying = false;
                    }

                }
                else if (playerResponse.ToLower() == "stand" || playerResponse.ToLower() == "s")
                {
                    playerPlaying = false;
                    Console.WriteLine($"{player.Name} stands!\n");
                }
            }

            return player;
        }


        private void GatherResults()
        {
            var winners = new List<Player>();

            var dealer = Players.FirstOrDefault(s => s.Name == "Dealer");


            foreach (var player in Players)
            {
                if (player.Name == dealer.Name) continue;
                if (player.IsBust) continue;


                if (dealer.IsBust)
                {
                    winners.Add(player); 
                    continue;
                }

                if(dealer.HandValue < player.HandValue) 
                {
                    winners.Add(player);
                }
                else if(dealer.HandValue == player.HandValue)
                {
                    Console.WriteLine($"It's a standoff between {player.Name} and {dealer.Name}");
                    Console.WriteLine("No one wins!");
                }
            }

            if (winners.Count == 0)
            {
                Console.WriteLine("House wins!");
            }
            else if(winners.Count > 0) 
            {
                Console.WriteLine("Winners");
            }

            foreach (var player in winners)
            {
                Console.WriteLine($"{player.Name} is a winner!");
            }

            Console.WriteLine("Game is over would you like to play another round?\n(yes,no)");
            var input = Console.ReadLine();
            bool playAgain = false;

            if(input.ToLower() == "yes" ||  input.ToLower() == "y") 
            {
                playAgain = true;
            }

            GameFinished?.Invoke(winners, playAgain);

        }

        public void SetPlayers(List<Player> players)
        {
            Players = players;

            Players.Add(new Player
            {
                Name = "Dealer",
                Hand = new()
            });

        }

        public void ResetGame()
        {
            Deck = new Deck();
            Deck.Shuffle();

            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Hand.Clear();
                Players[i].HandValue = 0;
            }
        }

    }
}
