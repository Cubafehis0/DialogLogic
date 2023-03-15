using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class FixedStarRelic : Relic
    {
        FixedStarRelic()
        {
            Name = "fixed_star";
            Title = "恒星";
            Description = "frag强势补正+8，frag无忌补正+3，若在一回合结算时的强势总数值大于30，则移除恒星，获得超新星";
        }

        public override object Clone()
        {
            return new FixedStarRelic();
        }

        public override void OnTurnEnd()
        {
            Personality personality = GameQuery.Instance.GetPersonality();
            int inner=personality.Inner;
            if (inner > 30)
            {
                LoseThis();
                GameConsole.Instance.ObtainRelic("supernova");
            }
        }
    }













}
