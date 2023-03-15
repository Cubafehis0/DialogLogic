using UnityEngine;

namespace Assets.Kernel.Relics.Implements
{
    public class BlurtOutRelic : Relic
    {
        BlurtOutRelic()
        {
            Name = "blurt_out";
            Title = "脱口而出";
            Description = "每回合开始时随机激活一张手牌";
        }

        public override object Clone()
        {
            return new BlurtOutRelic(); 
        }

        public override void OnTurnStart()
        {
            GameConsole.Instance.RandomActivateHand();
        }
    }
}
