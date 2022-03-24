using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.ChoiceEffect
{
    public class SlotRandomRevealNode : IChoiceSlotEffectNode
    {
        private readonly int num;

        public SlotRandomRevealNode(int num)
        {
            this.num = num;
        }

        public void Execute(ChoiceSlot slot)
        {
            for (int i = 0; i < num; i++)
            {
                PersonalityType? t = slot.PickupARandomUnmasked();
                if (t == null) return;
                slot.AddMask(t.Value);
            }
        }
    }
}
