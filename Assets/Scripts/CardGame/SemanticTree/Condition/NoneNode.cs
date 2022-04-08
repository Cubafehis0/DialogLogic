using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class NoneNode : ComplexCondition
    {
        public override bool Value
        {
            get
            {
                foreach (var condition in conditionsList)
                {
                    if (condition.Value)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
