using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class AnyNode : ConditionNode
    {
        [XmlElement]
        public ConditionList conditions;

        public override bool Value
        {
            get
            {
                foreach (var condition in conditions.conditions)
                {
                    if (condition.Value)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override void Construct()
        {
            throw new System.NotImplementedException();
        }
    }
}
