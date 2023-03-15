namespace Assets.Kernel.Relics.Implements
{
    public class SoftSpokenRelic : Relic
    {
        SoftSpokenRelic()
        {
            Name = "soft_spoken";
            Title = "善于言辞";
            Description = "每过3个回合，获得1点手牌上限";
        }

        public override object Clone()
        {
            return new SoftSpokenRelic();
        }

        public override void OnTurnStart()
        {
            counter++;
            if (counter == 3)
            {
                GameConsole.Instance.AddMaxHandNum(1);
                counter = 0;
            }
        }
    }
}
