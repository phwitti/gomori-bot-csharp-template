using System.Collections.Generic;

namespace Phwitti.PlayingCards
{
    public static class CardUtils
    {
        public static int CardCountPerSuit
            => 13;

        public static int CardCountPerColor
            => CardCountPerSuit * 2;

        //

        public static IEnumerable<ICard> EnumearteAllCards(int _iJokerCount)
        {
            foreach (var card in EnumerateAllCards())
                yield return card;

            for (int i = 0; i < _iJokerCount; i++)
            {
                yield return new Joker();
            }
        }

        public static IEnumerable<Card> EnumerateAllCards()
        {
            foreach (var card in EnumerateAllBlackCards())
                yield return card;

            foreach (var card in EnumerateAllRedCards())
                yield return card;
        }

        public static IEnumerable<Card> EnumerateAllBlackCards()
        {
            foreach (var card in EnumerateAllCardsOfSuit(Suit.Clubs))
                yield return card;

            foreach (var card in EnumerateAllCardsOfSuit(Suit.Spades))
                yield return card;
        }

        public static IEnumerable<Card> EnumerateAllRedCards()
        {
            foreach (var card in EnumerateAllCardsOfSuit(Suit.Diamonds))
                yield return card;

            foreach (var card in EnumerateAllCardsOfSuit(Suit.Hearts))
                yield return card;
        }

        public static IEnumerable<Card> EnumerateAllCardsOfSuit(Suit _suit)
        {
            yield return new Card(_suit, Rank.Ace);
            yield return new Card(_suit, Rank.Two);
            yield return new Card(_suit, Rank.Three);
            yield return new Card(_suit, Rank.Four);
            yield return new Card(_suit, Rank.Five);
            yield return new Card(_suit, Rank.Six);
            yield return new Card(_suit, Rank.Seven);
            yield return new Card(_suit, Rank.Eight);
            yield return new Card(_suit, Rank.Nine);
            yield return new Card(_suit, Rank.Ten);
            yield return new Card(_suit, Rank.Jack);
            yield return new Card(_suit, Rank.Queen);
            yield return new Card(_suit, Rank.King);
        }
    }
}
