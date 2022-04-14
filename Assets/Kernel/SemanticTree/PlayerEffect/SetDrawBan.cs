using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class SetDrawBan : Effect
    {
        [XmlElement(ElementName = "value")]
        public bool Value { get; set; }

        public SetDrawBan()
        {
            Value = true;
        }

        public SetDrawBan(bool value)
        {
            this.Value = value;
        }

        public SetDrawBan(XmlNode xmlNode)
        {
            Value = bool.Parse(xmlNode.InnerText);
        }

        public override void Construct()
        {

        }

        public override void Execute()
        {
            Context.PlayerContext.DrawBan = Value;
        }
    }
}
