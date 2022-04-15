using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapHoldingsState : MapStateScript
{
    public void OnMouseDown()
    {
        //Debug.Log("click holdings");
    }

    public void OnMouseEnter()
    {
        if (mapScript && !IsOnUI())
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayHoldings : MapState.MapNightHoldings;
        //Debug.Log("enter holdings");
    }

    public void OnMouseExit()
    {
        if (mapScript /*&& !IsOnUI()*/)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        //Debug.Log("exit holdings");
    }
}
