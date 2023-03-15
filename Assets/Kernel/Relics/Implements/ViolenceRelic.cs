namespace Assets.Kernel.Relics.Implements
{
    public class ViolenceRelic : Relic
    {
        ViolenceRelic()
        {
            Name = "violence";
            Title = "暴力倾向";
            Description = "每消耗一点外感，这一回合随机使一个已揭示的对话门槛值减3";
        }

        public override object Clone()
        {
            return new ViolenceRelic();
        }

        public override void OnPersonalityChange()
        {
            base.OnPersonalityChange();
        }
    }
}
