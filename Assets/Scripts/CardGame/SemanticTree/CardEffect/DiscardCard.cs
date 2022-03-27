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
    public class DiscardCard : CardEffect
    {
        public override void Construct() { }

        public override void Execute()
        {
            Context.PlayerContext.DiscardCard(Context.CardContext);
        }
    }
}
