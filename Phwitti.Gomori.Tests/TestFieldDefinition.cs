using NUnit.Framework;

namespace Phwitti.Gomori.Tests
{
    public class TestRowDefinitionUtils
    {
        [Test]
        public void TestTryOffset()
        {
            Assert.That(!RowDefinitionUtils.TryOffset(RowDefinition.First, HorizontalMovement.OneLeft, out _));
            Assert.That(RowDefinitionUtils.TryOffset(RowDefinition.First, HorizontalMovement.OneRight, out RowDefinition eRowOffsetted));
            Assert.That(eRowOffsetted == RowDefinition.Second);
        }
    }

    public class TestColumnDefinitionUtils
    {
        [Test]
        public void TestTryOffset()
        {
            Assert.That(!ColumnDefinitionUtils.TryOffset(ColumnDefinition.First, VerticalMovement.OneUp, out _));
            Assert.That(ColumnDefinitionUtils.TryOffset(ColumnDefinition.First, VerticalMovement.OneDown, out ColumnDefinition eColumnOffsetted));
            Assert.That(eColumnOffsetted == ColumnDefinition.Second);
        }
    }
}
