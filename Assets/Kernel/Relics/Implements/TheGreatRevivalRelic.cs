namespace Assets.Kernel.Relics.Implements
{
    public class TheGreatRevivalRelic : Relic
    {
        TheGreatRevivalRelic()
        {
            Name = "the_great_revival";
            Title = "卧薪尝胆";
            Description = "回合结束时，所有手牌增加1点“打出”时的数值效果";
        }

        public override object Clone()
        {
            return new TheGreatRevivalRelic();
        }
    }
}
