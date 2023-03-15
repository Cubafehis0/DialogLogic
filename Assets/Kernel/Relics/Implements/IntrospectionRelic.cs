using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class IntrospectionRelic : Relic
    {
        IntrospectionRelic()
        {
            Name = "introspection";
            Title = "内省";
            Description = "这一回合内每抽取4张牌，获得1点内感";
        }

        public override object Clone()
        {
            return new IntrospectionRelic();
        }

        public override void OnDraw(Card card)
        {
            counter++;
            if (counter == 4)
            {
                GameConsole.Instance.ModifyPersonality("self", new Personality(0, 0, 0, 0), -1, DMGType.Normal);
                counter = 0;
            }
        }

        public override void OnTurnEnd()
        {
            counter = 0;
        }
    }
}
