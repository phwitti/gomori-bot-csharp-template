using NUnit.Framework;
using Phwitti.PlayingCards;
using System.Collections.Generic;

namespace Phwitti.Gomori.Tests
{
    public class TestStack
    {
        [Test]
        public void TestCanFlip()
        {
            Stack stack = new();
            Assert.That(!stack.CanTurnFaceDown());
            stack.Play(new Card(Suit.Any, Rank.Any));
            Assert.That(stack.CanTurnFaceDown());
            stack.Play(new Card(Suit.Any, Rank.Ace));
            Assert.That(stack.CanTurnFaceDown());
            stack.TurnFaceDown();
            Assert.That(!stack.CanTurnFaceDown());
            stack.Play(new Card(Suit.Any, Rank.Any));
            Assert.That(stack.CanTurnFaceDown());
        }


        [Test]
        public void TestCanPlay()
        {
            Stack stack = new();
            Assert.That(stack.CanPlay(Card.Any));

            Card numberCard = new Card(Suit.Any, Rank.AnyNumber);
            stack.Play(numberCard);
            Assert.That(stack.CanPlay(new Card(Suit.Any, numberCard.Rank)));
            Assert.That(stack.CanPlay(new Card(numberCard.Suit, Rank.AnyFace)));
            Assert.That(stack.CanPlay(new Card(Suit.Any, Rank.Ace)));

            Card otherNumberCard = new Card(Suit.Any, Rank.GetAnyNumber(_arExcluding: new Rank[] { numberCard.Rank }));
            Card otherSuitAnyFaceCard = new Card(Suit.GetAny(_arExcluding: new Suit[] { numberCard.Suit }), Rank.AnyFace);
            Assert.That(!stack.CanPlay(otherNumberCard));
            Assert.That(!stack.CanPlay(otherSuitAnyFaceCard));

            stack.TurnFaceDown();
            Assert.That(stack.CanPlay(otherNumberCard));
            Assert.That(stack.CanPlay(otherSuitAnyFaceCard));
        }

        [Test]
        public void TestGetFaceUpTopCard()
        {
            Stack stack = new();
            Assert.That(!stack.TryGetFaceUpTopCard(out _));

            var card = new Card(Suit.Any, Rank.GetAny(_arExcluding: new Rank[] { Rank.Ace }));
            stack.Play(card);

            Assert.That(stack.TryGetFaceUpTopCard(out Card topCardCheckSuit) && topCardCheckSuit.Suit == card.Suit);
            Assert.That(stack.TryGetFaceUpTopCard(out Card topCardCheckRank) && topCardCheckRank.Rank == card.Rank);
            stack.Play(new Card(Suit.GetAny(_arExcluding: new Suit[] { card.Suit }), Rank.Ace));
            Assert.That(stack.TryGetFaceUpTopCard(out Card topCardCheckSuitDiffer) && topCardCheckSuitDiffer.Suit != card.Suit);
            Assert.That(stack.TryGetFaceUpTopCard(out Card topCardCheckRankDiffer) && topCardCheckRankDiffer.Rank != card.Rank);
            stack.TryTurnFaceDown();
            Assert.That(!stack.TryGetFaceUpTopCard(out _));
        }

        [Test]
        public void TestIsEmpty()
        {
            Stack stack = new();
            Assert.That(stack.IsEmpty);
            stack.Play(Card.Any);
            Assert.That(!stack.IsEmpty);
        }

        [Test]
        public void TestStackCount()
        {
            Stack stack = new();
            Assert.That(stack.StackCount == 0);
            stack.Play(Card.Any);
            Assert.That(stack.StackCount == 1);
            stack.Play(new Card(Suit.Any, Rank.Ace));
            Assert.That(stack.StackCount == 2);
            stack.TurnFaceDown();
            Assert.That(stack.StackCount == 2);
            stack.Play(Card.Any);
            Assert.That(stack.StackCount == 3);
        }

        [Test]
        public void TestStackedCount()
        {
            Stack stack = new();
            Assert.That(stack.StackedCount == 0);
            stack.Play(Card.Any);
            Assert.That(stack.StackedCount == 0);
            stack.Play(new Card(Suit.Any, Rank.Ace));
            Assert.That(stack.StackedCount == 1);
            stack.TurnFaceDown();
            Assert.That(stack.StackedCount == 2);
            stack.Play(Card.Any);
            Assert.That(stack.StackedCount == 2);
        }

        [Test]
        public void TestIsActivating()
        {
            Stack stack = new();
            var suit = Suit.Any;
            var rank = Rank.AnyNumber;
            Assert.That(!stack.IsPlayActivating(new Card(suit, rank)));
            Assert.That(!stack.IsPlayActivating(new Card(suit, Rank.AnyFace)));
            stack.Play(new Card(suit, rank));
            Assert.That(stack.IsPlayActivating(new Card(suit, Rank.AnyFace)));
        }

        [Test]
        public void TestIsStacking()
        {
            Stack stack = new();
            var suit = Suit.Any;
            var rank = Rank.GetAny(_arExcluding: new Rank[] { Rank.Ace });
            Assert.That(!stack.IsPlayStacking(new Card(suit, rank)));
            stack.Play(new Card(suit, rank));
            Assert.That(stack.IsPlayStacking(new Card(suit, Rank.Ace)));
        }

        [Test]
        public void TestPlay()
        {
            Stack stack = new();
            var suit = Suit.Any;
            var rank = Rank.GetAny(_arExcluding: new Rank[] { Rank.Ace });
            var card_0 = new Card(suit, rank);
            var card_1 = new Card(suit, Rank.Ace);
            stack.Play(card_0);
            Assert.That(stack.TryGetFaceUpTopCard(out Card topCardAny) && topCardAny.Suit == suit && topCardAny.Rank == rank);
            stack.Play(card_1);
            Assert.That(stack.TryGetFaceUpTopCard(out Card topCardAce) && topCardAce.Suit == suit && topCardAce.Rank == Rank.Ace);
        }

        [Test]
        public void TestTurnFaceDown()
        {
            Stack stack = new();
            Assert.That(!stack.IsFaceDown);
            stack.Play(new Card(Suit.Any, Rank.Any));
            Assert.That(!stack.IsFaceDown);
            stack.TurnFaceDown();
            Assert.That(stack.IsFaceDown);
        }

        [Test]
        public void TestGather()
        {
            Stack stack = new();
            Assert.That(stack.IsEmpty);
            Assert.That(stack.Gather().Count == 0);
            Assert.That(stack.IsEmpty);

            Card card = Card.Any;
            List<Card> liGathered;
            stack.Play(card);
            Assert.That(!stack.IsEmpty);
            liGathered = stack.Gather();
            Assert.That(liGathered.Count == 1);
            Assert.That(liGathered.Contains(card));
            Assert.That(stack.IsEmpty);

            Card cardAce = new Card(card.Suit, Rank.Ace);
            Card cardOther = Card.Any;
            liGathered.Clear();
            stack.Play(card);
            Assert.That(!stack.IsEmpty);
            stack.Play(cardAce);
            stack.TurnFaceDown();
            stack.Play(cardOther);
            liGathered = stack.Gather();
            Assert.That(liGathered.Count == 3);
            Assert.That(liGathered.Contains(card));
            Assert.That(liGathered.Contains(cardAce));
            Assert.That(liGathered.Contains(cardOther));
            Assert.That(stack.IsEmpty);
            Assert.That(!stack.IsFaceDown);
        }
    }
}
