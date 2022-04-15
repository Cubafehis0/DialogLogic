using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MapStateScript : MonoBehaviour
{
    protected MapScript mapScript;
    private void Start()
    {
        mapScript = MapScript.Instance;
    }
    public bool IsOnUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    ////djc:下面的定义是不需要的
    //public abstract void OnMouseDown();
    //public abstract void OnMouseEnter();
    //public abstract void OnMouseExit();
}
