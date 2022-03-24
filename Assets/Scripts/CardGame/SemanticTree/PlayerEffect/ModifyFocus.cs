using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class ModifyFocusNode : Effect
    {
        [XmlElement(ElementName = "type")]
        public SpeechType modifier;
        [XmlElement(ElementName = "duration")]
        public int? Timer { get; set; }

        public ModifyFocusNode()
        {
            modifier = SpeechType.Normal;
            Timer = null;
        }

        public ModifyFocusNode(SpeechType modifier, int? timer)
        {
            this.modifier = modifier;
            this.Timer = timer;
        }

        public ModifyFocusNode(XmlNode xmlNode)
        {
            modifier = SpeechType.Normal;
            Timer = null;
            if (!xmlNode.HasChildNodes) throw new SemanticException();
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "normal":
                        modifier = SpeechType.Normal;
                        break;
                    case "cheat":
                        modifier = SpeechType.Cheat;
                        break;
                    case "threat":
                        modifier = SpeechType.Threaten;
                        break;
                    case "persuade":
                        modifier = SpeechType.Persuade;
                        break;
                    case "last_turn":
                        Timer = int.Parse(xml.InnerText);
                        break;
                    default:
                        throw new SemanticException();
                };
                xml = xml.NextSibling;
            }
        }

        public override void Construct()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            Context.PlayerContext.AddFocusModifer(modifier, Timer);
        }
    }
}
