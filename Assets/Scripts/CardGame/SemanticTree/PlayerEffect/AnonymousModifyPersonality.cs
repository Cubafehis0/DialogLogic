using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// Effect
    /// 有缺陷,只能常数
    /// Personality参数
    /// </summary>
    public class AnonymousModifyPersonality : Effect
    {
        [XmlElement(ElementName = "personality")]
        public PersonalityExpression Modifier;

        [XmlElement(ElementName = "duration")]
        public int duration=-1;
        [XmlIgnore]
        public bool DurationSpecified { get => duration != -1; }
        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousPersonalityModifier(Modifier.Value, duration);
        }

        public override void Construct()
        {
            Modifier.Construct();
        }
    }
}
