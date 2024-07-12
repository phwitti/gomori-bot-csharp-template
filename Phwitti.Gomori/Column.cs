using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Phwitti.Gomori
{
    public class Column
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

        public Column(Stack _stack0, Stack _stack1, Stack _stack2, Stack _stack3)
        {
            m_arStacks = new Stack[4];
            m_arStacks[0] = _stack0;
            m_arStacks[1] = _stack1;
            m_arStacks[2] = _stack2;
            m_arStacks[3] = _stack3;
        }

        public bool CanMove(VerticalMovement _eVerticalMovement)
        {
            switch (_eVerticalMovement)
            {
                case VerticalMovement.ThreeUp:
                    return m_arStacks[0].IsEmpty
                        && m_arStacks[1].IsEmpty
                        && m_arStacks[2].IsEmpty;
                case VerticalMovement.TwoUp:
                    return m_arStacks[0].IsEmpty
                        && m_arStacks[1].IsEmpty;
                case VerticalMovement.OneUp:
                    return m_arStacks[0].IsEmpty;
                case VerticalMovement.None:
                    return true;
                case VerticalMovement.OneDown:
                    return m_arStacks[3].IsEmpty;
                case VerticalMovement.TwoDown:
                    return m_arStacks[3].IsEmpty
                        && m_arStacks[2].IsEmpty;
                case VerticalMovement.ThreeDown:
                    return m_arStacks[3].IsEmpty
                        && m_arStacks[2].IsEmpty
                        && m_arStacks[1].IsEmpty;
                default:
                    throw new InvalidEnumArgumentException(
                        argumentName: nameof(_eVerticalMovement),
                        invalidValue: (int)_eVerticalMovement,
                        enumClass: typeof(VerticalMovement));
            }
        }

        public List<Card> Gather(RowDefinition _excluding)
        {
            List<Card> result = new();

            foreach (RowDefinition row in RowDefinitionUtils.EnumerateAll(_excluding))
            {
                result.AddRange(this.GetStack(row).Gather());
            }

            return result;
        }

        public Stack GetStack(RowDefinition _eRowDefinition)
        {
            switch (_eRowDefinition)
            {
                case RowDefinition.First:
                    return m_arStacks[0];
                case RowDefinition.Second:
                    return m_arStacks[1];
                case RowDefinition.Third:
                    return m_arStacks[2];
                case RowDefinition.Fourth:
                    return m_arStacks[3];
                default:
                    throw new ArgumentException(nameof(_eRowDefinition));
            }
        }
    }
}
