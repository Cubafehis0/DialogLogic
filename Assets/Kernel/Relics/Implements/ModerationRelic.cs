namespace Assets.Kernel.Relics.Implements
{
    public class ModerationRelic : Relic
    {
        ModerationRelic()
        {
            Name = "moderation";
            Title = "中庸";
            Description = "当一张卡脱离卡位后紧接着另一张对立倾向的卡脱离卡位，可使这一回合内的这对倾向数值变为0";
        }

        public override object Clone()
        {
            return new ModerationRelic();
        }
    }
}
