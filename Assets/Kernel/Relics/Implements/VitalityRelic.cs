namespace Assets.Kernel.Relics.Implements
{
    public class VitalityRelic : Relic
    {
        VitalityRelic()
        {
            Name = "vitality";
            Title = "生命力";
            Description = "每完成一个事件降低5点压力值，并恢复2点san值";
        }

        public override object Clone()
        {
            return new VitalityRelic();
        }

        public override void OnVictory()
        {
            GameConsole.Instance.AddPressure("", -5);
            GameConsole.Instance.AddHealth("", 2);
        }
    }
}
