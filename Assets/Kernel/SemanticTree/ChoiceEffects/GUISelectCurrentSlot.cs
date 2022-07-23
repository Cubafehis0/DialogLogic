using SemanticTree.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    public class GUISelectCurrentSlot : ChoiceEffect
    {
        [XmlElement(ElementName = "action")]
        public EffectList action = null;

        public override void Construct()
        {
            action?.Construct();
        }

        public override void Execute()
        {
            GUISystemManager.Instance.OpenSlotSelectPanel(action);
        }
    }
}
