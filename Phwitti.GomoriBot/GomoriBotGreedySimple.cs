using Phwitti.Gomori;

namespace Phwitti.GomoriBot
{
    public class GomoriBotGreedySimple : GomoriBotGreedyBase
    {
        public override int GetRatingForAction(Board _board, Hand _hand, Action _action)
        {
            var result = new Board(_board).Apply(_action);

            return result.Gathered.Count * 2
                + (result.ShouldPlayAnotherCard ? 1 : 0);
        }
    }
}
