using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PileObject : MonoBehaviour
{
    private Pile<Card> pile = null;
    public Pile<Card> Pile
    {
        get => pile;
        set
        {
            if (pile == value) return;
            if (pile != null)
            {
                pile.OnAdd.RemoveListener(OnAdd);
            }
            if (value != null)
            {
                value.OnAdd.AddListener(OnAdd);
            }
            pile = value;
        }
    }

    private void OnAdd(Card card)
    {
       
        CardObject cardObject = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        if (cardObject)
        {
            cardObject.transform.SetParent(transform, true);
            cardObject.gameObject.SetActive(true);
        }
    }
}