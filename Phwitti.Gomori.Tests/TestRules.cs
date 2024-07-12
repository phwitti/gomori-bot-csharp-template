using NUnit.Framework;
using Phwitti.PlayingCards;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori.Tests
{
    public class TestRules
    {
        [Test]
        public void TestCanPlayCardOnEmpty()
        {
            Assert.That(Rules.CanPlayCardOnEmpty(Card.Any));
        }

        [Test]
        public void TestCanPlayCardOnFaceDown()
        {
            Assert.That(Rules.CanPlayCardOnFaceDown(Card.Any));
        }

        [Test]
        public void TestCanPlayCardOnOther()
        {
            Suit suit = Suit.Any;
            Suit suitOther = Suit.GetAny(_excluding: suit);
            Rank rankNumber = Rank.AnyNumber;
            Rank rankFace = Rank.AnyFace;
            Rank rankOther = Rank.GetAny(_arExcluding: new Rank[] { rankNumber, rankFace });

            // Number cards can be played on cards of equal rank
            Assert.That(Rules.CanPlayCardOnOther(
                _card: new Card(suit, rankNumber),
                _other: new Card(suitOther, rankNumber)));

            // But not on those of another rank
            Assert.That(!Rules.CanPlayCardOnOther(
                _card: new Card(suit, rankNumber),
                _other: new Card(suit, Rank.GetAny(_excluding: rankNumber))));

            // Face cards can additionally be played on cards of equal suit
            Assert.That(Rules.CanPlayCardOnOther(
                _card: new Card(suit, rankFace),
                _other: new Card(suit, Rank.GetAny(_excluding: rankFace))));

            // Both cannot be played on cards of another suit
            Assert.That(!Rules.CanPlayCardOnOther(
                _card: new Card(suit, rankNumber),
                _other: new Card(suitOther, rankOther)));

            Assert.That(!Rules.CanPlayCardOnOther(
                _card: new Card(suit, rankFace),
                _other: new Card(suitOther, rankOther)));

            // Aces can be played everywhere
            Assert.That(Rules.CanPlayCardOnOther(
                _card: new Card(suit, Rank.Ace),
                _other: new Card(suit, Rank.GetAny(_excluding: Rank.Ace))));

            Assert.That(Rules.CanPlayCardOnOther(
                _card: new Card(suit, Rank.Ace),
                _other: new Card(suitOther, Rank.Any)));
        }

        [Test]
        public void TestCanSelectTurnTargetOnActivation()
        {
            Assert.That(Rules.CanSelectTurnTargetOnActivation(new Card(Suit.Any, Rank.King)));
            Assert.That(!Rules.CanSelectTurnTargetOnActivation(new Card(Suit.Any, Rank.GetAny(Rank.King))));
        }

        [Test]
        public void TestIsPlayOnEmptyActivating()
        {
            Assert.That(!Rules.IsPlayOnEmptyActivating(Card.Any));
        }

        [Test]
        public void TestIsPlayOnFaceDownActivating()
        {
            Assert.That(!Rules.IsPlayOnFaceDownActivating(new Card(Suit.Any, Rank.AnyNumber)));
            Assert.That(Rules.IsPlayOnFaceDownActivating(new Card(Suit.Any, Rank.AnyFace)));
            Assert.That(!Rules.IsPlayOnFaceDownActivating(new Card(Suit.Any, Rank.Ace)));
        }

        [Test]
        public void TestIsPlayOnOtherActivating()
        {
            Assert.That(!Rules.IsPlayOnOtherActivating(new Card(Suit.Any, Rank.AnyNumber), Card.Any));
            Assert.That(Rules.IsPlayOnOtherActivating(new Card(Suit.Any, Rank.AnyFace), Card.Any));
            Assert.That(!Rules.IsPlayOnOtherActivating(new Card(Suit.Any, Rank.Ace), Card.Any));
        }

        [Test]
        public void TestIsPlayOnEmptyStacking()
        {
            Assert.That(!Rules.IsPlayOnEmptyStacking(Card.Any));
        }

        [Test]
        public void TestIsPlayOnFaceDownStacking()
        {
            Assert.That(Rules.IsPlayOnFaceDownStacking(Card.Any));
        }

        [Test]
        public void TestIsPlayOnOtherStacking()
        {
            Assert.That(Rules.IsPlayOnOtherStacking(Card.Any, Card.Any));
        }

        [Test]
        public void TestEnumerateFlipTargets()
        {
            Assert.That(Rules.EnumerateFlipTargets(Rank.AnyNumber).Count() == 0);
            Assert.That(Rules.EnumerateFlipTargets(Rank.Jack).Count() == 4);
            Assert.That(Rules.EnumerateFlipTargets(Rank.Queen).Count() == 4);
            Assert.That(Rules.EnumerateFlipTargets(Rank.King).Count() == 0);
            Assert.That(Rules.EnumerateFlipTargets(Rank.Ace).Count() == 0);

            List<RelativeFieldDefinition> liJackFlipTargets = Rules.EnumerateFlipTargets(Rank.Jack).ToList();
            Assert.That(liJackFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneLeft, VerticalOffset = VerticalMovement.None }));
            Assert.That(liJackFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneRight, VerticalOffset = VerticalMovement.None }));
            Assert.That(liJackFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.None, VerticalOffset = VerticalMovement.OneUp }));
            Assert.That(liJackFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.None, VerticalOffset = VerticalMovement.OneDown }));

            List<RelativeFieldDefinition> liQueenFlipTargets = Rules.EnumerateFlipTargets(Rank.Queen).ToList();
            Assert.That(liQueenFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneLeft, VerticalOffset = VerticalMovement.OneDown }));
            Assert.That(liQueenFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneLeft, VerticalOffset = VerticalMovement.OneUp }));
            Assert.That(liQueenFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneRight, VerticalOffset = VerticalMovement.OneDown }));
            Assert.That(liQueenFlipTargets.Contains(new RelativeFieldDefinition { HorizontalOffset = HorizontalMovement.OneRight, VerticalOffset = VerticalMovement.OneUp }));
        }
    }
}
