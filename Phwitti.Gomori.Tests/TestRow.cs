using NUnit.Framework;

namespace Phwitti.Gomori.Tests
{
    public class TestRow
    {
        [Test]
        public void TestCanMove()
        {
            Row row = new(new Stack(), new Stack(), new Stack(), new Stack());
            row.GetStack(ColumnDefinition.Second).Play(PlayingCards.Card.Any);
            
            Assert.That(row.CanMove(HorizontalMovement.OneLeft));
            Assert.That(!row.CanMove(HorizontalMovement.TwoLeft));
            Assert.That(row.CanMove(HorizontalMovement.OneRight));
            Assert.That(row.CanMove(HorizontalMovement.TwoRight));
            Assert.That(!row.CanMove(HorizontalMovement.ThreeRight));
        }
    }
}
