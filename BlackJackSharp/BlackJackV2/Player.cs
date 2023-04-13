using BlackJack;

namespace BlackJackV2
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public bool IsBust { get; set; } = false;
        public int HandValue { get; set; } = 0;

        public void AddCard(Card card)
        {
            Hand ??= new();
            Hand.Add(card);

            if (card.Rank == "A")
            {
                if ((HandValue + card.Value) > 21)
                {
                    HandValue += 1;
                }
                else
                {
                    HandValue += 11;
                }
            }
            else
            {
                HandValue += card.Value;
            }
        }
    }
}
