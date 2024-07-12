using Phwitti.PlayingCards;
using System.Collections.Generic;

namespace Phwitti.Gomori
{
    public interface IReadOnlyStack
    {
        public bool IsFaceDown { get; }
        public bool IsEmpty { get; }
        int StackCount { get; }
        int StackedCount { get; }
        Card? FaceUpTopCard { get; }
        bool CanPlay(Card _card);
        bool CanTurnFaceDown();
        bool IsPlayActivating(Card _card);
        bool IsPlayStacking(Card _card);
        bool TryGetFaceUpTopCard(out Card _card);
        bool LooksAlike(IReadOnlyStack _stack);
    }

    public class Stack : IReadOnlyStack
    {
        private bool m_bIsFaceDown;
        private List<Card> m_liCards = new();

        //

        // IReadOnlyStack
        public bool IsFaceDown
            => m_bIsFaceDown;

        // IReadOnlyStack
        public bool IsEmpty
            => m_liCards.Count == 0;

        // IReadOnlyStack
        public int StackCount
            => m_liCards.Count;

        // IReadOnlyStack
        public int StackedCount
            => m_bIsFaceDown
                ? m_liCards.Count
                : System.Math.Max(0, m_liCards.Count - 1);

        // IReadOnlyStack
        public Card? FaceUpTopCard
            => this.TryGetFaceUpTopCard(out Card card)
                ? card
                : null;

        //

        public Stack()
        {
        }

        public Stack(Stack _other)
        {
            m_bIsFaceDown = _other.m_bIsFaceDown;
            m_liCards = new List<Card>(_other.m_liCards);
        }

        public Stack(IReadOnlyList<Card> _liCards, bool _bIsFaceDown = false)
        {
            m_bIsFaceDown = _bIsFaceDown;
            m_liCards = new List<Card>(_liCards);
        }

        // IReadOnlyStack
        public bool CanPlay(Card _card)
        {
            if (!this.TryGetFaceUpTopCard(out Card topCard))
                return Rules.CanPlayCardOnEmpty(_card);

            if (this.IsFaceDown)
                return Rules.CanPlayCardOnFaceDown(_card);

            return Rules.CanPlayCardOnOther(_card, _other: topCard);
        }

        // IReadOnlyStack
        public bool CanTurnFaceDown()
        {
            return !this.IsEmpty
                && !m_bIsFaceDown;
        }

        // IReadOnlyStack
        public bool IsPlayActivating(Card _card)
        {
            if (this.IsFaceDown)
                return Rules.IsPlayOnFaceDownActivating(_card);

            if (!this.TryGetFaceUpTopCard(out Card topCard))
                return Rules.IsPlayOnEmptyActivating(_card);

            return Rules.IsPlayOnOtherActivating(_card, topCard);
        }

        // IReadOnlyStack
        public bool IsPlayStacking(Card _card)
        {
            if (this.IsFaceDown)
                return Rules.IsPlayOnFaceDownStacking(_card);

            if (!this.TryGetFaceUpTopCard(out Card topCard))
                return Rules.IsPlayOnEmptyStacking(_card);

            return Rules.IsPlayOnOtherStacking(_card, topCard);
        }

        // IReadOnlyStack
        public bool TryGetFaceUpTopCard(out Card _card)
        {
            if (this.IsEmpty || m_bIsFaceDown)
            {
                _card = default;
                return false;
            }
            else
            {
                _card = m_liCards[m_liCards.Count - 1];
                return true;
            }
        }

        // IReadOnlyStack
        public bool LooksAlike(IReadOnlyStack _other)
        {
            return this.StackCount == _other.StackCount
                && this.FaceUpTopCard.HasValue == _other.FaceUpTopCard.HasValue
                && (this.FaceUpTopCard.HasValue && _other.FaceUpTopCard.HasValue
                    ? this.FaceUpTopCard.Value == _other.FaceUpTopCard.Value
                    : true);
        }

        public void Play(Card _card)
        {
            if (!this.CanPlay(_card))
                throw new System.ArgumentException("Invalid card to play on stack.", nameof(_card));

            m_liCards.Add(_card);
            m_bIsFaceDown = false;
        }

        public void TurnFaceDown()
        {
            if (!TryTurnFaceDown())
                throw new System.InvalidOperationException();
        }

        public bool TryTurnFaceDown()
        {
            if (this.IsEmpty)
                return false;

            if (m_bIsFaceDown)
                return false;

            m_bIsFaceDown = true;
            return true;
        }

        public List<Card> Gather()
        {
            var liCards = new List<Card>(m_liCards);
            m_liCards.Clear();
            m_bIsFaceDown = false;
            return liCards;
        }
    }
}
