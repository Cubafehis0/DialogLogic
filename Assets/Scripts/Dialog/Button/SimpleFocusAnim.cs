using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleFocusAnim : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float biggerScale = 1.1f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale *= biggerScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale /= biggerScale;
    }
}
