using Phwitti.PlayingCards;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori
{
    public interface IReadOnlyBoard
    {
        bool IsEmpty { get; }
        bool CanMove(HorizontalMovement _eHorizontalMovement);
        bool CanMove(VerticalMovement _eVerticalMovement);
        IReadOnlyStack GetReadOnlyStack(FieldDefinition _fieldDefinition);
        bool LooksAlike(IReadOnlyBoard _other);
    }

    public class Board : IReadOnlyBoard
    {
        private Stack[] m_arStacks;
        private Row[] m_arRows;
        private Column[] m_arColumns;
        private Diagonal m_ascendingDiagonal;
        private Diagonal m_descendingDiagonal;

        //

        public bool IsEmpty
            => m_arStacks.All(x => x.IsEmpty);

        //

        public Board()
        {
            m_arStacks = new Stack[16] {
                new Stack(), new Stack(), new Stack(), new Stack(),
                new Stack(), new Stack(), new Stack(), new Stack(),
                new Stack(), new Stack(), new Stack(), new Stack(),
                new Stack(), new Stack(), new Stack(), new Stack() };

            this.InitializeColumnsDiagonalsAndRows(
                out m_arRows,
                out m_arColumns,
                out m_ascendingDiagonal,
                out m_descendingDiagonal);
        }

        public Board(IReadOnlyBoard _other)
        {
            // TODO: should use actual interface for GetReadOnlyStack
            Board other = (Board)_other;

            m_arStacks = new Stack[16] {
                new Stack(other.m_arStacks[0]), new Stack(other.m_arStacks[1]), new Stack(other.m_arStacks[2]), new Stack(other.m_arStacks[3]),
                new Stack(other.m_arStacks[4]), new Stack(other.m_arStacks[5]), new Stack(other.m_arStacks[6]), new Stack(other.m_arStacks[7]),
                new Stack(other.m_arStacks[8]), new Stack(other.m_arStacks[9]), new Stack(other.m_arStacks[10]), new Stack(other.m_arStacks[11]),
                new Stack(other.m_arStacks[12]), new Stack(other.m_arStacks[13]), new Stack(other.m_arStacks[14]), new Stack(other.m_arStacks[15])};

            this.InitializeColumnsDiagonalsAndRows(
                out m_arRows,
                out m_arColumns,
                out m_ascendingDiagonal,
                out m_descendingDiagonal);
        }

        public Board(
            Stack _stack0a, Stack _stack0b, Stack _stack0c, Stack _stack0d,
            Stack _stack1a, Stack _stack1b, Stack _stack1c, Stack _stack1d,
            Stack _stack2a, Stack _stack2b, Stack _stack2c, Stack _stack2d,
            Stack _stack3a, Stack _stack3b, Stack _stack3c, Stack _stack3d)
        {
            m_arStacks = new Stack[16] {
                _stack0a, _stack0b, _stack0c, _stack0d,
                _stack1a, _stack1b, _stack1c, _stack1d,
                _stack2a, _stack2b, _stack2c, _stack2d,
                _stack3a, _stack3b, _stack3c, _stack3d };

            this.InitializeColumnsDiagonalsAndRows(
                out m_arRows,
                out m_arColumns,
                out m_ascendingDiagonal,
                out m_descendingDiagonal);
        }

        //

        public ActionResult Apply(Action _action)
        {
            this.Apply(_action.MoveAction);
            return this.Apply(_action.PlayAction);
        }

        public ActionResult Apply(PlayAction _action)
        {
            bool isActivating = this.IsActivating(_action);
            bool isStacking = this.IsStacking(_action);

            this.GetStack(_action.Target).Play(_action.Card);

            if (isActivating)
            {
                foreach (var relativeTarget in Rules.EnumerateFlipTargets(_action.Card.Rank))
                    if (_action.Target.TryOffset(relativeTarget, out FieldDefinition target))
                        _ = this.GetStack(target).TryTurnFaceDown();

                if (_action.FlipTarget.HasValue)
                    this.GetStack(_action.FlipTarget.Value).TurnFaceDown();
            }

            List<Card> gathered = this.Gather(_fieldToExclude: _action.Target);
            return new ActionResult(_shouldPlayAnotherCard: isStacking, _gathered: gathered);
        }

        public void Apply(MoveAction _action)
        {
            this.MoveHorizontal(_action.HorizontalMovement);
            this.MoveVertical(_action.VerticalMovement);
        }

        public bool CanMove(MoveAction _moveAction)
        {
            return CanMove(_moveAction.HorizontalMovement)
                && CanMove(_moveAction.VerticalMovement);
        }

        public bool CanMove(HorizontalMovement _eHorizontalMovement)
        {
            if (_eHorizontalMovement == HorizontalMovement.None)
                return true;

            foreach (var row in m_arRows)
            {
                if (!row.CanMove(_eHorizontalMovement))
                    return false;
            }

            return true;
        }

        public bool CanMove(VerticalMovement _eVerticalMovement)
        {
            if (_eVerticalMovement == VerticalMovement.None)
                return true;

            foreach (var column in m_arColumns)
            {
                if (!column.CanMove(_eVerticalMovement))
                    return false;
            }

            return true;
        }

        // IReadOnlyBoard
        public IReadOnlyStack GetReadOnlyStack(FieldDefinition _fieldDefinition)
        {
            return this.GetRow(_fieldDefinition.Row)
                .GetStack(_fieldDefinition.Column);
        }

        public bool IsActivating(PlayAction _playAction)
        {
            return this.GetStack(_playAction.Target)
                .IsPlayActivating(_playAction.Card);
        }

        public bool IsStacking(PlayAction _playAction)
        {
            return this.GetStack(_playAction.Target)
                .IsPlayStacking(_playAction.Card);
        }

        //

        private void InitializeColumnsDiagonalsAndRows(
            out Row[] _arRows,
            out Column[] _arColumns,
            out Diagonal _ascendingDiagonal,
            out Diagonal _descendingDiagonal)
        {
            _arRows = new Row[4] {
                new Row(m_arStacks[0], m_arStacks[1], m_arStacks[2], m_arStacks[3]),
                new Row(m_arStacks[4], m_arStacks[5], m_arStacks[6], m_arStacks[7]),
                new Row(m_arStacks[8], m_arStacks[9], m_arStacks[10], m_arStacks[11]),
                new Row(m_arStacks[12], m_arStacks[13], m_arStacks[14], m_arStacks[15]) };

            _arColumns = new Column[4] {
                new Column(m_arStacks[0], m_arStacks[4], m_arStacks[8], m_arStacks[12]),
                new Column(m_arStacks[1], m_arStacks[5], m_arStacks[9], m_arStacks[13]),
                new Column(m_arStacks[2], m_arStacks[6], m_arStacks[10], m_arStacks[14]),
                new Column(m_arStacks[3], m_arStacks[7], m_arStacks[11], m_arStacks[15]) };

            _ascendingDiagonal
                = new Diagonal(m_arStacks[12], m_arStacks[9], m_arStacks[6], m_arStacks[3]);

            _descendingDiagonal
                = new Diagonal(m_arStacks[0], m_arStacks[5], m_arStacks[10], m_arStacks[15]);
        }

        private List<Card> Gather(FieldDefinition _fieldToExclude)
        {
            var gathered = new List<Card>();

            foreach (var row in m_arRows)
            {
                if (row.IsComplete)
                    gathered.AddRange(row.Gather(_excluding: _fieldToExclude.Column));
            }

            foreach (var column in m_arColumns)
            {
                if (column.IsComplete)
                    gathered.AddRange(column.Gather(_excluding: _fieldToExclude.Row));
            }

            if (m_ascendingDiagonal.IsComplete)
            {
                DiagonalPosition position = DiagonalPositionUtils.CreateFromFieldDefinition(
                    _fieldToExclude, DiagonalDefinition.Ascending);

                gathered.AddRange(m_ascendingDiagonal.Gather(_excluding: position));
            }

            if (m_descendingDiagonal.IsComplete)
            {
                DiagonalPosition position = DiagonalPositionUtils.CreateFromFieldDefinition(
                    _fieldToExclude, DiagonalDefinition.Descending);

                gathered.AddRange(m_descendingDiagonal.Gather(_excluding: position));
            }

            return gathered;
        }

        private Column GetColumn(ColumnDefinition _eColumnDefinition)
        {
            switch (_eColumnDefinition)
            {
                case ColumnDefinition.First:
                    return m_arColumns[0];
                case ColumnDefinition.Second:
                    return m_arColumns[1];
                case ColumnDefinition.Third:
                    return m_arColumns[2];
                case ColumnDefinition.Fourth:
                    return m_arColumns[3];
                default:
                    throw new System.ArgumentException(nameof(_eColumnDefinition));
            }
        }

        private Row GetRow(RowDefinition _eRowDefinition)
        {
            switch (_eRowDefinition)
            {
                case RowDefinition.First:
                    return m_arRows[0];
                case RowDefinition.Second:
                    return m_arRows[1];
                case RowDefinition.Third:
                    return m_arRows[2];
                case RowDefinition.Fourth:
                    return m_arRows[3];
                default:
                    throw new System.ArgumentException(nameof(_eRowDefinition));
            }
        }

        private Stack GetStack(FieldDefinition _fieldDefinition)
        {
            return this.GetRow(_fieldDefinition.Row)
                .GetStack(_fieldDefinition.Column);
        }

        private void MoveHorizontal(HorizontalMovement _eHorizontalMovement)
        {
            if (_eHorizontalMovement == HorizontalMovement.None)
                return;

            if (!CanMove(_eHorizontalMovement))
                throw new System.InvalidOperationException($"Can't move board with {_eHorizontalMovement}");


            int iMovement = (int)_eHorizontalMovement;
            Stack[] s = m_arStacks;
            m_arStacks = new Stack[16] {
                s[(0 * 4) + (4 - iMovement) % 4], s[(0 * 4) + (5 - iMovement) % 4], s[(0 * 4) + (6 - iMovement) % 4], s[(0 * 4) + (7 - iMovement) % 4],
                s[(1 * 4) + (4 - iMovement) % 4], s[(1 * 4) + (5 - iMovement) % 4], s[(1 * 4) + (6 - iMovement) % 4], s[(1 * 4) + (7 - iMovement) % 4],
                s[(2 * 4) + (4 - iMovement) % 4], s[(2 * 4) + (5 - iMovement) % 4], s[(2 * 4) + (6 - iMovement) % 4], s[(2 * 4) + (7 - iMovement) % 4],
                s[(3 * 4) + (4 - iMovement) % 4], s[(3 * 4) + (5 - iMovement) % 4], s[(3 * 4) + (6 - iMovement) % 4], s[(3 * 4) + (7 - iMovement) % 4] };

            this.InitializeColumnsDiagonalsAndRows(
                out m_arRows,
                out m_arColumns,
                out m_ascendingDiagonal,
                out m_descendingDiagonal);
        }

        private void MoveVertical(VerticalMovement _eVerticalMovement)
        {
            if (_eVerticalMovement == VerticalMovement.None)
                return;

            if (!CanMove(_eVerticalMovement))
                throw new System.InvalidOperationException($"Can't move board with {_eVerticalMovement}");

            int iMovement = (int)_eVerticalMovement;
            Stack[] s = m_arStacks;
            m_arStacks = new Stack[16] {
                s[((4 - iMovement) % 4 * 4) + 0], s[((4 - iMovement) % 4 * 4) + 1], s[((4 - iMovement) % 4 * 4) + 2], s[((4 - iMovement) % 4 * 4) + 3],
                s[((5 - iMovement) % 4 * 4) + 0], s[((5 - iMovement) % 4 * 4) + 1], s[((5 - iMovement) % 4 * 4) + 2], s[((5 - iMovement) % 4 * 4) + 3],
                s[((6 - iMovement) % 4 * 4) + 0], s[((6 - iMovement) % 4 * 4) + 1], s[((6 - iMovement) % 4 * 4) + 2], s[((6 - iMovement) % 4 * 4) + 3],
                s[((7 - iMovement) % 4 * 4) + 0], s[((7 - iMovement) % 4 * 4) + 1], s[((7 - iMovement) % 4 * 4) + 2], s[((7 - iMovement) % 4 * 4) + 3] };

            this.InitializeColumnsDiagonalsAndRows(
                out m_arRows,
                out m_arColumns,
                out m_ascendingDiagonal,
                out m_descendingDiagonal);
        }

        //

        // IReadOnlyBoard
        public bool LooksAlike(IReadOnlyBoard _other)
        {
            return StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.First, Row = RowDefinition.First })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Second, Row = RowDefinition.First })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Third, Row = RowDefinition.First })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Fourth, Row = RowDefinition.First })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.First, Row = RowDefinition.Second })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Second, Row = RowDefinition.Second })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Third, Row = RowDefinition.Second })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Fourth, Row = RowDefinition.Second })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.First, Row = RowDefinition.Third })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Second, Row = RowDefinition.Third })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Third, Row = RowDefinition.Third })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Fourth, Row = RowDefinition.Third })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.First, Row = RowDefinition.Fourth })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Second, Row = RowDefinition.Fourth })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Third, Row = RowDefinition.Fourth })
                && StacksLookAlike(_other, new FieldDefinition { Column = ColumnDefinition.Fourth, Row = RowDefinition.Fourth });
        }

        private bool StacksLookAlike(IReadOnlyBoard _other, FieldDefinition _fieldDefinition)
        {
            return this.GetReadOnlyStack(_fieldDefinition)
                .LooksAlike(_other.GetReadOnlyStack(_fieldDefinition));
        }

        //
        public Column test_GetColumn(ColumnDefinition _eColumnDefinition)
        {
            return this.GetColumn(_eColumnDefinition);
        }

        public Row test_GetRow(RowDefinition _eRowDefinition)
        {
            return this.GetRow(_eRowDefinition);
        }

        public Stack test_GetStack(FieldDefinition _fieldDefinition)
        {
            return this.GetStack(_fieldDefinition);
        }
    }
}
