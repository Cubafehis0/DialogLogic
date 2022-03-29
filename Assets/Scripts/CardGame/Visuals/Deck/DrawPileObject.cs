using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileObject : MonoBehaviour
{
    public static DrawPileObject instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void OnEnable()
    {
        CardPlayerState.Instance.DrawPile.OnAdd.AddListener(OnAdd);
    }

    private void OnAdd(Card newCard)
    {
        newCard.transform.SetParent(transform, true);
        newCard.transform.localPosition = Vector3.zero;
        newCard.gameObject.SetActive(false);
    }
}
