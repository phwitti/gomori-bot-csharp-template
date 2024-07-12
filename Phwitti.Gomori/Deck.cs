using Phwitti.PlayingCards;
using System.Linq;

namespace Phwitti.Gomori
{
    public enum DeckColor
    {
        Black,
        Red
    }

    public class Deck : PlayingCards.Deck
    {
        public DeckCount DeckCount
            => DeckCountFromInt(this.Count, Deck.FullDeckCount);

        //

        public Deck(DeckColor _color)
            : base(_color == DeckColor.Black
                  ? CardUtils.EnumerateAllBlackCards().ToList()
                  : CardUtils.EnumerateAllRedCards().ToList())
        {
        }

        //

        public static int FullDeckCount
            => CardUtils.CardCountPerColor;

        public static DeckCount DeckCountFromInt(int _iCount, int _iMaxCount)
        {
            if (_iCount <= 0)
                return DeckCount.Empty;

            if (_iCount == 1)
                return DeckCount.One;

            if (_iCount == 2)
                return DeckCount.Two;

            if (_iCount == 3)
                return DeckCount.Three;

            if (_iCount == 4)
                return DeckCount.Four;

            if (_iCount == 5)
                return DeckCount.Five;

            if (_iCount <= (int)(_iMaxCount * 0.5f))
                return DeckCount.Half;

            return DeckCount.Full;
        }
    }
}
