using Phwitti.PlayingCards;
using System;
using System.Linq;

namespace Phwitti.Gomori
{
    public enum HorizontalMovement
    {
        ThreeLeft = -3,
        TwoLeft = -2,
        OneLeft = -1,
        None = 0,
        OneRight = 1,
        TwoRight = 2,
        ThreeRight = 3
    }

    public static class HorizontalMovementUtils
    {
        private static readonly HorizontalMovement[] s_arAll = [
            HorizontalMovement.ThreeLeft, HorizontalMovement.TwoLeft, HorizontalMovement.OneLeft, HorizontalMovement.None,
            HorizontalMovement.OneRight, HorizontalMovement.TwoRight, HorizontalMovement.ThreeRight ];

        public static HorizontalMovement[] All
            => s_arAll;

        public static HorizontalMovement Any
            => s_arAll.Random();        

        public static HorizontalMovement GetAny(HorizontalMovement[] _arExcluding)
            => s_arAll.Where(x => !_arExcluding.Contains(x)).Random();
    }

    public enum VerticalMovement
    {
        ThreeUp = -3,
        TwoUp = -2,
        OneUp = -1,
        None = 0,
        OneDown = 1,
        TwoDown = 2,
        ThreeDown = 3
    }

    public static class VerticalMovementUtils
    {
        private static readonly VerticalMovement[] s_arAll = [
            VerticalMovement.ThreeUp, VerticalMovement.TwoUp, VerticalMovement.OneUp, VerticalMovement.None,
            VerticalMovement.OneDown, VerticalMovement.TwoDown, VerticalMovement.ThreeDown ];

        public static VerticalMovement[] All
            => s_arAll;

        public static VerticalMovement Any
            => s_arAll.Random();        

        public static VerticalMovement GetAny(VerticalMovement[] _arExcluding)
            => s_arAll.Where(x => !_arExcluding.Contains(x)).Random();
    }
}
