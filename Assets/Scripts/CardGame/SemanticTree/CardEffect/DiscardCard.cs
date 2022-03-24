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
    public class DiscardCard : CardNode
    {
        public DiscardCard() { }

        public override void Construct() { }

        public override void Execute()
        {
            Context.PlayerContext.DiscardCard(Context.CardContext);
        }
    }
}
