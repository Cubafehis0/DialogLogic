using UnityEngine;
using UnityEngine.Events;

public abstract class TurnController : MonoBehaviour, ITurnController
{
    public UnityEvent onTurnStart = new UnityEvent();
    public UnityEvent onTurnEnd = new UnityEvent();
    public abstract bool EndTurnTrigger { get; }
    public UnityEvent OnTurnStart { get => onTurnStart; }
    public UnityEvent OnTurnEnd { get => onTurnEnd; }
    public virtual void StartTurn() { OnTurnStart.Invoke(); }
    public virtual void EndTurn() { OnTurnEnd.Invoke(); }
}
