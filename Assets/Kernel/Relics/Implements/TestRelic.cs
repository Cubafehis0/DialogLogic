using UnityEngine;

namespace Assets.Kernel.Relics.Implements
{
    public class TestRelic : Relic
    {
        TestRelic()
        {
            Name = "test";
            Title = "测试";
            Description = "测试输出";
        }

        public override object Clone()
        {
            return new TestRelic();
        }

        public override void OnBattleStart()
        {
            Debug.Log("测试遗物：战斗开始");
        }

        public override void OnDiscard(Card card)
        {
            Debug.Log("测试遗物：丢弃卡牌");
        }

        public override void OnDiscard2Draw()
        {
            Debug.Log("测试遗物：洗牌");
        }

        public override void OnDraw(Card card)
        {
            Debug.Log("测试遗物：抽牌");
        }

        public override void OnEnergyChange()
        {
            Debug.Log("测试遗物：能量改变");
        }

        public override void OnHealthChange()
        {
            Debug.Log("测试遗物：生命改变");
        }

        public override void OnLoseThis()
        {
            Debug.Log("测试遗物：失去遗物");
        }

        public override void OnPersonalityChange()
        {
            Debug.Log("测试遗物：人格改变");
        }

        public override void OnPickUp()
        {
            Debug.Log("测试遗物：拾起");
        }

        public override void OnPlayCard(Card card)
        {
            Debug.Log("测试遗物：打出卡牌");
        }

        public override void OnTurnEnd()
        {
            Debug.Log("测试遗物：回合结束");
        }

        public override void OnTurnStart()
        {
            Debug.Log("测试遗物：回合开始");
        }

        public override void OnVictory()
        {
            Debug.Log("测试遗物：胜利");
        }
    }
}
