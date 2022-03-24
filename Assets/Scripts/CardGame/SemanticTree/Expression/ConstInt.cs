using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Expression
{
    public class ConstInt : IExpression
    {
        public ConstInt(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
    }
}
