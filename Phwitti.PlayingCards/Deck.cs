using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Phwitti.PlayingCards
{
    public class Deck
    {
        private List<ICard> m_liCards;

        //

        public int Count
            => m_liCards.Count;

        //

        public Deck(List<Card> _liCards)
        {
            m_liCards = _liCards.Select(x => (ICard)x).ToList();
        }

        public Deck(List<ICard> _liCards)
        {
            m_liCards = new(_liCards);
        }

        public void Shuffle()
        {
            m_liCards.Shuffle();
        }

        public bool TryDrawOne([NotNullWhen(true)] out ICard? _card)
        {
            _card = default;

            if (m_liCards.Count == 0)
                return false;

            _card = m_liCards[0];
            m_liCards.RemoveAt(0);
            return true;
        }
    }
}
