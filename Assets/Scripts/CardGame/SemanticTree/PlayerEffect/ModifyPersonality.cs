using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// Effect
    /// 有缺陷,只能常数
    /// Personality参数
    /// </summary>
    public class ModifyPersonalityNode : Effect
    {
        [XmlElement(ElementName = "personality")]
        public Personality Modifier { get; set; }

        [XmlElement(ElementName = "duration")]
        public int? Timer { get; set; }

        public ModifyPersonalityNode()
        {
            Modifier = null;
            Timer = null;
        }

        public ModifyPersonalityNode(Personality modifier, int? timer)
        {
            this.Modifier = modifier;
            this.Timer = timer;
        }

        public ModifyPersonalityNode(XmlNode xmlNode)
        {
            Modifier = new Personality();
            Timer = null;
            if (!xmlNode.HasChildNodes) throw new SemanticException();
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "inside":
                        Modifier.Inner = int.Parse(xml.InnerText);
                        break;
                    case "outside":
                        Modifier.Outside = int.Parse(xml.InnerText);
                        break;
                    case "logic":
                        Modifier.Logic = int.Parse(xml.InnerText);
                        break;
                    case "spritial":
                        Modifier.Spritial = int.Parse(xml.InnerText);
                        break;
                    case "moral":
                        Modifier.Moral = int.Parse(xml.InnerText);
                        break;
                    case "unethic":
                        Modifier.Immoral = int.Parse(xml.InnerText);
                        break;
                    case "detour":
                        Modifier.Roundabout = int.Parse(xml.InnerText);
                        break;
                    case "strong":
                        Modifier.Aggressive = int.Parse(xml.InnerText);
                        break;
                    case "last_turn":
                        Timer = int.Parse(xml.InnerText);
                        break;
                };
                xml = xml.NextSibling;
            }
        }

        public override void Execute()
        {
            Context.PlayerContext.AddPersonalityModifer(Modifier, Timer);
        }

        public override void Construct()
        {

        }
    }
}
