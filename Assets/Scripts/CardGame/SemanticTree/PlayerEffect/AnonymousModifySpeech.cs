using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// Effect
    /// </summary>
    public class AnonymousModifySpeech : Effect
    {
        [XmlElement(ElementName = "Modifer")]
        public SpeechArtExpression Modifier;

        [XmlElement(ElementName = "duration")]
        public string DurationExpression;
        private IExpression duration;
        public override void Construct()
        {
            duration = ExpressionParser.AnalayseExpression(DurationExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousSpeechModifer(Modifier.Value, duration.Value);
        }
    }
}
