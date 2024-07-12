using NUnit.Framework;
using Phwitti.PlayingCards;

namespace Phwitti.Gomori.Tests
{
    public class TestBoard
    {
        [Test]
        public void TestCanMoveHorizontal()
        {
            Board board = new();
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovementUtils.Any, VerticalMovement = VerticalMovementUtils.Any }));

            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = RowDefinitionUtils.Any, Column = ColumnDefinition.First }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.OneLeft, VerticalMovement = VerticalMovement.None }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.TwoLeft, VerticalMovement = VerticalMovement.None }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.ThreeLeft, VerticalMovement = VerticalMovement.None }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.OneRight, VerticalMovement = VerticalMovement.None }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.TwoRight, VerticalMovement = VerticalMovement.None }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.ThreeRight, VerticalMovement = VerticalMovement.None }));

            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = RowDefinitionUtils.Any, Column = ColumnDefinition.Third }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.OneRight, VerticalMovement = VerticalMovement.None }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.TwoRight, VerticalMovement = VerticalMovement.None }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.ThreeRight, VerticalMovement = VerticalMovement.None }));

            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = RowDefinitionUtils.Any, Column = ColumnDefinition.Fourth }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.OneRight, VerticalMovement = VerticalMovement.None }));
        }

        [Test]
        public void TestCanMoveVertical()
        {
            Board board = new();
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovementUtils.Any, VerticalMovement = VerticalMovementUtils.Any }));

            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = RowDefinition.First, Column = ColumnDefinitionUtils.Any }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.OneUp }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.TwoUp }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.ThreeUp }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.OneDown }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.TwoDown }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.ThreeDown }));

            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = RowDefinition.Third, Column = ColumnDefinitionUtils.Any }));
            Assert.That(board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.OneDown }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.TwoDown }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.ThreeDown }));

            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = RowDefinition.Fourth, Column = ColumnDefinitionUtils.Any }));
            Assert.That(!board.CanMove(new MoveAction { HorizontalMovement = HorizontalMovement.None, VerticalMovement = VerticalMovement.OneDown }));
        }

        [Test]
        public void TestGetStack()
        {
            Board board = new();
            RowDefinition row = RowDefinitionUtils.Any;
            ColumnDefinition column = ColumnDefinitionUtils.Any;
            board.Apply(new PlayAction(Card.Any, new FieldDefinition { Row = row, Column = column }));

            Assert.That(!board.test_GetStack(
                new FieldDefinition { Row = row, Column = column }).IsEmpty);

            Assert.That(board.test_GetStack(
                new FieldDefinition
                {
                    Row = row,
                    Column = ColumnDefinitionUtils.GetAny(_excluding: column)
                }).IsEmpty);
        }

        [Test]
        public void TestApplyMoveActionHorizontal()
        {
            Board board = new();
            board.Apply(new PlayAction(
                _card: Card.Any,
                _target: new FieldDefinition
                {
                    Row = RowDefinitionUtils.Any,
                    Column = ColumnDefinition.Second
                }));

            Assert.That(board.test_GetColumn(ColumnDefinition.First).IsEmpty);
            Assert.That(!board.test_GetColumn(ColumnDefinition.Second).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Third).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Fourth).IsEmpty);

            board.Apply(new MoveAction { HorizontalMovement = HorizontalMovement.OneLeft });
            Assert.That(!board.test_GetColumn(ColumnDefinition.First).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Second).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Third).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Fourth).IsEmpty);

            board.Apply(new MoveAction { HorizontalMovement = HorizontalMovement.ThreeRight });
            Assert.That(board.test_GetColumn(ColumnDefinition.First).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Second).IsEmpty);
            Assert.That(board.test_GetColumn(ColumnDefinition.Third).IsEmpty);
            Assert.That(!board.test_GetColumn(ColumnDefinition.Fourth).IsEmpty);
        }

        [Test]
        public void TestApplyMoveActionVertical()
        {
            Board board = new();
            board.Apply(new PlayAction(
                _card: Card.Any,
                _target: new FieldDefinition
                {
                    Row = RowDefinition.Second,
                    Column = ColumnDefinitionUtils.Any
                }));

            Assert.That(board.test_GetRow(RowDefinition.First).IsEmpty);
            Assert.That(!board.test_GetRow(RowDefinition.Second).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Third).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Fourth).IsEmpty);

            board.Apply(new MoveAction { VerticalMovement = VerticalMovement.OneUp });
            Assert.That(!board.test_GetRow(RowDefinition.First).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Second).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Third).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Fourth).IsEmpty);

            board.Apply(new MoveAction { VerticalMovement = VerticalMovement.ThreeDown });
            Assert.That(board.test_GetRow(RowDefinition.First).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Second).IsEmpty);
            Assert.That(board.test_GetRow(RowDefinition.Third).IsEmpty);
            Assert.That(!board.test_GetRow(RowDefinition.Fourth).IsEmpty);
        }

        [Test]
        public void TestApplyPlayAction()
        {
            Board board = new();
            var suit = Suit.Any;
            var suitOther = Suit.GetAny(_excluding: suit);
            var column = ColumnDefinitionUtils.Any;
            var result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suit, Rank.Any),
                    new FieldDefinition { Column = column, Row = RowDefinition.First })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(!result.ShouldPlayAnotherCard);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suit, Rank.Any),
                    new FieldDefinition { Column = column, Row = RowDefinition.Second })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(!result.ShouldPlayAnotherCard);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suitOther, Rank.Any),
                    new FieldDefinition { Column = column, Row = RowDefinition.Third })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(!result.ShouldPlayAnotherCard);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suitOther, Rank.Any),
                    new FieldDefinition { Column = column, Row = RowDefinition.Fourth })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(!result.ShouldPlayAnotherCard);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suit, Rank.Ace),
                    new FieldDefinition { Column = column, Row = RowDefinition.Third })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(result.ShouldPlayAnotherCard);
            Assert.That(!board.test_GetStack(new FieldDefinition { Column = column, Row = RowDefinition.Second }).IsFaceDown);
            Assert.That(!board.test_GetStack(new FieldDefinition { Column = column, Row = RowDefinition.Fourth }).IsFaceDown);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suit, Rank.Jack),
                    new FieldDefinition { Column = column, Row = RowDefinition.Third })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(result.ShouldPlayAnotherCard);
            Assert.That(board.test_GetStack(new FieldDefinition { Column = column, Row = RowDefinition.Second }).IsFaceDown);
            Assert.That(board.test_GetStack(new FieldDefinition { Column = column, Row = RowDefinition.Fourth }).IsFaceDown);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suit, Rank.AnyNumber),
                    new FieldDefinition { Column = column, Row = RowDefinition.Second })
            });
            Assert.That(result.Gathered.Count == 0);
            Assert.That(result.ShouldPlayAnotherCard);

            result = board.Apply(new Action()
            {
                PlayAction = new PlayAction(
                    new Card(suit, Rank.AnyNumber),
                    new FieldDefinition { Column = column, Row = RowDefinition.Fourth })
            });
            Assert.That(result.Gathered.Count == 6);
            Assert.That(result.ShouldPlayAnotherCard);
        }
    }
}
