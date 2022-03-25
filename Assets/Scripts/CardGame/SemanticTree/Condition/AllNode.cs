using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class AllNode : ConditionNode
    {
        [XmlElement]
        public ConditionList conditions;
        public override bool Value
        {
            get
            {
                foreach (var condition in conditions.conditions)
                {
                    if (!condition.Value)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public override void Construct()
        {
            conditions.Construct();
        }
    }
}
