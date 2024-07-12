using System.Collections.Generic;

namespace Phwitti.Gomori
{
    public class Player
    {
        private readonly Deck m_deck;
        private readonly Hand m_hand;
        private readonly Stash m_stash;

        //

        public IReadOnlyHand Hand
            => m_hand;

        public bool IsHandEmpty
            => m_hand.IsEmpty;

        public DeckCount DeckCount
            => m_deck.DeckCount;

        public DeckCount StashCount
            => Deck.DeckCountFromInt(m_stash.Count, Deck.FullDeckCount);

        //

        public Player(DeckColor _color)
        {
            m_deck = new Deck(_color);
            m_deck.Shuffle();
            m_hand = new Hand();
            m_stash = new Stash();
        }

        public void Apply(Action _action)
        {
            if (!this.CanApply(_action))
                throw new System.InvalidOperationException();

            m_hand.Remove(_action.PlayAction.Card);
        }

        public bool CanApply(Action _action)
        {
            return m_hand.Contains(_action.PlayAction.Card);
        }

        public void Gather(IReadOnlyList<PlayingCards.Card> _gathered)
        {
            m_stash.Add(_gathered);
        }

        public bool TryDrawCards()
        {
            return m_hand.TryFillHandFromDeck(m_deck);
        }
    }
}
