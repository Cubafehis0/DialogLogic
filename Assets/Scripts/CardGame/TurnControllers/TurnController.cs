using UnityEngine;
using UnityEngine.Events;

public abstract class TurnController : MonoBehaviour, ITurnController
{
    [SerializeField]
    private UnityEvent onTurnStart = new UnityEvent();
    [SerializeField]
    private UnityEvent onTurnEnd = new UnityEvent();
    public abstract bool EndTurnTrigger { get; }
    public virtual bool AdditionalTurn => false;
    public UnityEvent OnTurnStart { get => onTurnStart; }
    public UnityEvent OnTurnEnd { get => onTurnEnd; }
    public virtual void StartTurn() { OnTurnStart.Invoke(); }
    public virtual void EndTurn() { OnTurnEnd.Invoke(); }
}
