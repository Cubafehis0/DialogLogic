using ModdingAPI;
namespace Assets.Kernel.Relics.Implements
{
    public class GoodAtTalkRelic : Relic
    {
        GoodAtTalkRelic()
        {
            Name = "good_at_talk";
            Title = "出口成章";
            Description = "这一回合每打出2张激情牌，随机激活一张手牌";
        }

        public override object Clone()
        {
            return new GoodAtTalkRelic();
        }

        public override void OnPlayCard(Card card)
        {
            base.OnPlayCard(card);
            if (card.category == CardType.Spt)
            {
                counter++;
                if (counter == 2)
                {
                    GameConsole.Instance.RandomActivateHand();
                    counter = 0;
                }
            }
        }
    }
}
