namespace Assets.Kernel.Relics.Implements
{
    public class IronmanRelic : Relic
    {
        IronmanRelic()
        {
            Name = "ironman";
            Title = "硬汉做派";
            Description = "frag强势补正+12";
        }

        public override object Clone()
        {
            return new IronmanRelic();
        }
    }
}
