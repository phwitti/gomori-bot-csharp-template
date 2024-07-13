using Phwitti.Gomori;
using System.Collections.Generic;

namespace Phwitti.GomoriBot
{
    public class GomoriBotRandom : GomoriBotBase
    {
        public override Action? GetActionForBoardAndHand(Board _board, Hand _hand)
        {
            List<Action> actions = new();

            foreach (var card in _hand.Cards)
            {
                actions.AddRange(_board.GetValidActions(card));
            }

            return actions.Count == 0
                 ? null
                 : actions[Phwitti.PlayingCards.ShuffleUtils.RandomInstance.Next(actions.Count)];
        }
    }
}
