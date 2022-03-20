using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MyEventTrigger : EventTrigger
{
    private EventTriggerGroup group=null;
    private Dictionary<EventTriggerType, Entry> scriptEvent = new Dictionary<EventTriggerType, Entry>();
    public void AddEventTrigger(EventTriggerType type, UnityAction<BaseEventData> action)
    {
        Entry entry;
        if (scriptEvent.ContainsKey(type))
        {
            entry = scriptEvent[type];
        }
        else
        {
            entry = new Entry
            {
                eventID = type
            };
            triggers.Add(entry);
            scriptEvent.Add(type, entry);
        }
        entry.callback.AddListener(action);
    }

    public void RemoveEventTrigger(EventTriggerType type, UnityAction<BaseEventData> action)
    {
        Entry entry = scriptEvent[type];
        entry.callback.RemoveListener(action);
    }
    private void Start()
    {
        group = transform.parent.GetComponentInParent<EventTriggerGroup>();
    }

    private void OnTransformParentChanged()
    {
        group = transform.parent.GetComponentInParent<EventTriggerGroup>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (group) group.OnPointerClick(eventData);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        if (group) group.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (group) group.OnEndDrag(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (group) group.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (group) group.OnPointerExit(eventData);
    }
}
