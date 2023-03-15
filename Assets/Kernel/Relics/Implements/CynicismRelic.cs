namespace Assets.Kernel.Relics.Implements
{
    public class CynicismRelic : Relic
    {
        CynicismRelic()
        {
            Name = "cynicism";
            Title = "犬儒主义";
            Description = "每一回合结算完成后，弃置所有手牌";
        }

        public override object Clone()
        {
            return new CynicismRelic();
        }

        public override void OnTurnEnd()
        {
            GameConsole.Instance.DiscardAllHand();
        }
    }
}
