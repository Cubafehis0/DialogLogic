using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingPileObject : PilePacked
{
    [SerializeField]
    private CardController player;

    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
    }

    private void OnAddAnim(Card newCard)
    {
        CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
    }
}
