using System.Collections.Generic;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class AnyNode : ICondition
    {
        [XmlElement]
        public List<ICondition> conditions;

        public bool Value
        {
            get
            {
                foreach (var condition in conditions)
                {
                    if (condition.Value)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
