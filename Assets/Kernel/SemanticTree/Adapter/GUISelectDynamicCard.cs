using ExpressionAnalyser;
using SemanticTree.Condition;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SemanticTree.Adapter
{
    public class GUISelectDynamicCard : Effect
    {
        [XmlElement(ElementName = "include")]
        public PileType type = 0;

        [XmlElement(ElementName = "num")]
        public string NumExpression = "1";
        private IExpression num;

        [XmlElement(ElementName = "condition")]
        public AllNode Condition = null;

        [XmlElement(ElementName = "action")]
        public EffectList Actions = null;


        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
            Condition?.Construct();
            Actions?.Construct();
        }

        public override void Execute()
        {
            List<Card> candidate = new List<Card>();
            if ((PileType.Hand & type) > 0) candidate.AddRange(Context.PlayerContext.Hand);
            if ((PileType.DrawDeck & type) > 0) candidate.AddRange(Context.PlayerContext.DrawPile);
            if ((PileType.DiscardDeck & type) > 0) candidate.AddRange(Context.PlayerContext.DiscardPile);
            List<Card> res = new List<Card>();
            foreach (Card card in candidate)
            {
                Context.PushCardContext(card);
                if (Condition?.Value ?? true) res.Add(card);
                Context.PopCardContext();
            }
            GUISystemManager.Instance.OpenPileChoosePanel(res, num.Value, Actions);
        }
    }
}
