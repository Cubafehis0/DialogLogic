public interface IStatusManager:ITurnStart,ITurnEnd
{
    IReadonlyModifierGroup Modifiers { get; }

    void AddAnonymousCostModifer(CostModifier costModifier, int timer);
    void AddAnonymousFocusModifer(SpeechType speechType, int timer);
    void AddAnonymousPersonalityModifier(Personality personality, int timer);
    void AddAnonymousSpeechModifer(SpeechArt speechArt, int timer);
    void AddStatusCounter(Status status, int value);
    int GetStatusValue(Status status);
    int GetStatusValue(string name);
}