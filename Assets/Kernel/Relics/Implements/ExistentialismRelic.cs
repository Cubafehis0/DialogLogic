using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class ExistentialismRelic : Relic
    {
        ExistentialismRelic()
        {
            Name = "existentialism";
            Title = "存在主义";
            Description = "每过10个回合，根据内视大小增加持续1回合的frag道德补正，frag逻辑补正";
        }

        public override object Clone()
        {
            return new ExistentialismRelic();
        }

        public override void OnTurnStart()
        {
            counter++;
            if (counter == 10)
            {
                Personality personality = GameQuery.Instance.GetPersonality();
                int value = personality.Inner;
                GameConsole.Instance.AddStatus("self", "frag道德补正", value);
                GameConsole.Instance.AddStatus("self", "frag逻辑补正", value);
                counter = 0;
            }
        }
    }
}
