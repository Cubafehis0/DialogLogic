using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public MissionState missionState = MissionState.Unopened;

    public string missionName;
    public MissionType missionType;
    public string description;
    public string promulgator;
    public List<string> relevantIncident;

    public int triggerDate;
    public List<string> triggerIncident;

    public List<string> conditionIncident;
    public List<string> conditionKey;
    public List<string> conditionFavor;
    public List<string> conditionReputation;

    public int deadline;
    public List<string> deadIncident;

    public List<string> missionKey;
    public List<string> missionIncident;

    public Award award;

    public Mission(string missionName, MissionType missionType, string description, string promulgator, string relevantIncident,
        int triggerDate, string triggerIncident, string conditionIncident, string conditionKey, string conditionFavor, string conditionReputation,
        int deadline, string deadIncident, string missionKey, string missionIncident)
    {

    }

    public MissionState CheckMissionState()
    {
        MissionState state = this.missionState;

        if (IsDead()) this.missionState = MissionState.Expired;
        switch (this.missionState){
            case MissionState.Unopened:
                if (IncidentSystem.Instance.IsFinishedAllIncidents(triggerIncident))
                {
                    this.missionState = MissionState.Opened;
                }
                break;
            case MissionState.Opened:
                if (this.missionType == MissionType.Main)
                {
                    this.missionState = MissionState.UnderWay;
                }
                break;
            case MissionState.UnderWay:
                if (CheckFinshedCondition())
                {
                    this.missionState = MissionState.CanDeliver;
                }
                break;
            case MissionState.CanDeliver:
                if (this.missionType == MissionType.Main || this.missionType == MissionType.Lambda)
                {
                    this.missionState = MissionState.Finished;
                }
                break;
            case MissionState.Finished:
                break;
            case MissionState.Expired:
                break;
            default:
                Debug.LogError("wrong MissionState");
                break;
        }

        if (this.missionState != state)
        {

        }

        return this.missionState;
    }
    
    private bool CheckFinshedCondition()
    {
        if (IncidentSystem.Instance.IsFinishedAllIncidents(missionIncident))
        {
            return true;
        }
        return false;
    }
    
    private bool IsDead()
    {
        if (IncidentSystem.Instance.IsFinishedAllIncidents(deadIncident))
            return true;
        return false;
    }
}
