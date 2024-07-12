using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Phwitti.PlayingCards
{
    public class Hand
    {
        private int m_iCardsOnHand;
        private List<ICard> m_liCards = new();

        //

        public IReadOnlyList<ICard> Cards
            => m_liCards;

        public bool IsEmpty
            => m_liCards.Count == 0;

        //

        public Hand(int _iCardsOnHand)
        {
            m_iCardsOnHand = _iCardsOnHand;
        }

        public Hand(Hand _other)
        {
            m_iCardsOnHand = _other.m_iCardsOnHand;
            m_liCards = new List<ICard>(_other.m_liCards);
        }

        public Hand(int _iCardsOnHand, IEnumerable<ICard> _liCards)
        {
            m_iCardsOnHand = _iCardsOnHand;
            m_liCards = new List<ICard>(_liCards);

            if (m_liCards.Count > m_iCardsOnHand)
            {
                m_liCards.RemoveRange(m_iCardsOnHand, m_liCards.Count - m_iCardsOnHand);
            }
        }

        public bool Contains(ICard card)
        {
            return m_liCards.Contains(card);
        }

        public void Remove(ICard card)
        {
            m_liCards.Remove(card);
        }

        public bool TryGetCard(int _iIndex, [NotNullWhen(true)] out ICard? _card)
        {
            _card = default;

            if (_iIndex < 0 || _iIndex >= m_liCards.Count)
                return false;

            _card = m_liCards[_iIndex];
            return true;
        }

        public bool TryFillHandFromDeck(Deck _deck)
        {
            while (m_liCards.Count < m_iCardsOnHand)
            {
                if (!_deck.TryDrawOne(out ICard? card))
                    return false;

                m_liCards.Add(card);
            }

            return true;
        }
    }
}
