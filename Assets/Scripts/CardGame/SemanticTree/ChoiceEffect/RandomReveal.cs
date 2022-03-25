using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class RandomRevealNode : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression exp;
        [XmlElement(ElementName = "speech_type")]
        public SpeechType? SpeechType { get; set; }


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
            NumExpression=xmlNode.InnerText;
            Construct();
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
