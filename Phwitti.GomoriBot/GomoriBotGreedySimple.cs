using Phwitti.Gomori;

namespace Phwitti.GomoriBot
{
    public class GomoriBotGreedySimple : GomoriBotGreedyBase
    {
        public override float GetRatingForAction(Board _board, Hand _hand, Action _action)
        {
            var result = new Board(_board).Apply(_action);

            return result.Gathered.Count
                + (result.ShouldPlayAnotherCard ? 0.1f : 0f);
        }
    }
}
