using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class RemoveCostModifier : Effect
    {
        [XmlElement(ElementName = "name")]
        public string ModifierName { get; set; }

        private CostModifier modifier = null;

        public RemoveCostModifier()
        {
            ModifierName = "";
        }
        public RemoveCostModifier(CostModifier modifier)
        {
            this.modifier = modifier;
        }
        public RemoveCostModifier(XmlNode xmlNode)
        {
            ModifierName = xmlNode.InnerText;
            Construct();
        }
        public override void Execute()
        {
            Context.PlayerContext.RemoveCostModifer(modifier);
        }

        public override void Construct()
        {
            modifier = StaticCostModifierLibrary.GetByName(ModifierName);
        }
    }
}
