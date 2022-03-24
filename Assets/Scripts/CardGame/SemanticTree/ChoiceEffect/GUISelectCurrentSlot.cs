using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffect
{
    public class GUISelectCurrentSlot : Effect
    {
        [XmlElement(ElementName ="action")]
        public EffectList action;

        public override void Construct()
        {
            action.Construct();
        }

        public override void Execute()
        {
            CardGameManager.Instance.OpenSlotSelectPanel(action);
        }
    }
}
