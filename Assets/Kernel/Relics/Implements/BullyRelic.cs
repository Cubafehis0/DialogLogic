namespace Assets.Kernel.Relics.Implements
{
    public class BullyRelic : Relic
    {
        BullyRelic()
        {
            Name = "bully";
            Title = "欺辱";
            Description = "战斗开始时，给予对方动摇1回合";
        }

        public override object Clone()
        {
            return new BullyRelic();
        }

        public override void OnBattleStart()
        {
            GameConsole.Instance.AddStatus("target", "动摇", 1);
        }
    }
}
