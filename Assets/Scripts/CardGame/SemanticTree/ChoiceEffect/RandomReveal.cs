using MyExpressionParse;
using SemanticTree.Expression;
using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.ChoiceEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class RandomRevealNode : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        [XmlElement(ElementName = "speech_type")]
        public SpeechType? SpeechType { get; set; }
        private IExpression exp;

        public RandomRevealNode()
        {
            NumExpression = "";
        }

        public RandomRevealNode(SpeechType? speechType, IExpression exp)
        {
            this.SpeechType = speechType;
            this.exp = exp;
        }

        public RandomRevealNode(XmlNode xmlNode)
        {
            //有缺陷
            exp = MyExpressionParse.ExpressionParser.AnalayseExpression(xmlNode.InnerText);
        }

        public override void Execute()
        {
            if (SpeechType == null) Context.PlayerContext.RandomReveal(exp.Value);
            else Context.PlayerContext.RandomReveal(SpeechType.Value, exp.Value);
        }

        public override void Construct()
        {
            exp = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
