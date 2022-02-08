using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ŀǰ����Ϊ���
/// </summary>
[RequireComponent(typeof(Pile))]
public class DrawPileObject : MonoBehaviour,IPileListener
{
    public static DrawPileObject instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void OnEnable()
    {
        GetComponent<Pile>().UpdateBindingObject();
    }

    private void OnDisable()
    {
        GetComponent<Pile>().UpdateBindingObject();
    }

    public void OnAdd(Card newCard)
    {
        newCard.transform.SetParent(transform, true);
        newCard.transform.localPosition = Vector3.zero;
        newCard.gameObject.SetActive(false);
    }

    public void OnRemove(Card oldCard)
    {
        return;
    }

    public void OnShuffle()
    {
        return;
    }
}
