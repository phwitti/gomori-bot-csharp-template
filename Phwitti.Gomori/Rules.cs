using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;

namespace Phwitti.Gomori
{
    public static class Rules
    {
        public static int CardsOnHand
            => 5;

        public static bool CanPlayCardOnEmpty(Card _card)
        {
            return true;
        }

        public static bool CanPlayCardOnFaceDown(Card _card)
        {
            return true;
        }

        public static bool CanPlayCardOnOther(Card _card, Card _other)
        {
            if (_card.Rank.IsNumber)
                return _other.Rank == _card.Rank;

            if (_card.Rank.IsFace)
                return _card.Rank == _other.Rank
                    || _card.Suit == _other.Suit;

            if (_card.Rank.IsAce)
                return true;

            return false;
        }

        public static bool CanSelectTurnTargetOnActivation(Card _card)
        {
            return _card.Rank == Rank.King;
        }

        public static bool IsPlayOnEmptyActivating(Card _card)
        {
            return false;
        }

        public static bool IsPlayOnFaceDownActivating(Card _card)
        {
            return _card.Rank.IsFace;
        }

        public static bool IsPlayOnOtherActivating(Card _card, Card _other)
        {
            return _card.Rank.IsFace;
        }

        public static bool IsPlayOnEmptyStacking(Card _card)
        {
            return false;
        }

        public static bool IsPlayOnFaceDownStacking(Card _card)
        {
            return true;
        }

        public static bool IsPlayOnOtherStacking(Card _card, Card _other)
        {
            return true;
        }

        public static IEnumerable<RelativeFieldDefinition> EnumerateFlipTargets(Rank _rank)
        {
            if (_rank == Rank.Jack)
            {
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneLeft, VerticalOffset = VerticalMovement.None };
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneRight, VerticalOffset = VerticalMovement.None };
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.None, VerticalOffset = VerticalMovement.OneUp };
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.None, VerticalOffset = VerticalMovement.OneDown };
                yield break;
            }

            if (_rank == Rank.Queen)
            {
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneLeft, VerticalOffset = VerticalMovement.OneUp };
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneLeft, VerticalOffset = VerticalMovement.OneDown };
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneRight, VerticalOffset = VerticalMovement.OneUp };
                yield return new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneRight, VerticalOffset = VerticalMovement.OneDown };
                yield break;
            }
        }
    }
}
