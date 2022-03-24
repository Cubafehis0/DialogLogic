using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Expression
{
    /// <summary>
    /// expression
    /// </summary>
    public class GetStatusCounterValue : IExpression
    {
        private string name;

        public GetStatusCounterValue(string name)
        {
            this.name = name;
        }

        public int Value
        {
            get => Context.PlayerContext.StatusManager.GetStatusValue(name);
        }
    }
}
