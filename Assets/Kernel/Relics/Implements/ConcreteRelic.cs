using UnityEngine;

namespace Assets.Kernel.Relics.Implements
{
    public class ConcreteRelic : Relic
    {
        public ConcreteRelic()
        {
            Name = "concrete";
            Title = "具现化";
            Description = "每过n回合后，你这回合获取的所有对话判定效果的数值翻倍";
            Rarity = 1;
            Category = ModdingAPI.PersonalityType.Logic;
            InitNum = 0;
            counter = 0;
            modifier = null;
        }

        public override object Clone()
        {
            return new ConcreteRelic();
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();
            counter++;
            if (counter == 5)
            {
                GameConsole.Instance.AddStatus("self", "双倍判定buff", 1);
                counter = 0;
            }
        }
    }
}
