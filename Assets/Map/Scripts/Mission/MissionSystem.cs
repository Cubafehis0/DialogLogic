using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IMissionSystem
{

}
public class MissionSystem : MonoBehaviour, IMissionSystem
{
    private Dictionary<string, Mission> missionsAll = new Dictionary<string, Mission>();
    private Dictionary<string, Mission> missionsNotFinished = new Dictionary<string, Mission>();
    private Dictionary<string, Mission> missionsFinished = new Dictionary<string, Mission>();


    private static IMissionSystem instance = null;
    public static IMissionSystem Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void UpdateMissionSystem()
    {
        UpdateMission();
    }

    private void UpdateMission()
    {
        Dictionary<string, Mission>.ValueCollection missions = missionsNotFinished.Values;
        foreach (Mission mission in missions)
        {
            MissionState missionState = mission.CheckMissionState();
            if (MissionState.CanDeliver >= missionState && missionState >= MissionState.Opened)
            {
                NotificationBar.Instance.AddMissionToExpandButton(mission);
            }
        }
    }

}
