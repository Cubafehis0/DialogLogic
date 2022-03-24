using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class DiscardAllHand : Effect
    {
        public DiscardAllHand() { }
        public DiscardAllHand(XmlNode xmlNode)
        {
            if (!xmlNode.Name.Equals("discard_all")) throw new SemanticException();
            if (xmlNode.HasChildNodes) throw new SemanticException();
        }

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
