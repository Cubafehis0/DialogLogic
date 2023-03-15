namespace Assets.Kernel.Relics.Implements
{
    public class SwallowRelic : Relic 
    {
        SwallowRelic()
        {
            Name = "swallow";
            Title = "吞噬";
            Description = "获得该碎片时，选择移除牌库中2张牌，获得任意2张强势牌和恒星";
        }

        public override object Clone()
        {
            return new SwallowRelic();
        }

        public override void OnPickUp()
        {
            GameConsole.Instance.OpenGUI("选择移除牌库中2张牌");
            GameConsole.Instance.OpenGUI("获得任意2张强势牌和恒星");
        }
    }
}
