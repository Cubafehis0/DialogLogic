using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class DiscardAllHand : Effect
    {
        public override void Construct()
        {

        }

        public override void Execute()
        {
            List<Card> cards = new List<Card>(Context.PlayerContext.Hand);
            foreach (Card card in cards)
            {
                Context.PlayerContext.DiscardCard(card);
            }
        }
    }
}
