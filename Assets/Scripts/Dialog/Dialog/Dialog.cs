using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialog : MonoBehaviour
{

    public virtual void HideChildren(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            parent.transform.GetChild(i).gameObject.SetActive(false);

    }


}
