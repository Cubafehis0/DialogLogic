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
            if ((PileType.Hand & type) > 0) candidate.AddRange(Context.PlayerContext.CardController.Hand);
            if ((PileType.DrawDeck & type) > 0) candidate.AddRange(Context.PlayerContext.CardController.DrawPile);
            if ((PileType.DiscardDeck & type) > 0) candidate.AddRange(Context.PlayerContext.CardController.DiscardPile);
            List<Card> res = new List<Card>();
            foreach (Card card in candidate)
            {
                Context.PushCardContext(card);
                if (Condition?.Value ?? true) res.Add(card);
                Context.PopCardContext();
            }
            var msg = new PileSelectGUIContext(res, num.Value, Actions);
            GUISystemManager.Instance.Open("w_select_pile", msg);
        }
    }
}
