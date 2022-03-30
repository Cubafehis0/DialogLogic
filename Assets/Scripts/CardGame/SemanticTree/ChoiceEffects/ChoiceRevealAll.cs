using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    internal class ChoiceRevealAll : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression;
        private IExpression numExpression;
        public override void Construct()
        {
            numExpression = ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            var cs = Context.choiceSlotStack.Peek();
            int num = numExpression.Value;
            for (int i = 0; i < num; i++)
            {
                if (cs.PickupAllUnmasked() == null) return;
                var p = cs.PickupAllUnmasked();
                foreach (var c in p)
                    cs.AddMask(c);
            }
        }
    }
}
