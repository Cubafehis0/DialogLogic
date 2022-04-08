using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class AllNode : ComplexCondition
    {
        public override bool Value
        {
            get
            {
                foreach (var condition in conditionsList)
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
