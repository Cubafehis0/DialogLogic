namespace Assets.Kernel.Relics.Implements
{
    public class PhysiqueRelic : Relic
    {
        PhysiqueRelic()
        {
            Name = "physique";
            Title = "体魄";
            Description = "每有一张牌保留在手牌里大于等于3个回合，获得2点内视";
        }

        public override object Clone()
        {
            return new PhysiqueRelic();
        }
    }
}
