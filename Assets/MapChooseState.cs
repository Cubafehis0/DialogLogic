using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapChooseState : MapStateScript
{
    public override void OnMouseDown()
    {
        Debug.Log("click choose");
    }

    public override void OnMouseEnter()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayChoose : MapState.MapNightChoose;
        Debug.Log("enter choose");
    }

    public override void OnMouseExit()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        Debug.Log("exit choose");
    }


}
