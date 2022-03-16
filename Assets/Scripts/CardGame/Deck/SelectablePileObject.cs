using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SelectablePileObject : PileObject
{
    public UnityEvent<Card> OnSelectCard = new UnityEvent<Card>();
    protected override void OnAdd(Card card)
    {
        base.OnAdd(card);
        CardObject cardObject = card.GetComponent<CardObject>();
        if (cardObject)
        {
            cardObject.OnPointerEnter.AddListener(OnSelectCard.Invoke);
        }
    }
    protected override void OnRemove(Card card)
    {
        base.OnRemove(card);
        CardObject cardObject = card.GetComponent<CardObject>();
        if (cardObject)
        {
            cardObject.OnPointerEnter.RemoveListener(OnSelectCard.Invoke);
        }
    }
}