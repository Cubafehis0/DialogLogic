using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileObject : MonoBehaviour
{
    [SerializeField]
    private CardManager player;


    private void OnEnable()
    {
        player.DrawPile.OnAdd.AddListener(OnAdd);
    }

    private void OnAdd(Card newCard)
    {
        newCard.transform.SetParent(transform, true);
        newCard.transform.localPosition = Vector3.zero;
        newCard.gameObject.SetActive(false);
    }
}
