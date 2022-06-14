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

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.CardController.Draw(num.Value);
        }
    }
}
