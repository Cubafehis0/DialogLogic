using ExpressionAnalyser;
using SemanticTree;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    public class GUISelectHand : Effect
    {
        [XmlElement(ElementName ="action")]
        public EffectList Actions { get; set; }

        [XmlIgnore]
        public ICondition condition;

        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }

        private IExpression num;

        public override void Construct()
        {
            num = ExpressionAnalyser.ExpressionParser.AnalayseExpression(NumExpression);
            Actions.Construct();
        }

        public override void Execute()
        {
            CardGameManager.Instance.OpenHandChoosePanel(condition, num.Value, Actions);
        }
    }
}
