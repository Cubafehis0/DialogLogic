using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SelectHandPileObject : HandPileObject
{
    public UnityEvent<Card> onSelectCard = new UnityEvent<Card>();
    public override void SelectCard(Card card)
    {
        FocusedCard = card;
        lockFocus = true;
        onSelectCard.Invoke(card);
    }
}