namespace Assets.Kernel.Relics.Implements
{
    public class MaltreatedRelic : Relic
    {
        MaltreatedRelic()
        {
            Name = "maltreated";
            Title = "受虐";
            Description = "每一回合开始，扣除1点san值，这一回合frag无忌补正+2";
        }

        public override object Clone()
        {
            return new MaltreatedRelic();
        }

        public override void OnTurnStart()
        {
            GameConsole.Instance.AddHealth("self", -1);
            GameConsole.Instance.AddStatus("self", "frag无忌补正", 2);
        }
    }
}
