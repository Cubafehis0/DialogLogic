using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.ChoiceEffect
{
    public interface IChoiceSlotEffectNode
    {
        void Execute(ChoiceSlot slot);
    }
}
