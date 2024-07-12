using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori
{
    public enum DiagonalDefinition
    {
        Ascending,
        Descending,
    }

    public class Diagonal
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

        public Diagonal(Stack _stack0, Stack _stack1, Stack _stack2, Stack _stack3)
        {
            m_arStacks = new Stack[4];
            m_arStacks[0] = _stack0;
            m_arStacks[1] = _stack1;
            m_arStacks[2] = _stack2;
            m_arStacks[3] = _stack3;
        }

        public List<Card> Gather(DiagonalPosition _excluding)
        {
            List<Card> result = new();

            foreach (DiagonalPosition position in DiagonalPositionUtils.EnumerateAll(_excluding))
            {
                result.AddRange(this.GetStack(position).Gather());
            }

            return result;
        }

        public Stack GetStack(DiagonalPosition _ePosition)
        {
            switch (_ePosition)
            {
                case DiagonalPosition.First:
                    return m_arStacks[0];
                case DiagonalPosition.Second:
                    return m_arStacks[1];
                case DiagonalPosition.Third:
                    return m_arStacks[2];
                case DiagonalPosition.Fourth:
                    return m_arStacks[3];
                default:
                    throw new ArgumentException(nameof(_ePosition));
            }
        }
    }
}
