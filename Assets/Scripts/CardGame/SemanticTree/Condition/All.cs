using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class AllNode : ICondition
    {
        [XmlElement]
        public List<ICondition> conditions;
        public bool Value
        {
            get
            {
                foreach (var condition in conditions)
                {
                    if (!condition.Value)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
