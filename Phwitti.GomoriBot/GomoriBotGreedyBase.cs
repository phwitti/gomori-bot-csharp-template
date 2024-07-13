using Phwitti.Gomori;
using System.Collections.Generic;

namespace Phwitti.GomoriBot
{
    public abstract class GomoriBotGreedyBase : GomoriBotBase
    {
        public override Action? GetActionForBoardAndHand(Board _board, Hand _hand)
        {
            int iMaxRating = int.MinValue;
            List<Action> actions = new();

            foreach (var card in _hand.Cards)
            {
                foreach (var action in _board.GetValidActions(card))
                {
                    int iRating = this.GetRatingForAction(_board, _hand, action);

                    if (iRating > iMaxRating)
                    {
                        actions.Clear();
                        iMaxRating = iRating;
                    }

                    if (iRating == iMaxRating)
                    {
                        actions.Add(action);
                    }
                }
            }

            return actions.Count == 0
                 ? null
                 : actions[Phwitti.PlayingCards.ShuffleUtils.RandomInstance.Next(actions.Count)];
        }

        public abstract int GetRatingForAction(Board _board, Hand _hand, Action _action);
    }
}
