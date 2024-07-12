using Phwitti.Gomori;
using System.Linq;

namespace Phwitti.GomoriShell
{
    public static class RequestExtensions
    {
        public static void ToBoardAndHand(this JsonObjectRequest _request,
            out Board _board, out Hand _hand, out int _iColumnOffset, out int _iRowOffset)
        {
            _request.GetOffset(out _iColumnOffset, out _iRowOffset);

            var boardStackList = new Stack[4,4];
            for (int column = 0; column < 4; column++)
                for (int row = 0; row < 4; row++)
                    boardStackList[column, row] = new Stack();

            if (_request.Fields != null)
            {
                foreach (var field in _request.Fields)
                {
                    foreach (var hiddenCard in field.HiddenCards)
                    {
                        boardStackList[field.Column - _iColumnOffset, field.Row - _iRowOffset].Play(hiddenCard.ToCard());
                        boardStackList[field.Column - _iColumnOffset, field.Row - _iRowOffset].TurnFaceDown();
                    }

                    if (field.TopCard.HasValue)
                    {
                        boardStackList[field.Column - _iColumnOffset, field.Row - _iRowOffset].Play(field.TopCard.Value.ToCard());
                    }
                }
            }

            _board = new Board(
                boardStackList[0,0],
                boardStackList[1,0],
                boardStackList[2,0],
                boardStackList[3,0],
                boardStackList[0,1],
                boardStackList[1,1],
                boardStackList[2,1],
                boardStackList[3,1],
                boardStackList[0,2],
                boardStackList[1,2],
                boardStackList[2,2],
                boardStackList[3,2],
                boardStackList[0,3],
                boardStackList[1,3],
                boardStackList[2,3],
                boardStackList[3,3]);

            _hand = new Hand(_request.Cards.Select(x => (PlayingCards.ICard)x.ToCard()));
        }

        public static DeckColor ToDeckColor(this JsonObjectColor _color)
        {
            switch (_color)
            {
                case JsonObjectColor.Black:
                    return DeckColor.Black;
                case JsonObjectColor.Red:
                    return DeckColor.Red;
                default:
                    throw new System.ArgumentException(nameof(_color));
            }
        }

        public static PlayingCards.Card ToCard(this JsonObjectCard _card)
        {
            return new PlayingCards.Card(
                _card.Suit.ToSuit(), _card.Rank.ToRank());
        }

        public static PlayingCards.Suit ToSuit(this JsonObjectSuit _suit)
        {
            switch (_suit)
            {
                case JsonObjectSuit.Clubs:
                    return PlayingCards.Suit.Clubs;
                case JsonObjectSuit.Diamonds:
                    return PlayingCards.Suit.Diamonds;
                case JsonObjectSuit.Hearts:
                    return PlayingCards.Suit.Hearts;
                case JsonObjectSuit.Spades:
                    return PlayingCards.Suit.Spades;
                default:
                    throw new System.ArgumentException(null, nameof(_suit));
            }
        }

        public static PlayingCards.Rank ToRank(this JsonObjectRank _rank)
        {
            switch (_rank)
            {
                case JsonObjectRank.Two:
                    return PlayingCards.Rank.Two;
                case JsonObjectRank.Three:
                    return PlayingCards.Rank.Three;
                case JsonObjectRank.Four:
                    return PlayingCards.Rank.Four;
                case JsonObjectRank.Five:
                    return PlayingCards.Rank.Five;
                case JsonObjectRank.Six:
                    return PlayingCards.Rank.Six;
                case JsonObjectRank.Seven:
                    return PlayingCards.Rank.Seven;
                case JsonObjectRank.Eight:
                    return PlayingCards.Rank.Eight;
                case JsonObjectRank.Nine:
                    return PlayingCards.Rank.Nine;
                case JsonObjectRank.Ten:
                    return PlayingCards.Rank.Ten;
                case JsonObjectRank.Jack:
                    return PlayingCards.Rank.Jack;
                case JsonObjectRank.Queen:
                    return PlayingCards.Rank.Queen;
                case JsonObjectRank.King:
                    return PlayingCards.Rank.King;
                case JsonObjectRank.Ace:
                    return PlayingCards.Rank.Ace;
                default:
                    throw new System.ArgumentException(null, nameof(_rank));
            }
        }

        //

        private static void GetOffset(this JsonObjectRequest _request, out int _iColumnOffset, out int _iRowOffset)
        {
            int? iColumnOffset = null;
            int? iRowOffset = null;

            if (_request.Fields != null)
            {
                foreach (var field in _request.Fields)
                {
                    if (iColumnOffset == null || field.Column < iColumnOffset.Value)
                        iColumnOffset = field.Column;

                    if (iRowOffset == null || field.Row < iRowOffset.Value)
                        iRowOffset = field.Row;
                }
            }

            if (iColumnOffset == null)
                iColumnOffset = 0;

            if (iRowOffset == null)
                iRowOffset = 0;

            _iColumnOffset = iColumnOffset.Value;
            _iRowOffset = iRowOffset.Value;
        }
    }
}
