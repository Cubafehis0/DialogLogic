using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticTree.CardEffects
{
    /// <summary>
    /// 无参数
    /// </summary>
    public class ExecuteCard : CardEffect
    {
        public override void Construct() { }

        public override void Execute()
        {
            Context.CardContext.info.Effects.Execute();
        }
    }
}
