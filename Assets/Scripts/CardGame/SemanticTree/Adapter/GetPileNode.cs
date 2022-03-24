using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XmlParser;

namespace SemanticTree.Adapter
{

    /// <summary>
    /// effect
    /// enum未处理
    /// </summary>
    public class GetPileNode : Effect
    {
        private int type = 0;
        private EffectList effects;

        public GetPileNode(int type, EffectList effect)
        {
            this.effects = effect;
            this.type = type;
        }

        public GetPileNode(XmlNode xmlNode)
        {
            if (!xmlNode.HasChildNodes) throw new SemanticException();
            type = xmlNode.Name switch
            {
                "hand" => PileType.Hand,
                "draw_deck" => PileType.DrawDeck,
                "discard_deck" => PileType.DiscardDeck,
                "card_set" => PileType.All,
                _ => throw new SemanticException(),
            };
            effects = SemanticAnalyser.AnalayseEffectList(xmlNode);
        }

        public override void Execute()
        {
            List<Card> ret = new List<Card>();
            if ((PileType.Hand & type) > 0) ret.AddRange(Context.PlayerContext.Hand);
            if ((PileType.DrawDeck & type) > 0) ret.AddRange(Context.PlayerContext.DrawPile);
            if ((PileType.DiscardDeck & type) > 0) ret.AddRange(Context.PlayerContext.DiscardPile);
            Context.PushPileContext(ret);
            effects.Execute();
            Context.PopPileContext();
        }

        public override void Construct()
        {
            throw new System.NotImplementedException();
        }
    }
}
