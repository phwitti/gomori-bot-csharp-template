using System;
using System.Linq;

namespace Phwitti.PlayingCards
{
    public struct Rank : IComparable<Rank>, IEquatable<Rank>
    {
        private RankType m_type;

        //

        public bool IsAce
            => m_type == RankType.Ace;

        public bool IsFace
            => m_type == RankType.Jack
            || m_type == RankType.Queen
            || m_type == RankType.King;

        public bool IsNumber
            => !this.IsAce && !this.IsFace;

        public RankType Type
            => m_type;

        //

        public Rank(RankType _definition)
        {
            m_type = _definition;
        }

        public override bool Equals(object? obj)
            => obj is Rank rank
                && Equals(rank);

        public override int GetHashCode()
            => m_type.GetHashCode();

        // IComparable<Rank>
        public int CompareTo(Rank _other)
            => m_type.CompareTo(_other.m_type);

        // IEquatable<Rank>
        public bool Equals(Rank _other)
            => m_type == _other.m_type;

        //

        private static Rank[] s_arAll = new Rank[] {
            Rank.Ace, Rank.Two, Rank.Three, Rank.Four, Rank.Five,
            Rank.Six,Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten,
            Rank.Jack, Rank.Queen, Rank.King };

        private static Rank[] s_arAllFaces = new Rank[] {
            Rank.Jack, Rank.Queen, Rank.King };

        private static Rank[] s_arAllNumbers = new Rank[] {
            Rank.Two, Rank.Three, Rank.Four, Rank.Five,
            Rank.Six,Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten };

        public static Rank Ace => new Rank(RankType.Ace);
        public static Rank Two => new Rank(RankType.Two);
        public static Rank Three => new Rank(RankType.Three);
        public static Rank Four => new Rank(RankType.Four);
        public static Rank Five => new Rank(RankType.Five);
        public static Rank Six => new Rank(RankType.Six);
        public static Rank Seven => new Rank(RankType.Seven);
        public static Rank Eight => new Rank(RankType.Eight);
        public static Rank Nine => new Rank(RankType.Nine);
        public static Rank Ten => new Rank(RankType.Ten);
        public static Rank Jack => new Rank(RankType.Jack);
        public static Rank Queen => new Rank(RankType.Queen);
        public static Rank King => new Rank(RankType.King);

        public static Rank[] All
            => s_arAll;
        public static Rank Any
            => s_arAll.Random();
        public static Rank AnyFace
            => s_arAllFaces.Random();
        public static Rank AnyNumber
            => s_arAllNumbers.Random();

        public static Rank GetAny(Rank _excluding)
            => s_arAll.Where(x => x != _excluding).Random();
        public static Rank GetAny(Rank[] _arExcluding)
            => s_arAll.Where(x => !_arExcluding.Contains(x)).Random();
        public static Rank GetAnyFace(Rank _excluding)
            => s_arAllFaces.Where(x => x != _excluding).Random();
        public static Rank GetAnyFace(Rank[] _arExcluding)
            => s_arAllFaces.Where(x => !_arExcluding.Contains(x)).Random();
        public static Rank GetAnyNumber(Rank _excluding)
            => s_arAllNumbers.Where(x => x != _excluding).Random();
        public static Rank GetAnyNumber(Rank[] _arExcluding)
            => s_arAllNumbers.Where(x => !_arExcluding.Contains(x)).Random();

        public static bool operator ==(Rank a, Rank b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Rank a, Rank b)
        {
            return !(a == b);
        }

        //

        public enum RankType
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
        }

    }
}
