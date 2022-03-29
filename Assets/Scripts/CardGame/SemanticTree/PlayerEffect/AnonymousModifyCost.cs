using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AnonymousModifyCost : Effect
    {
        [XmlElement(ElementName = "duration")]
        public int Duration;

        [XmlElement(ElementName = "name")]
        public CostModifier Modifier;

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousCostModifer(Modifier, Duration);
        }

        public override void Construct()
        {
            Modifier.Construct();
        }
    }
}
