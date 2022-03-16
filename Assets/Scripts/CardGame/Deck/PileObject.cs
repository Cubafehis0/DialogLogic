using System.Collections;
using UnityEngine;

public class PileObject : MonoBehaviour
{

    protected Pile<Card> pile = null;
    public Pile<Card> Pile
    {
        get => pile;
        set
        {
            if (pile == value) return;
            if (pile != null)
            {
                pile.OnAdd.RemoveListener(OnAdd);
                pile.OnRemove.RemoveListener(OnAdd);
            }
            if (value != null)
            {
                value.OnAdd.AddListener(OnAdd);
                value.OnRemove.AddListener(OnRemove);
            }
            pile = value;
        }
    }
    protected virtual void OnAdd(Card card)
    {
        card.transform.SetParent(transform, true);
    }

    protected virtual void OnRemove(Card card)
    {

    }
}