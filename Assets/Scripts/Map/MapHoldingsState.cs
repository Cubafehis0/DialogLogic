using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapHoldingsState : MapStateScript
{
    public override void OnMouseDown()
    {
        //Debug.Log("click holdings");
    }

    public override void OnMouseEnter()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayHoldings : MapState.MapNightHoldings;
        //Debug.Log("enter holdings");
    }

    public override void OnMouseExit()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        //Debug.Log("exit holdings");
    }
}
