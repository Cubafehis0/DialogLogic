namespace Assets.Kernel.Relics.Implements
{
    public class RadicalRelic : Relic
    {
        RadicalRelic()
        {
            Name = "radical";
            Title = "激进";
            Description = "每回合触发的第一次“抽取”效果触发两次";
        }

        public override object Clone()
        {
            return new RadicalRelic();
        }

        public override void OnTurnStart()
        {
            counter = 1;
        }

        public override void OnDraw(Card card)
        {
            if (card.DrawEffects != null)
            {
                card.DrawEffects.Invoke();
            }
        }


    }
}
