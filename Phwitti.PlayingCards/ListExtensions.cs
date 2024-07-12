using System.Collections.Generic;

namespace Phwitti.PlayingCards
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> _list)
        {
            int n = _list.Count;
            while (n > 1)
            {
                int k = ShuffleUtils.RandomInstance.Next(n--);
                (_list[n], _list[k]) = (_list[k], _list[n]);
            }
        }
    }
}
