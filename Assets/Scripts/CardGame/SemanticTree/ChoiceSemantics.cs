using System.Collections;
using UnityEngine;

namespace SemanticTree
{
    namespace ChoiceSemantics
    {
        public interface IChoiceSlotEffectNode
        {
            void Execute(ChoiceSlot slot);
        }

        public class GUISelectCurrentSlot : IEffectNode
        {
            private readonly IChoiceSlotEffectNode action;

            public GUISelectCurrentSlot(IChoiceSlotEffectNode action)
            {
                this.action = action;
            }

            public void Execute()
            {
                CardGameManager.Instance.OpenSlotSelectPanel(action);
            }
        }

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
}
