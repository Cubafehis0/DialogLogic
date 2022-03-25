using SemanticTree.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    public class ConditionList
    {
        [XmlElement(typeof(StringCondition),ElementName ="requirement")]
        [XmlElement(typeof(AllNode),ElementName ="all")]
        [XmlElement(typeof(AnyNode), ElementName = "any")]
        [XmlElement(typeof(NoneNode), ElementName = "none")]
        [XmlElement(typeof(CountRequirement), ElementName = "count_requirement")]
        public List<ConditionNode> conditions = new List<ConditionNode>();

        public void Construct()
        {
            conditions.ForEach(x => x.Construct());
        }


        public static implicit operator ConditionList(ConditionNode condition)
        {
            return new ConditionList { conditions = new List<ConditionNode> { condition } };
        }
    }
}
