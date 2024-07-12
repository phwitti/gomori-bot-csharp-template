using System;
using System.Linq;
using System.Collections.Generic;

namespace Phwitti.PlayingCards
{
    public static class ShuffleUtils
    {
        private static Random? s_random;

        //

        public static Random RandomInstance
        {
            get
            {
                if (s_random == null)
                    s_random = new System.Random();

                return s_random;
            }
        }

        public static T Random<T>(this IEnumerable<T> _enumerable)
        {
            int iCount = _enumerable.Count();
            return _enumerable.Skip(RandomInstance.Next(iCount)).First();
        }
    }
}
