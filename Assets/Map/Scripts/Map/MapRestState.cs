using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapRestState : MapStateScript 
{ 
    public override void OnMouseDown()
    {
        //Debug.Log("click rest");
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? mapScript.MapState + 4 : mapScript.MapState - 4;
    }

    public override void OnMouseEnter()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayRest : MapState.MapNightRest;
        //Debug.Log("enter rest");
    }

    public override void OnMouseExit()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        //Debug.Log("exit rest");
    }
}
