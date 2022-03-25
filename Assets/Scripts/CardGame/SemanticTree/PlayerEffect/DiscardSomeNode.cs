using ExpressionAnalyser;
using SemanticTree.CardEffect;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class DiscardSomeHandNode : Effect
    {

        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression num;
        public DiscardSomeHandNode() { }
        public DiscardSomeHandNode(XmlNode xmlNode)
        {
            num = ExpressionParser.AnalayseExpression(xmlNode.InnerText);
        }

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
