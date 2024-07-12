using Phwitti.PlayingCards;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori
{
    public static class BoardExtensions
    {
        public static IEnumerable<Action> GetValidActions(this Board _board, Card _card)
        {
            foreach (var playAction in _board.GetValidPlayActions(_card))
                yield return new Action
                {
                    PlayAction = playAction,
                    MoveAction = new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.None }
                };
        }

        public static IEnumerable<MoveAction> GetValidMoveActions(this Board _board)
        {
            if (_board.IsEmpty)
            {
                yield return new MoveAction()
                {
                    HorizontalMovement = HorizontalMovement.None,
                    VerticalMovement = VerticalMovement.None
                };

                yield break;
            }

            foreach (var horizontal in _board.GetValidMoveActionsHorizontal())
                foreach (var vertical in _board.GetValidMoveActionsVertical())
                    yield return new MoveAction() { HorizontalMovement = horizontal, VerticalMovement = vertical };
        }

        public static IEnumerable<HorizontalMovement> GetValidMoveActionsHorizontal(this Board _board)
        {
            foreach (var movement in HorizontalMovementUtils.All.Where(_board.CanMove))
                yield return movement;
        }

        public static IEnumerable<VerticalMovement> GetValidMoveActionsVertical(this Board _board)
        {
            foreach (var movement in VerticalMovementUtils.All.Where(_board.CanMove))
                yield return movement;
        }

        public static IEnumerable<PlayAction> GetValidPlayActions(this Board _board, Card _card)
        {
            foreach (var target in FieldDefinition.All)
            {
                var stack = _board.GetReadOnlyStack(target);
                if (!stack.CanPlay(_card))
                    continue;

                if (!Rules.CanSelectTurnTargetOnActivation(_card) || !stack.IsPlayActivating(_card))
                {
                    yield return new PlayAction(_card, target);
                }
                else
                {
                    yield return new PlayAction(_card, target, target);

                    foreach (var flipTarget in FieldDefinition.EnumerateAll(_excluding: target).Where(x => _board.GetReadOnlyStack(x).CanTurnFaceDown()))
                        yield return new PlayAction(_card, target, flipTarget);
                }
            }
        }
    }
}
