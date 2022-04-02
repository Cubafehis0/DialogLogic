using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    public class ChoiceReveal : Effect
    {
        [XmlElement(ElementName = "include")]
        public HashSet<PersonalityType> SpeechTypes;
        public override void Construct()
        {

        }

        public override void Execute()
        {
            var cs = Context.choiceSlotStack.Peek();
            foreach (PersonalityType speechType in SpeechTypes)
            {
                cs.AddMask(speechType);
            }
        }
    }
}
