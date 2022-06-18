namespace Kernel.Interfaces
{
    internal interface IPlayerState
    {
        IPlayer Player { get; set; }
        ModifierGroup Modifiers { get; }
    }
}
