using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    /// <summary>
    /// 
    /// </summary>
    public class AllChoiceRandomReveal : ChoiceEffect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression = null;
        private IExpression num = null;

        [XmlElement(ElementName = "speech_type")]
        public SpeechType SpeechType = SpeechType.Normal;
        [XmlIgnore]
        public bool SpeechTypeSpecified = false;

        public override void Execute()
        {
            if (SpeechTypeSpecified) Context.PlayerContext.RandomReveal(SpeechType, num.Value);
            else Context.PlayerContext.RandomReveal(num.Value);
        }

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
