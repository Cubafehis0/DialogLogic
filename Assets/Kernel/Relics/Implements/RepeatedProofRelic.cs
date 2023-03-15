namespace Assets.Kernel.Relics.Implements
{
    public class RepeatedProofRelic : Relic
    {
        RepeatedProofRelic()
        {
            Name = "repeat_proof";
            Title = "反复论证";
            Description = "每一回合结算时中如果手牌中逻辑、道德、迂回牌数量大于等于其对立倾向牌数量，则frag逻辑补正、frag道德补正、frag迂回补正+n；否则相反";
        }

        public override object Clone()
        {
            return new RepeatedProofRelic();
        }
    }
}
