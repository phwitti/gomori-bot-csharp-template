using Phwitti.Gomori;
using System.Collections.Generic;

namespace Phwitti.GomoriBot
{
    public abstract class GomoriBotBase
    {
        private DeckColor m_deckColor;

        //

        public DeckColor DeckColor
            => m_deckColor;

        //

        public virtual void Initialize(DeckColor _deckColor)
        {
            m_deckColor = _deckColor;
        }

        public virtual IEnumerable<Action> EnumerateActionsForBoardAndHand(Board _board, Hand _hand)
        {
            bool bShouldPlayAnotherCard;

            do
            {
                Action? conjecturedAction = this.GetActionForBoardAndHand(_board, _hand);

                if (conjecturedAction == null)
                    break;

                Action action = conjecturedAction.Value;
                if (TryApplyAction(_board, _hand, action, out bShouldPlayAnotherCard))
                    yield return action;

            } while (bShouldPlayAnotherCard);
        }

        public abstract Action? GetActionForBoardAndHand(Board _board, Hand _hand);

        //

        private bool TryApplyAction(Board _board, Hand _hand, Action _action, out bool _bShouldPlayAnotherCard)
        {
            if (!_hand.Contains(_action.PlayAction.Card))
            {
                _bShouldPlayAnotherCard = false;
                return false;
            }

            try
            {
                var result = _board.Apply(_action);
                _hand.Remove(_action.PlayAction.Card);
                _bShouldPlayAnotherCard = result.ShouldPlayAnotherCard;

                return true;
            }
            catch
            {
                _bShouldPlayAnotherCard = false;
                return false;
            }
        }
    }
}
