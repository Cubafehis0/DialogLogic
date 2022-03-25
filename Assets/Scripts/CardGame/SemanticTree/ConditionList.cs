using SemanticTree.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    public abstract class ConditionList
    {
        [XmlElement(typeof(string), ElementName = "requirement")]
        [XmlElement(typeof(AllNode), ElementName = "all")]
        [XmlElement(typeof(AnyNode), ElementName = "any")]
        [XmlElement(typeof(NoneNode), ElementName = "none")]
        [XmlElement(typeof(CountRequirement), ElementName = "count_requirement")]
        public List<object> conditions = new List<object>();

        [XmlIgnore]
        public List<ICondition> conditionsList = new List<ICondition>();
        public void Construct()
        {
            foreach (var condition in conditions)
            {
                if (condition is ICondition complexCondition)
                {
                    conditionsList.Add(complexCondition);
                }
                else if (condition is string stringCondition)
                {
                    var node = new StringCondition { ConditionExpression = stringCondition };
                    node.Construct();
                    conditionsList.Add(node);
                }
                else throw new SemanticException();
            }
        }
        public abstract bool Value{get; } 
    }
}
