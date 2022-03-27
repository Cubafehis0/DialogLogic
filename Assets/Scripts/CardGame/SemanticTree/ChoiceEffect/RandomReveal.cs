using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomReveal : ChoiceEffect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression = null;
        private IExpression num = null;

        [XmlElement(ElementName = "speech_type")]
        public SpeechType? SpeechType = null;

        public override void Execute()
        {
            if (SpeechType.HasValue) Context.PlayerContext.RandomReveal(SpeechType.Value, num.Value); 
            else Context.PlayerContext.RandomReveal(num.Value);
        }

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
