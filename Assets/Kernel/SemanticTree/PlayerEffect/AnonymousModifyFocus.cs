using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AnonymousModifyFocus : Effect
    {
        [XmlElement(ElementName = "type")]
        public SpeechType modifier;
        [XmlElement(ElementName = "duration")]
        public string DurationExpression;
        private IExpression duration;
        public override void Construct()
        {
            duration = ExpressionParser.AnalayseExpression(DurationExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousFocusModifer(modifier, duration.Value);
        }
    }
}
