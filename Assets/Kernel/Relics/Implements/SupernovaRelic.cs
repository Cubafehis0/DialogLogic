namespace Assets.Kernel.Relics.Implements
{
    public class SupernovaRelic : Relic
    {
        SupernovaRelic()
        {
            Name = "supernova";
            Title = "超新星";
            Description = "frag强势补正+5，frag无忌补正+5，frag激情补正+5，frag道德补正+5，frag逻辑补正+5，frag迂回补正+5";
        }

        public override object Clone()
        {
            return new SupernovaRelic();
        }
    }
}
