using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface IMissionSystem
{
}

public class MissionsInfo
{
    [XmlArray(ElementName = "missions")]
    [XmlArrayItem(ElementName = "mission")]
    public List<MissionInfo> missionsDic = new List<MissionInfo>();
}

public class MissionSystem : IMissionSystem
{
    [SerializeField]
    private List<Mission> missions = new List<Mission>();

    private Dictionary<string, Mission> missionsAll = new Dictionary<string, Mission>();
    private Dictionary<string, Mission> missionsNotFinished = new Dictionary<string, Mission>();
    private Dictionary<string, Mission> missionsFinished = new Dictionary<string, Mission>();

    public MissionSystem(MissionsInfo info)
    {
        missions = info.missionsDic.ConvertAll(x => new Mission(x));
        //Debug.LogWarning(missions.Count);
        foreach (Mission m in missions)
        {
            missionsAll.Add(m.missionName, m);
            missionsNotFinished.Add(m.missionName, m);
        }
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
            //MissionState missionState = mission.CheckMissionState();
            //if (MissionState.CanDeliver >= missionState && missionState >= MissionState.Opened)
            //{
            //
            //}
            //Debug.LogError(missions.Count);
            if (mission.CheckStartCondition() && !mission.Finished)
                NotificationBar.Instance.AddMissionToExpandButton(mission);
        }
    }
}