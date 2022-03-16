using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileObject : PileObject
{
    public static DrawPileObject instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void OnEnable()
    {
        pile = CardPlayerState.Instance.DrawPile;
    }

    protected override void OnAdd(Card newCard)
    {
        base.OnAdd(newCard);
        newCard.transform.localPosition = Vector3.zero;
        newCard.gameObject.SetActive(false);
    }
}
