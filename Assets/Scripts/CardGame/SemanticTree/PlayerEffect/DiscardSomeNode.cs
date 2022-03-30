using ExpressionAnalyser;
using SemanticTree.CardEffects;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class DiscardSomeHand : Effect
    {

        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression num;

        public override void Execute()
        {
            CardGameManager.Instance.OpenHandChoosePanel(null, num.Value, new DiscardCard());
        }

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
