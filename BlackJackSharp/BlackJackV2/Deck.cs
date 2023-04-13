using System;
using System.Collections.Generic;

namespace BlackJack
{
    public class Deck
    {
        public Queue<Card> Cards;

        

        public Deck()
        {
            Cards = new Queue<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int i = 1; i < 14; i++)
                {
                    Cards.Enqueue(new Card() { Rank = ToName(i), Suit = suit , Value = Math.Min(i, 10)});
                }

            }
            var test = Cards;


        }

        public void Shuffle()
        {
            var rnd = new Random();

            var list = Cards.ToArray();
            int n = list.Count();
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Card value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            Cards = new();

            foreach (var card in list)
            {
                Cards.Enqueue(card);
            }
            
        }

        public Card Draw()
        {
            return Cards.Dequeue();
        }


        private static string ToName(int value)
        {
            switch (value)
            {
                case 1:
                    return "A";
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";

                default:
                    return value.ToString();
            }
        }


    }
}
