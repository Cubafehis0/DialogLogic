using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AddCostModifier : Effect
    {
        [XmlElement(ElementName = "duration")]
        public int? Duration { get; set; }

        [XmlElement(ElementName = "name")]
        public string ModifierName { get; set; }

        private CostModifier modifier = null;

        public AddCostModifier()
        {
            Duration = null;
            ModifierName = "";
            modifier = null;
        }


        public AddCostModifier(CostModifier modifier, int? cd)
        {
            this.modifier = modifier;
            this.Duration = cd;
        }

        public AddCostModifier(XmlNode xmlNode)
        {
            modifier = StaticCostModifierLibrary.GetByName(xmlNode.InnerText);
            Duration = null;
        }

        public override void Execute()
        {
            Context.PlayerContext.AddCostModifer(modifier, Duration);
        }

        public override void Construct()
        {
            modifier = StaticCostModifierLibrary.GetByName(ModifierName);
        }
    }
}
