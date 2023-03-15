using UnityEngine;

namespace Assets.Kernel.Relics.Implements
{
    public class AmassRelic : Relic
    {
        AmassRelic()
        {
            Name = "amass";
            Title = "积水成河";
            Description = "不消耗行动点的卡牌”打出“的数值效果+4";
        }

        public override object Clone()
        {
            return new AmassRelic();
        }

        public override void OnPlayCard(Card card)
        {
            if (GameQuery.Instance.GetCardCost(card.id) == 0)
            {
                GameConsole.Print("不消耗行动点的卡牌”打出“的数值效果+4");
            }
        }
    }
}
