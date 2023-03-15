namespace Assets.Kernel.Relics.Implements
{
    public class EmotionalRelic : Relic
    {
        EmotionalRelic()
        {
            Name = "emotional";
            Title = "情绪化";
            Description = "每消耗一点内视，这一回合获得1点frag无忌补正";
        }

        public override object Clone()
        {
            return new EmotionalRelic();
        }

        public override void OnPersonalityChange()
        {
            base.OnPersonalityChange();
            GameConsole.Print("获得1点frag无忌补正");
        }
    }
}
