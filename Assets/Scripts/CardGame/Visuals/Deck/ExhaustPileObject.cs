using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustPileObject : MonoBehaviour
{
    [SerializeField]
    private CardController player;

    private void OnEnable()
    {
        player.ExhaustPile.OnAdd.AddListener(OnAdd);
    }

    private void OnAdd(Card newCard)
    {
        CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
