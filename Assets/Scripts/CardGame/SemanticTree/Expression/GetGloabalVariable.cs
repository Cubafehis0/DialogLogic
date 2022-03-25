using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Expression
{
    public class GetGlobalVariable : IExpression
    {
        //待修改
        private static readonly Dictionary<string, int> variableDictionary = new Dictionary<string, int>();
        private readonly string name;
        public GetGlobalVariable(string name)
        {
            this.name = name;
        }
        public int Value
        {
            get
            {
                return variableDictionary.ContainsKey(name) ? variableDictionary[name] : 0;
            }
        }
    }
}
