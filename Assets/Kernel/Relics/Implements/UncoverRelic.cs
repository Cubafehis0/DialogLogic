namespace Assets.Kernel.Relics.Implements
{
    public class UncoverRelic : Relic
    {
        UncoverRelic()
        {
            Name = "uncover";
            Title = "揭开";
            Description = "每一回合开始时，每有1点外感随机揭开1个对话门槛；frag逻辑补正+4";
        }

        public override object Clone()
        {
            return new UncoverRelic();
        }

        public override void OnTurnStart()
        {
            int cnt = GameQuery.Instance.GetPlayerProp("", "outside");

            GameConsole.Instance.RevealRandomCondition("", cnt);

        }
    }













}
