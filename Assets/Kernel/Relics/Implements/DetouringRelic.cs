namespace Assets.Kernel.Relics.Implements
{
    public class DetouringRelic : Relic
    {
        DetouringRelic()
        {
            Name = "detouring";
            Title = "绕弯";
            Description = "每次洗牌后获得一个不增加压力值的额外回合";
        }

        public override object Clone()
        {
            return new DetouringRelic();
        }

        public override void OnDiscard2Draw()
        {
            GameConsole.Instance.GainAdditionalTurn();
        }
    }
}
