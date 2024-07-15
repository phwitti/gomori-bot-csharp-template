using Phwitti.PlayingCards;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori
{
    public interface IReadOnlyHand
    {
        Card? GetCardAtIndex(PlayerCardIndex _ePlayerCardIndex);
        bool LooksAlike(IReadOnlyHand _other);
    }

    public class Hand : PlayingCards.Hand,
        IReadOnlyHand
    {
        public new IReadOnlyList<Card> Cards
            => base.Cards.Where(x => x is Card).Select(x => (x as Card?)!.Value ).ToList();

        //

        public Hand()
            : base(Rules.CardsOnHand)
        {
        }

        public Hand(Hand _other)
            : base(_other)
        {
        }

        public Hand(IEnumerable<ICard> _liCards)
            : base(Rules.CardsOnHand, _liCards)
        {
        }

        // IReadOnlyHand
        public Card? GetCardAtIndex(PlayerCardIndex _ePlayerCardIndex)
        {
            return this.TryGetCard(GetIndexForPlayerCardIndex(_ePlayerCardIndex), out ICard? card)
                && card is Card playingCard
                    ? playingCard
                    : null;
        }

        // IReadOnlyHand
        public bool LooksAlike(IReadOnlyHand _other)
        {
            return this.GetCardAtIndex(PlayerCardIndex.First) == _other.GetCardAtIndex(PlayerCardIndex.First)
                && this.GetCardAtIndex(PlayerCardIndex.Second) == _other.GetCardAtIndex(PlayerCardIndex.Second)
                && this.GetCardAtIndex(PlayerCardIndex.Third) == _other.GetCardAtIndex(PlayerCardIndex.Third)
                && this.GetCardAtIndex(PlayerCardIndex.Fourth) == _other.GetCardAtIndex(PlayerCardIndex.Fourth)
                && this.GetCardAtIndex(PlayerCardIndex.Fifth) == _other.GetCardAtIndex(PlayerCardIndex.Fifth);
        }

        //

        private static int GetIndexForPlayerCardIndex(PlayerCardIndex _ePlayerCardIndex)
        {
            return _ePlayerCardIndex switch
            {
                PlayerCardIndex.First => 0,
                PlayerCardIndex.Second => 1,
                PlayerCardIndex.Third => 2,
                PlayerCardIndex.Fourth => 3,
                PlayerCardIndex.Fifth => 4,
                _ => throw new System.InvalidOperationException()
            };
        }
    }
}
