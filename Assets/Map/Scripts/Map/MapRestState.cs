using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapRestState : MapStateScript 
{
/*    [SerializeField]
    GameObject gb;
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[results.Count - 1].gameObject;
        }

        return null;
    }*/
    //public void Update()
    //{
    //    GameObject g = GetOverUI(gb);
    //    Debug.Log(IsOnUI() + ((g == null) ? "null" : g.name));
    //}
    public void OnMouseDown()
    {
        //Debug.Log("click rest");
        if (mapScript && !IsOnUI())
            mapScript.MapState = (int)mapScript.MapState < 4 ? mapScript.MapState + 4 : mapScript.MapState - 4;
    }

    public void OnMouseEnter()
    {
        if (mapScript&&!IsOnUI())
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayRest : MapState.MapNightRest;
        //Debug.Log("enter rest");
    }

    public void OnMouseExit()
    {
        if (mapScript/* && !IsOnUI()*/)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        //Debug.Log("exit rest");
    }
}
