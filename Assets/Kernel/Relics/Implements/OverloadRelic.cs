namespace Assets.Kernel.Relics.Implements
{
    public class OverloadRelic : Relic
    {
        OverloadRelic()
        {
            Name = "overload";
            Title = "超负荷运转";
            Description = "每一张卡牌占2个卡位，打出一张牌后这一回合保留这张牌“持有”效果";
        }

        public override object Clone()
        {
            return new OverloadRelic();
        }
    }
}
