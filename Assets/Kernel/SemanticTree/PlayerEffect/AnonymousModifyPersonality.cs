using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{


    public class AnonymousModifyPersonality : Effect
    {
        [XmlElement(ElementName = "personality")]
        public PersonalityExpression Modifier;

        [XmlElement(ElementName = "duration")]
        public int Duration = -1;
        [XmlIgnore]
        public bool DurationSpecified { get => Duration != -1; }

        [XmlElement(ElementName = "dmg_type")]
        public DMGType DMGType = DMGType.Normal;
        [XmlIgnore]
        public bool DMGTypeSpecified { get => DMGType != DMGType.Normal; }




        public override void Execute()
        {
            Context.PlayerContext.AddAnonymousPersonalityModifier(Modifier.Value, Duration,DMGType);
        }

        public override void Construct()
        {
            Modifier.Construct();
        }
    }
}
