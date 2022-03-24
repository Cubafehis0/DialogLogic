using SemanticTree;
using SemanticTree.Expression;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    public class GUISelectHandNode : Effect
    {
        [XmlElement(ElementName ="action")]
        public EffectList Actions { get; set; }

        [XmlElement(ElementName ="condition")]
        public ICondition condition;

        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }

        private IExpression num;

        public override void Construct()
        {
            num = MyExpressionParse.ExpressionParser.AnalayseExpression(NumExpression);
            Actions.Construct();
        }

        public override void Execute()
        {
            CardGameManager.Instance.OpenHandChoosePanel(condition, num.Value, Actions);
        }
    }
}
