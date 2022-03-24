using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Expression
{
    public interface ITreeNode { }
    public interface IExpression : ITreeNode
    {
        int Value { get; }
    }
}
