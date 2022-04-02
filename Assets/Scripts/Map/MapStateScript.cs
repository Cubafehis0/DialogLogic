using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class MapStateScript : MonoBehaviour
{
    protected MapScript mapScript;
    private void Start()
    {
        mapScript = MapScript.Instance;
    }
    public abstract void OnMouseDown();
    public abstract void OnMouseEnter();
    public abstract void OnMouseExit();
}
