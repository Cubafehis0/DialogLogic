using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.CardEffect
{
    /// <summary>
    /// 无参数
    /// </summary>
    public class ExecuteCard : CardNode
    {
        public ExecuteCard() { }

        public override void Construct() { }

        public override void Execute()
        {
            Context.CardContext.effectNode.Execute();
        }
    }
}
