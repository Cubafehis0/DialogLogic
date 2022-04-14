using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    public class ChoiceRevealRandom : Effect
    {
        [XmlElement(ElementName ="num")]
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
            for(int i=0;i< num; i++)
            {
                if (cs.PickupARandomUnmasked() == null) return;
                cs.AddMask(cs.PickupARandomUnmasked().Value);
            }
        }
    }
}
