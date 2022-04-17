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

    [SerializeField]
    private CardController player;

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
        player.Hand.OnAdd.AddListener(OnAdd);
        player.Hand.OnRemove.AddListener(OnRemove);
    }

    private void OnDisable()
    {
        player.Hand.OnAdd.RemoveListener(OnAdd);
        player.Hand.OnRemove.RemoveListener(OnRemove);
    }

    private void OnAdd(Card card)
    {
        CardObject cardObject = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        AnimationManager.Instance.AddAnimation(Animating(cardObject.gameObject));
    }

    private IEnumerator Animating(GameObject cardObject)
    {
        cardObject.SetActive(true);
        cardObject.transform.SetParent(transform, true);
        cardObject.transform.rotation = Quaternion.identity;
        cardObject.transform.localScale = Vector3.one;
        cardObject.GetComponent<CardObject>().Draggable = true ;
        yield return null;
    }

    public void SetEnableDragging(bool value)
    {
        foreach(var card in player.Hand)
        {
            CardObject o= GameManager.Instance.CardObjectLibrary.GetCardObject(card);
            o.Draggable = value;
        }
    }

    protected virtual void OnRemove(Card card)
    {
        CardObject cardObject = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        if (cardObject)
        {
            cardObject.Draggable = false;
        }

    }
    public void TakeoverAllCard()
    {
        Debug.Log("takeover");
        foreach(Card card in player.Hand)
        {
            CardObject o= GameManager.Instance.CardObjectLibrary.GetCardObject(card);
            o.transform.SetParent(transform, true);
        }
    }

}
