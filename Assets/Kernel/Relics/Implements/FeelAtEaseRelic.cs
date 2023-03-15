namespace Assets.Kernel.Relics.Implements
{
    public class FeelAtEaseRelic : Relic
    {
        FeelAtEaseRelic()
        {
            Name = "feel_at_ease";
            Title = "心安理得";
            Description = "frag道德补正+6；frag迂回补正+3";
        }

        public override object Clone()
        {
            return new FeelAtEaseRelic();
        }
    }
}
