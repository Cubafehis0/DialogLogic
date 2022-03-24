using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.Adapter
{
    /// <summary>
    /// 未完成
    /// </summary>
    public class AllCard : ICondition
    {
        private readonly ICondition condition;

        public AllCard(ICondition condition)
        {
            this.condition = condition;
        }

        public bool Value
        {
            get
            {
                foreach (Card card in Context.PileContext)
                {
                    Context.PushCardContext(card);
                    if (!condition.Value) return false;
                    Context.PopCardContext();
                }
                return true;
            }
        }
    }
}
