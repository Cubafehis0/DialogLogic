using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[RequireComponent(typeof(HandLayout))]
[RequireComponent(typeof(EventTriggerGroup))]
public class DragHandPileObject : MonoBehaviour
{
    public static DragHandPileObject instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("不允许多个HandsArrangement单例");
        }
    }

    private void OnEnable()
    {
        CardPlayerState.Instance.Hand.OnAdd.AddListener(OnAdd);
        CardPlayerState.Instance.Hand.OnRemove.AddListener(OnRemove);
    }

    private void OnDisable()
    {
        CardPlayerState.Instance.Hand.OnAdd.RemoveListener(OnAdd);
        CardPlayerState.Instance.Hand.OnRemove.RemoveListener(OnRemove);
    }

    private void OnAdd(Card card)
    {
        card.transform.SetParent(transform, true);
        card.transform.localPosition = Vector3.zero;
        CardObject cardObject = card.GetComponent<CardObject>();
        if (cardObject)
        {
            int index = CardPlayerState.Instance.Hand.IndexOf(card);
            cardObject.gameObject.SetActive(true);
            cardObject.transform.SetSiblingIndex(index);
            cardObject.transform.rotation = Quaternion.identity;
            cardObject.transform.localScale = Vector3.one;
            Draggable draggable = cardObject.GetComponent<Draggable>();
            if (draggable == null) cardObject.gameObject.AddComponent<Draggable>();
        }

    }
    protected virtual void OnRemove(Card card)
    {
        CardObject cardObject = card.GetComponent<CardObject>();
        if (cardObject)
        {
            Destroy(cardObject.GetComponent<Draggable>());
        }

    }
    public void TakeoverAllCard()
    {
        Debug.Log("takeover");
        CardPlayerState.Instance.Hand.ForEach(item => item.transform.SetParent(transform, true));
    }

}
