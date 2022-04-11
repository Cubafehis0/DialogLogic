using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileObject : MonoBehaviour
{
    [SerializeField]
    private CardController player;


    private void OnEnable()
    {
        player.DrawPile.OnAdd.AddListener(OnAdd);
    }

    private void OnAdd(Card newCard)
    {
        CardObject o=StaticCardLibrary.Instance.GetCardObject(newCard);
        if (o == null) 
        {
            throw new System.NotImplementedException();
        }
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
