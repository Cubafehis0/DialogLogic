using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class LookIntoRelic : Relic
    {
        public LookIntoRelic()
        {
            Name = "look_into";
            Title = "窥视";
            Description = "每回合开始获得1点外感";
            Rarity = 1;
            Category = PersonalityType.Logic;
            InitNum = 0;
            counter = 0;
            modifier = null;
        }

        public override object Clone()
        {
            return new LookIntoRelic();
        }

        public override void OnTurnStart()
        {
            GameConsole.Instance.ModifyPersonality("self", new Personality(0, 0, 0, 0), -1, DMGType.Normal);
        }
    }
}
