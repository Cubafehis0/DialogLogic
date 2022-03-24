using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Expression
{
    /// <summary>
    /// 未完成
    /// </summary>
    public class CountCardNode : IExpression
    {
        private readonly ICondition condition;

        public CountCardNode(ICondition condition)
        {
            this.condition = condition;
        }

        public int Value
        {
            get
            {
                int ret = 0;
                foreach (Card card in Context.PileContext)
                {
                    Context.PushCardContext(card);
                    if (condition?.Value ?? true) ret++;
                    Context.PopCardContext();
                }
                return ret;
            }
        }
    }
}
