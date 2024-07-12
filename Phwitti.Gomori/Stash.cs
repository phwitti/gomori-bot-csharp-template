using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;

namespace Phwitti.Gomori
{
    public class Stash
    {
        private List<Card> m_liCards = new();

        //

        public int Count
            => m_liCards.Count;

        //

        public void Add(IReadOnlyList<Card> _gathered)
        {
            m_liCards.AddRange(_gathered);
        }
    }
}
