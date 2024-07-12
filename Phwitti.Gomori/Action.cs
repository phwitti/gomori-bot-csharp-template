using Phwitti.PlayingCards;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori
{
    public struct Action
    {
        public PlayAction PlayAction;
        public MoveAction MoveAction;
    }

    public struct PlayAction
    {
        private Card m_card;
        private FieldDefinition m_target;
        private FieldDefinition? m_flipTarget;

        //

        public Card Card
            => m_card;

        public FieldDefinition Target
            => m_target;

        public FieldDefinition? FlipTarget
            => m_flipTarget;

        //

        public PlayAction(Card _card, FieldDefinition _target, FieldDefinition? _flipTarget = null)
        {
            m_card = _card;
            m_target = _target;
            m_flipTarget = _flipTarget;
        }
    }

    public struct MoveAction
    {
        public HorizontalMovement HorizontalMovement;
        public VerticalMovement VerticalMovement;
    }

    public struct FieldDefinition
    {
        public RowDefinition Row;
        public ColumnDefinition Column;

        //

        public bool TryOffset(RelativeFieldDefinition _offset, out FieldDefinition _fieldDefinition)
        {
            RowDefinition eRowOffsetted = default;
            ColumnDefinition eColumnOffsetted = default;

            if (!RowDefinitionUtils.TryOffset(Row, _offset.HorizontalOffset, out eRowOffsetted)
                || !ColumnDefinitionUtils.TryOffset(Column, _offset.VerticalOffset, out eColumnOffsetted))
            {
                _fieldDefinition = default;
                return false;
            }
            else
            {
                _fieldDefinition = new FieldDefinition { Row = eRowOffsetted, Column = eColumnOffsetted };
                return true;
            }
        }

        //

        public static FieldDefinition[] All
            => ColumnDefinitionUtils.All.SelectMany(column
                => RowDefinitionUtils.All.Select(row => new FieldDefinition { Row = row, Column = column })).ToArray();

        public static FieldDefinition Any
            => new FieldDefinition { Column = ColumnDefinitionUtils.Any, Row = RowDefinitionUtils.Any };

        public static IEnumerable<FieldDefinition> EnumerateAll(FieldDefinition _excluding)
        {
            foreach (var definition in All.Where(x => !x.Equals(_excluding)))
                yield return definition;
        }
    }

    public struct RelativeFieldDefinition
    {
        public HorizontalMovement HorizontalOffset;
        public VerticalMovement VerticalOffset;
    }
}
