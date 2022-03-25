using ExpressionAnalyser;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SemanticTree.Adapter
{
    public class GUISelectDynamicCard : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression num;

        [XmlElement(ElementName = "condition")]
        public ICondition Condition { get; set; }

        [XmlElement(ElementName = "action")]
        public EffectList Actions { get; set; }

        public GUISelectDynamicCard()
        {
            NumExpression = "";
            Condition = null;
            Actions = new EffectList();
        }

        public GUISelectDynamicCard(ICondition condition, string num, EffectList action)
        {
            Condition = condition;
            NumExpression = num;
            Actions = action;
        }

        public override void Construct()
        {
            num = ExpressionAnalyser.ExpressionParser.AnalayseExpression(NumExpression);
            Actions.Execute();
        }

        public override void Execute()
        {
            List<Card> res = new List<Card>();
            foreach (Card card in Context.PileContext)
            {
                Context.PushCardContext(card);
                if (Condition.Value) res.Add(card);
                Context.PopCardContext();
            }
            CardGameManager.Instance.OpenPileChoosePanel(res, num.Value, Actions);
            //禁用输入
        }
    }
}
