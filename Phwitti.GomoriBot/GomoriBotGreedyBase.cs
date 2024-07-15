using Phwitti.Gomori;
using System.Collections.Generic;

namespace Phwitti.GomoriBot
{
    public abstract class GomoriBotGreedyBase : GomoriBotBase
    {
        public override Action? GetActionForBoardAndHand(IReadOnlyBoard _board, Hand _hand)
        {
            float fMaxRating = float.NegativeInfinity;
            List<Action> actions = new();

            foreach (var card in _hand.Cards)
            {
                foreach (var action in _board.GetValidActions(card))
                {
                    float fRating = this.GetRatingForAction(_board, _hand, action);

                    if (fRating > fMaxRating)
                    {
                        actions.Clear();
                        fMaxRating = fRating;
                    }

                    if (fRating == fMaxRating)
                    {
                        actions.Add(action);
                    }
                }
            }

            return actions.Count == 0
                 ? null
                 : actions[PlayingCards.ShuffleUtils.RandomInstance.Next(actions.Count)];
        }

        public abstract float GetRatingForAction(IReadOnlyBoard _board, Hand _hand, Action _action);
    }
}
