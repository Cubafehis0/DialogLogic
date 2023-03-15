namespace Assets.Kernel.Relics.Implements
{
    public class PleasureCriminalRelic : Relic
    {
        PleasureCriminalRelic()
        {
            Name = "pleasure_criminal";
            Title = "愉悦犯";
            Description = "每一次对方获得debuff时，降低4点压力值，恢复2点san值";
        }

        public override object Clone()
        {
            return new PleasureCriminalRelic();
        }
    }
}
