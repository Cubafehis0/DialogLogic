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
        public string NumExpression;
        private IExpression num;
        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            Context.Console.Damage(Context.PlayerContext.ToString(), num.Value);
        }
    }
}
