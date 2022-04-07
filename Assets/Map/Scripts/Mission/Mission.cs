using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [XmlIgnore]
    public MissionState missionState = MissionState.Unopened;

    [XmlElement(ElementName = "name")]
    public string missionName;

    [XmlElement(ElementName = "type")]
    public MissionType missionType;

    [XmlElement(ElementName = "description")]
    public string description;

    [XmlElement(ElementName = "promulgator")]
    //������
    public string promulgator;

    [XmlArray(ElementName = "relevantIncident")]
    public List<string> relevantIncident;

    [XmlElement(ElementName ="trigger_date")]
    public int triggerDate;

    [XmlElement(ElementName ="")]
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


    public MissionState CheckMissionState()
    {
        MissionState state = this.missionState;

        if (IsDead()) this.missionState = MissionState.Expired;
        switch (this.missionState)
        {
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
