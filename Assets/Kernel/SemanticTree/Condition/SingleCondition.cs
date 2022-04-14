using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Condition
{
    public abstract class SingleCondition : ICondition
    {
        public abstract void Construct();
        public abstract bool Value { get; }
    }
}
