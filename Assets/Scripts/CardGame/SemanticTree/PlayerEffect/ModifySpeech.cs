using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// Effect
    /// </summary>
    public class ModifySpeech : Effect
    {
        [XmlElement(ElementName = "Modifer")]
        public SpeechArt Modifier { get; set; }
        [XmlElement(ElementName = "duration")]
        public int? Timer { get; set; }

        public ModifySpeech()
        {
            Modifier = new SpeechArt();
            Timer = null;
        }

        public ModifySpeech(SpeechArt modifier, int? timer)
        {
            this.Modifier = modifier;
            this.Timer = timer;
        }

        public ModifySpeech(XmlNode xmlNode)
        {
            Modifier = new SpeechArt();
            Timer = null;
            if (!xmlNode.HasChildNodes) throw new SemanticException();
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "normal":
                        Modifier[SpeechType.Normal] = int.Parse(xml.InnerText);
                        break;
                    case "cheat":
                        Modifier[SpeechType.Cheat] = int.Parse(xml.InnerText);
                        break;
                    case "threat":
                        Modifier[SpeechType.Threaten] = int.Parse(xml.InnerText);
                        break;
                    case "persuade":
                        Modifier[SpeechType.Persuade] = int.Parse(xml.InnerText);
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

        }

        public override void Execute()
        {
            Context.PlayerContext.AddSpeechModifer(Modifier, Timer);
        }
    }
}
