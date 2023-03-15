namespace Assets.Kernel.Relics.Implements
{
    public class StrongRelic : Relic
    {
        StrongRelic()
        {
            Name = "strong";
            Title = "强壮";
            Description = "san值上限提升10点，压力值上限提升10点";
        }

        public override object Clone()
        {
            return new StrongRelic();
        }

        public override void OnPickUp()
        {
            GameConsole.Instance.AddMaxHealth(10);
            GameConsole.Instance.AddMaxPressure(10);
        }
    }
}
