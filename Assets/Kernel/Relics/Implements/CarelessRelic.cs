using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class CarelessRelic : Relic
    {
        CarelessRelic()
        {
            Name = "careless";
            Title = "散漫";
            Description = "每回合开始消耗1点内视，1点外感";
        }

        public override object Clone()
        {
            return new CarelessRelic();
        }

        public override void OnTurnStart()
        {
            GameConsole.Instance.ModifyPersonality("self", new Personality(0, 0, 0, 1),-1, DMGType.Normal);
        }
    }
}
