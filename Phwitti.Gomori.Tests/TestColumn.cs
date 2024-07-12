using NUnit.Framework;

namespace Phwitti.Gomori.Tests
{
    public class TestColumn
    {
        [Test]
        public void TestCanMove()
        {
            Column column = new(new Stack(), new Stack(), new Stack(), new Stack());
            column.GetStack(RowDefinition.Second).Play(PlayingCards.Card.Any);
            
            Assert.That(column.CanMove(VerticalMovement.OneUp));
            Assert.That(!column.CanMove(VerticalMovement.TwoUp));
            Assert.That(column.CanMove(VerticalMovement.OneDown));
            Assert.That(column.CanMove(VerticalMovement.TwoDown));
            Assert.That(!column.CanMove(VerticalMovement.ThreeDown));
        }
    }
}
