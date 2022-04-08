using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public enum CompareType
    {
        LT,
        GT,
        LE,
        GE,
        EQ,
        NE,
    }

    public class CountRequirement : SingleCondition
    {

        [XmlElement(ElementName = "compare_type")]
        public CompareType CompareType;

        [XmlElement(ElementName = "value")]
        public int target;

        [XmlElement(ElementName = "requirements")]
        public ComplexCondition conditions;
        public override bool Value
        {
            get
            {
                int count = 0;
                foreach (var condition in conditions.conditionsList)
                {
                    if (condition.Value)
                    {
                        count++;
                    }
                }
                return CompareType switch
                {
                    CompareType.LT => count<target,
                    CompareType.GT => count>target,
                    CompareType.LE => count<=target,
                    CompareType.GE => count>=target,
                    CompareType.EQ => count==target,
                    CompareType.NE => count!=target,
                    _ => throw new SemanticException(),
                };
            }
        }

        public override void Construct()
        {
            conditions.Construct();
        }
    }
}
