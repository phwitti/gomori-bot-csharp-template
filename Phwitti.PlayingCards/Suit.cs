using System;
using System.Linq;

namespace Phwitti.PlayingCards
{
    public struct Suit : IComparable<Suit>, IEquatable<Suit>
    {
        private SuitType m_type;

        //

        public bool IsRed
            => m_type == SuitType.Diamonds
            || m_type == SuitType.Hearts;

        public bool IsBlack
            => m_type == SuitType.Spades
            || m_type == SuitType.Clubs;

        public SuitType Type
            => m_type;

        //

        public Suit(SuitType _definition)
        {
            m_type = _definition;
        }

        public override bool Equals(object? obj)
            => obj is Suit suit
                && Equals(suit);

        public override int GetHashCode()
            => m_type.GetHashCode();

        // IComparable<Suit>
        public int CompareTo(Suit _other)
            => m_type.CompareTo(_other.m_type);

        // IEquatable<Suit>
        public bool Equals(Suit _other)
            => m_type == _other.m_type;

        //

        private static Suit[] s_arAll = new Suit[] {
            Suit.Diamonds, Suit.Hearts, Suit.Spades, Suit.Clubs };

        public static Suit Diamonds
            => new Suit(SuitType.Diamonds);

        public static Suit Hearts
            => new Suit(SuitType.Hearts);

        public static Suit Spades
            => new Suit(SuitType.Spades);

        public static Suit Clubs
            => new Suit(SuitType.Clubs);

        public static Suit[] All
            => s_arAll;

        public static Suit Any
            => s_arAll.Random();

        public static Suit GetAny(Suit _excluding)
            => s_arAll.Where(x => x != _excluding).Random();

        public static Suit GetAny(Suit[] _arExcluding)
            => s_arAll.Where(x => !_arExcluding.Contains(x)).Random();

        public static bool operator ==(Suit a, Suit b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Suit a, Suit b)
        {
            return !(a == b);
        }

        //

        public enum SuitType
        {
            Diamonds,
            Hearts,
            Spades,
            Clubs
        }
    }
}
