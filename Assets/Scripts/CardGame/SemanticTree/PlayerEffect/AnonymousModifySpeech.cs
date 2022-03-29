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
        public SpeechArt Modifier;

        [XmlElement(ElementName = "duration")]
        public int Timer;

        public override void Construct()
        {

        }

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousSpeechModifer(Modifier, Timer);
        }
    }
}
