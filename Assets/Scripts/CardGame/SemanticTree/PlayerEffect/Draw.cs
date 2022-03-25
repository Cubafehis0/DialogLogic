using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class Draw : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }

        private IExpression num;

        public Draw()
        {
            NumExpression = "";
        }

        public Draw(int num)
        {
            this.num = new ConstNode(num);
        }

        public Draw(XmlNode xmlNode)
        {
            NumExpression = xmlNode.InnerText;
            Construct();
        }

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.Draw((uint)num.Value);
        }
    }
}
