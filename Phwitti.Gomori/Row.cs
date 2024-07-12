using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Phwitti.Gomori
{
    public class Row
    {
        private Stack[] m_arStacks;

        //

        public bool IsEmpty
            => m_arStacks.All(x => x.IsEmpty);

        public bool IsComplete
        {
            get
            {
                foreach (var stack in m_arStacks)
                {
                    if (stack.IsFaceDown || !stack.TryGetFaceUpTopCard(out Card topCard))
                        return false;

                    if (!m_arStacks[0].TryGetFaceUpTopCard(out Card firstTopCard)
                        || topCard.Suit != firstTopCard.Suit)
                        return false;
                }

                return true;
            }
        }

        //

        public Row(Stack _stack0, Stack _stack1, Stack _stack2, Stack _stack3)
        {
            m_arStacks = new Stack[4];
            m_arStacks[0] = _stack0;
            m_arStacks[1] = _stack1;
            m_arStacks[2] = _stack2;
            m_arStacks[3] = _stack3;
        }

        public bool CanMove(HorizontalMovement _eHorizontalMovement)
        {
            switch (_eHorizontalMovement)
            {
                case HorizontalMovement.ThreeLeft:
                    return m_arStacks[0].IsEmpty
                        && m_arStacks[1].IsEmpty
                        && m_arStacks[2].IsEmpty;
                case HorizontalMovement.TwoLeft:
                    return m_arStacks[0].IsEmpty
                        && m_arStacks[1].IsEmpty;
                case HorizontalMovement.OneLeft:
                    return m_arStacks[0].IsEmpty;
                case HorizontalMovement.None:
                    return true;
                case HorizontalMovement.OneRight:
                    return m_arStacks[3].IsEmpty;
                case HorizontalMovement.TwoRight:
                    return m_arStacks[3].IsEmpty
                        && m_arStacks[2].IsEmpty;
                case HorizontalMovement.ThreeRight:
                    return m_arStacks[3].IsEmpty
                        && m_arStacks[2].IsEmpty
                        && m_arStacks[1].IsEmpty;
                default:
                    throw new InvalidEnumArgumentException(
                        argumentName: nameof(_eHorizontalMovement),
                        invalidValue: (int)_eHorizontalMovement,
                        enumClass: typeof(HorizontalMovement));
            }
        }

        public List<Card> Gather(ColumnDefinition _excluding)
        {
            List<Card> result = new();

            foreach (ColumnDefinition column in ColumnDefinitionUtils.EnumerateAll(_excluding))
            {
                result.AddRange(this.GetStack(column).Gather());
            }

            return result;
        }

        public Stack GetStack(ColumnDefinition _eColumnDefinition)
        {
            switch (_eColumnDefinition)
            {
                case ColumnDefinition.First:
                    return m_arStacks[0];
                case ColumnDefinition.Second:
                    return m_arStacks[1];
                case ColumnDefinition.Third:
                    return m_arStacks[2];
                case ColumnDefinition.Fourth:
                    return m_arStacks[3];
                default:
                    throw new ArgumentException(nameof(_eColumnDefinition));
            }
        }
    }
}
