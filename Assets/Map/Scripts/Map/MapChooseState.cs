using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapChooseState : MapStateScript
{


    public void OnMouseDown()
    {
        //Debug.Log("click choose");
        //djc:修改为用名字搜寻
        if (!IsOnUI())
        {
            /*            string s = IncidentSystem.Instance.GetIncident();
                        Debug.Log("incidentname" + s);
                        SceneManager.LoadScene(s);*/
            SceneManager.LoadScene("PlaceScene");
        }
    }

    public void OnMouseEnter()
    {
        if (mapScript && !IsOnUI())
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDayChoose : MapState.MapNightChoose;
        //Debug.Log("enter choose");
    }

    public void OnMouseExit()
    {
        if (mapScript/*&& !IsOnUI()*/)
            mapScript.MapState = (int)mapScript.MapState < 4 ? MapState.MapDay : MapState.MapNight;
        //Debug.Log("exit choose");
    }


}
