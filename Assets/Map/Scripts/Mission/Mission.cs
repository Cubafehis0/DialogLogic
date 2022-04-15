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
        this.missionName = missionName;
        this.missionType = missionType;
        this.description = description;
        this.promulgator = promulgator;

        this.relevantIncident = new List<string>(relevantIncident.Split(';'));
        this.triggerDate = triggerDate;
        this.triggerIncident = new List<string>(triggerIncident.Split(';'));
        this.conditionIncident = new List<string>(conditionIncident.Split(';'));
        this.conditionKey = new List<string>(conditionKey.Split(';'));
        this.conditionFavor = new List<string>(conditionFavor.Split(';'));
        this.conditionReputation = new List<string>(conditionReputation.Split(';'));
        this.deadline = deadline;
        this.deadIncident = new List<string>(deadIncident.Split(';'));
        this.missionKey = new List<string>(missionKey.Split(';'));
        this.missionIncident = new List<string>(missionIncident.Split(';'));
    }
    public Mission(MissionEntity m)
    {
        this.missionName = m.mission;
        this.missionType = TryGetMissionType(m.type);
        this.description = m.description;
        this.promulgator = m.promulgator;

        this.relevantIncident = new List<string>(m.relevant_incident.Split(';'));
        this.triggerDate = IncidentTool.StringToInt(m.trigger_date);
        this.triggerIncident = new List<string>(m.trigger_incident.Split(';'));
        this.conditionIncident = new List<string>(m.condition_incident.Split(';'));
        this.conditionKey = new List<string>(m.condition_key.Split(';'));
        this.conditionFavor = new List<string>(m.condition_favor.Split(';'));
        this.conditionReputation = new List<string>(m.condition_reputation.Split(';'));
        this.deadline = IncidentTool.StringToInt(m.deadline);
        this.deadIncident = new List<string>(m.dead_incident.Split(';'));
        this.missionKey = new List<string>(m.mission_key.Split(';'));
        this.missionIncident = new List<string>(m.mission_incident.Split(';'));
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
            return CheckMissionState();
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
    private MissionType TryGetMissionType(string s)
    {
        if (s.Equals("main")) return MissionType.Main;
        if (s.Equals("branch")) return MissionType.Branch;
        if (s.Equals("lambda")) return MissionType.Lambda;
        if (s.Equals("daily")) return MissionType.Daily;
        Debug.LogError("worong missiontype:" + s);
        return MissionType.Daily;
    }
}
