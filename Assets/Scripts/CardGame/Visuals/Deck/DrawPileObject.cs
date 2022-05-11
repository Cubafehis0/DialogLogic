using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PilePacked))]
public class DrawPileObject : MonoBehaviour
{
    [SerializeField]
    private PilePacked pile;

    private void Awake()
    {
        pile = GetComponent<PilePacked>();
    }
    private void OnEnable()
    {
        pile.OnAdd.AddListener(OnAdd);
    }
    private void OnDisable()
    {
        pile.OnAdd.AddListener(OnAdd);
    }

    private void OnAdd(Card newCard)
    {
        CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        if (o == null)
        {
            throw new System.NotImplementedException();
        }
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
