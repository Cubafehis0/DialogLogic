using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree
{
    public abstract class ConditionNode : ICondition
    {
        public abstract void Construct();
        public abstract bool Value { get; }
    }
}
