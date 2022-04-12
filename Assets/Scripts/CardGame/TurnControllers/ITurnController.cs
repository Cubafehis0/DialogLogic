using UnityEngine.Events;

public interface ITurnController
{
    void StartTurn();
    void EndTurn();
    bool EndTurnTrigger { get; }
    UnityEvent OnTurnStart { get; }
    UnityEvent OnTurnEnd { get; }
}
