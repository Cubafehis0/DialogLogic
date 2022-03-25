using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// Expression参数num
    /// </summary>
    public class ModifyHealth : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }

        private IExpression num;

        public ModifyHealth()
        {
            NumExpression = "";
            this.num = null;
        }

        public ModifyHealth(XmlNode xmlNode)
        {

            num = ExpressionAnalyser.ExpressionParser.AnalayseExpression(xmlNode.InnerText);
        }

        public override void Construct()
        {
            num = ExpressionAnalyser.ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.Player.Health += num.Value;
        }
    }
}
