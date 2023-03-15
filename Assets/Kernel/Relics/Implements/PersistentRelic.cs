namespace Assets.Kernel.Relics.Implements
{
    public class PersistentRelic : Relic
    {
        PersistentRelic()
        {
            Name = "persistent";
            Title = "坚毅";
            Description = "你不会再被动摇";
        }

        public override object Clone()
        {
            return new PersistentRelic();
        }
    }
}
