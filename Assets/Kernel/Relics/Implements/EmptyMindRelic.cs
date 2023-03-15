using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class EmptyMindRelic : Relic
    {
        EmptyMindRelic()
        {
            Name = "empty_mind";
            Title = "放空";
            Description = "这一回合内每弃置4张牌获得1点外感";
        }

        public override object Clone()
        {
            return new EmptyMindRelic();
        }

        public override void OnDiscard(Card card)
        {
            counter++;
            if (counter == 4)
            {
                GameConsole.Instance.ModifyPersonality("self", new Personality(0, 0, 0, 1), -1, DMGType.Normal);
                counter = 0;
            }
        }
    }
}
