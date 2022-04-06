using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapChooseState : MapStateScript
{

    
    public override void OnMouseDown()
    {
        //Debug.Log("click choose");
        //djc:修改为用名字搜寻
        SceneManager.LoadScene("PlaceScene");
    }

    public override void OnMouseEnter()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayChoose : MapState.MapNightChoose;
        //Debug.Log("enter choose");
    }

    public override void OnMouseExit()
    {
        if (mapScript)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        //Debug.Log("exit choose");
    }


}
