using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    public class ChoiceRevealAll : Effect
    {
        public override void Construct()
        {
           
        }

        public override void Execute()
        {
            var cs = Context.choiceSlotStack.Peek();
            for (int i = 0; i < 10; i++)
            {
                if (cs.PickupAllUnmasked() == null) return;
                var p = cs.PickupAllUnmasked();
                foreach (var c in p)
                    cs.AddMask(c);
            }
        }
    }
}
