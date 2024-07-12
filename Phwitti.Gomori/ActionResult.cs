using Phwitti.PlayingCards;
using System.Collections.Generic;

namespace Phwitti.Gomori
{
    public class ActionResult
    {
        private bool m_bShouldPlayAnotherCard;
        private List<Card> m_liGathered;

        //

        public bool ShouldPlayAnotherCard
            => m_bShouldPlayAnotherCard;

        public IReadOnlyList<Card> Gathered
            => m_liGathered;

        //

        public ActionResult(bool _shouldPlayAnotherCard, List<Card> _gathered)
        {
            m_bShouldPlayAnotherCard = _shouldPlayAnotherCard;
            m_liGathered = _gathered;
        }
    }
}
