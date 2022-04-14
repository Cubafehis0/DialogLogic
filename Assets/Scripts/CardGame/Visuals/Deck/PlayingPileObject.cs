using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingPileObject : MonoBehaviour
{
    [SerializeField]
    private CardController player;

    private void OnEnable()
    {
        player.PlayingPile.OnAdd.AddListener(OnAdd);
    }

    private void OnAdd(Card newCard)
    {
        CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
    }
}
