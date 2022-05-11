using UnityEngine.Events;

public interface ITurnStart
{
    void OnTurnStart();
}

public interface ITurnEnd
{
    void OnTurnEnd();
}


public interface ITurnController
{
    void StartTurn();
    void EndTurn();
    bool EndTurnTrigger { get; }
    bool AdditionalTurn { get; }
    UnityEvent OnTurnStart { get; }
    UnityEvent OnTurnEnd { get; }
}
