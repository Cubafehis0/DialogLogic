using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IMissionSystem
{
    public void InitMissionSystem(List<MissionEntity> missionEntities);
    public void UpdateMissionSystem();
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
    public void InitMissionSystem(List<MissionEntity> missionEntities)
    {
        foreach (MissionEntity missionEntity in missionEntities)
        {
            Mission mission = new Mission(missionEntity);
            missionsAll.Add(mission.missionName, mission);
            missionsNotFinished.Add(mission.missionName, mission);
        }
    }
    public void UpdateMissionSystem()
    {
        UpdateMission();
    }

    private void UpdateMission()
    {
        Dictionary<string, Mission>.ValueCollection missions = this.missionsNotFinished.Values;
        List<Mission> removeList = new List<Mission>();
        foreach (Mission mission in missions)
        {
            MissionState missionState = mission.CheckMissionState();
            //Debug.Log(missionState);
            if (MissionState.CanDeliver >= missionState && missionState >= MissionState.Opened)
            {
                NotificationBar.Instance.AddMissionToExpandButton(mission);
            }
            if (MissionState.Expired == missionState || MissionState.Finished == missionState)
            {
                NotificationBar.Instance.AddMissionToExpandButton(mission);
                removeList.Add(mission);
            }
        }
        RemoveMissionFromNotFinished(removeList);
    }
    public void RemoveMissionFromAll(Mission mission)
    {
        missionsAll.Remove(mission.missionName);
        missionsNotFinished.Remove(mission.missionName);
        missionsFinished.Remove(mission.missionName);
    }
    public void RemoveMissionFromNotFinished(Mission mission)
    {
        missionsNotFinished.Remove(mission.missionName);
    }
    public void RemoveMissionFromNotFinished(List<Mission> missions)
    {
        foreach(Mission mission in missions)
        {
            RemoveMissionFromNotFinished(mission);
        }
    }
}
