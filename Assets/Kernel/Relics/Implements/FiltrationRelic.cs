namespace Assets.Kernel.Relics.Implements
{
    public class FiltrationRelic : Relic
    {
        FiltrationRelic()
        {
            Name = "filtration";
            Title = "过滤";
            Description = "每回合结算完成后弃置2张手牌";
        }

        public override object Clone()
        {
           return new FiltrationRelic();
        }

        public override void OnTurnEnd()
        {
            //GameConsole.Instance.Discard()
        }
    }
}
