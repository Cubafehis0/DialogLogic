using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> listeners = new List<GameObject>();
    //[SerializeField]
    private UnityEvent onTurnStart = new UnityEvent();
    //[SerializeField]
    private UnityEvent onTurnEnd = new UnityEvent();

    private List<ITurnStart> startListeners = new List<ITurnStart>();
    private List<ITurnEnd> endListeners = new List<ITurnEnd>();

    public virtual bool EndTurnTrigger => endTurntrigger;

    private bool endTurntrigger=false;
    public virtual bool AdditionalTurn => false;
    public UnityEvent OnTurnStart { get => onTurnStart; }
    public UnityEvent OnTurnEnd { get => onTurnEnd; }

    private void Awake()
    {
        foreach (var go in listeners)
        {
            var start = go.GetComponents<ITurnStart>();
            var end = go.GetComponents<ITurnEnd>();
            if (start != null) startListeners.AddRange(start);
            if (end != null) endListeners.AddRange(end);
        }
        gameObject.SetActive(false);
    }

    public void AddListener(GameObject gameObject)
    {
        listeners.Add(gameObject);
    }
    public void EndTurn()
    {
        endTurntrigger = true;
    }

    public virtual void OnStartTurn()
    {
        endTurntrigger = false;
        gameObject.SetActive(true);
        foreach (var s in startListeners) s.OnTurnStart();
        OnTurnStart.Invoke();
    }
    public virtual void OnEndTurn() 
    {
        endTurntrigger = true;
        gameObject.SetActive(false);
        foreach (var s in endListeners) s.OnTurnEnd();
        OnTurnEnd.Invoke();
    }
}
