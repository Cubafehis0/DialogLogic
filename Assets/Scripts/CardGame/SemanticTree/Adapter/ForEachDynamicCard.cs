using SemanticTree.GlobalEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.Adapter
{
    /// <summary>
    /// effect
    /// </summary>
    public class ForEachDynamicCard : Effect
    {
        [XmlElement(ElementName = "include")]
        public PileType type = 0;
        [XmlElement(ElementName = "potantial")]
        public List<IF> actions;


        public override void Execute()
        {
            List<Card> ret = new List<Card>();
            if ((PileType.Hand & type) > 0) ret.AddRange(Context.PlayerContext.Hand);
            if ((PileType.DrawDeck & type) > 0) ret.AddRange(Context.PlayerContext.DrawPile);
            if ((PileType.DiscardDeck & type) > 0) ret.AddRange(Context.PlayerContext.DiscardPile);
            foreach(Card card in ret)
            {
                Context.PushCardContext(card);
                actions.ForEach(x => x.Execute());
                Context.PopCardContext();
            }
        }

        public override void Construct()
        {
            actions.ForEach((x) => x.Construct());
        }
    }
}
