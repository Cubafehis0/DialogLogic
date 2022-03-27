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
    public class AnyCard : ICondition
    {
        private readonly ICondition condition = null;
        public bool Value
        {
            get
            {
                foreach (Card card in Context.PileContext)
                {
                    Context.PushCardContext(card);
                    if (condition.Value) return true;
                    Context.PopCardContext();
                }
                return false;
            }
        }
    }
}
