using Phwitti.Gomori;
using Phwitti.PlayingCards;

namespace Phwitti.GomoriShell
{
    public static class ResponseExtensions
    {
        public static JsonObjectCard ToFirstTurnResponse(this JsonObjectResponse _response)
        {
            return _response.Card;
        }

        public static JsonObjectResponse ToShellAction(this Action _action, int _iColumnOffset, int _iRowOffset)
        {
            return new JsonObjectResponse() {
                Card = _action.PlayAction.Card.ToJsonObjectCard(),
                TargetFieldForKingAbility = _action.PlayAction.FlipTarget.HasValue
                    ? [ (int)_action.PlayAction.FlipTarget.Value.Row + _iRowOffset, (int)_action.PlayAction.FlipTarget.Value.Column + _iColumnOffset ]
                    : null,
                Column = (int)_action.PlayAction.Target.Column + _iColumnOffset,
                Row = (int)_action.PlayAction.Target.Row + _iRowOffset,
            };
        }

        public static JsonObjectCard ToJsonObjectCard(this Card _card)
        {
            return new JsonObjectCard
            {
                Rank = _card.Rank.ToJsonObjectRank(),
                Suit = _card.Suit.ToJsonObjectSuit()
            };
        }

        public static JsonObjectSuit ToJsonObjectSuit(this Suit _suit)
        {
            switch (_suit.Type)
            {
                case Suit.SuitType.Clubs:
                    return JsonObjectSuit.Clubs;
                case Suit.SuitType.Diamonds:
                    return JsonObjectSuit.Diamonds;
                case Suit.SuitType.Hearts:
                    return JsonObjectSuit.Hearts;
                case Suit.SuitType.Spades:
                    return JsonObjectSuit.Spades;
                default:
                    throw new System.ArgumentException(null, nameof(_suit));
            }
        }

        public static JsonObjectRank ToJsonObjectRank(this Rank _rank)
        {
            switch (_rank.Type)
            {
                case Rank.RankType.Two:
                    return JsonObjectRank.Two;
                case Rank.RankType.Three:
                    return JsonObjectRank.Three;
                case Rank.RankType.Four:
                    return JsonObjectRank.Four;
                case Rank.RankType.Five:
                    return JsonObjectRank.Five;
                case Rank.RankType.Six:
                    return JsonObjectRank.Six;
                case Rank.RankType.Seven:
                    return JsonObjectRank.Seven;
                case Rank.RankType.Eight:
                    return JsonObjectRank.Eight;
                case Rank.RankType.Nine:
                    return JsonObjectRank.Nine;
                case Rank.RankType.Ten:
                    return JsonObjectRank.Ten;
                case Rank.RankType.Jack:
                    return JsonObjectRank.Jack;
                case Rank.RankType.Queen:
                    return JsonObjectRank.Queen;
                case Rank.RankType.King:
                    return JsonObjectRank.King;
                case Rank.RankType.Ace:
                    return JsonObjectRank.Ace;
                default:
                    throw new System.ArgumentException(null, nameof(_rank));
            }
        }
    }
}
