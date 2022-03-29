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
        public int Timer;


        public override void Construct()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousFocusModifer(modifier, Timer);
        }
    }
}
