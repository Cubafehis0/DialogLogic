namespace Assets.Kernel.Relics.Implements
{
    public class HurriedRelic : Relic
    {
        HurriedRelic()
        {
            Name = "hurried";
            Title = "匆忙";
            Description = "每消耗一点内视，抽取一张牌";
        }

        public override object Clone()
        {
            return new HurriedRelic();
        }

        public override void OnPersonalityChange()
        {
            GameConsole.Instance.Draw("self",1);
        }
    }
}
