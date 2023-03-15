namespace Assets.Kernel.Relics.Implements
{
    public class RestRelic : Relic
    {
        RestRelic()
        {
            Name = "rest";
            Title = "歇息";
            Description = "每洗一次牌，内视增加3";
        }

        public override object Clone()
        {
           return new RestRelic();
        }

        public override void OnDiscard2Draw()
        {
            GameConsole.Instance.ModifyPersonality("", new ModdingAPI.Personality(0, 0, 0, 3), -1, ModdingAPI.DMGType.Normal);
        }
    }
}
