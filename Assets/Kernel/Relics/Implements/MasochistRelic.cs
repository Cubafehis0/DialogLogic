namespace Assets.Kernel.Relics.Implements
{
    public class MasochistRelic : Relic
    {
        MasochistRelic()
        {
            Name = "masochist";
            Title = "自虐狂";
            Description = "每次扣除san值时，这一回合获得2点frag无忌补正，1点frag强势补正";
        }

        public override object Clone()
        {
            return new MasochistRelic();
        }

        public override void OnHealthChange()
        {
            GameConsole.Instance.AddStatus("self", "frag无忌", 2);
            GameConsole.Instance.AddStatus("self", "frag强势", 1);
        }
    }
}
