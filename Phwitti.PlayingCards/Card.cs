using System;

namespace Phwitti.PlayingCards
{
    public struct Card : IComparable<Card>, IEquatable<Card>, ICard
    {
        private Rank m_rank;
        private Suit m_suit;

        //

        // ICard
        public bool IsJoker => false;
        public Suit Suit => m_suit;
        public Rank Rank => m_rank;
        public override bool Equals(object? _other)
            => _other is Card card
                && Equals(card);

        public override int GetHashCode()
            => HashCode.Combine(
                m_rank.GetHashCode(),
                m_suit.GetHashCode());

        //

        public Card(Suit _suit, Rank _rank)
        {
            m_suit = _suit;
            m_rank = _rank;
        }

        // IComparable<Card>
        public int CompareTo(Card _other)
        {
            return !this.Suit.Equals(_other.Suit)
                ? this.Suit.CompareTo(_other.Suit)
                : this.Rank.CompareTo(_other.Rank);
        }

        // IEquatable<Card>
        public bool Equals(Card _other)
        {
            return m_rank == _other.m_rank
                && m_suit == _other.m_suit;
        }

        //

        public static Card Any
            => new Card(Suit.Any, Rank.Any);

        public static bool operator ==(Card a, Card b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Card a, Card b)
        {
            return !(a == b);
        }
    }
}
