using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incident
{
    public string incidentName;
    public IncidentType incidentType;
    public int remainingTimes;
    public string incidentPlace;
    public bool hadDone;//是否做过

    public List<string> conditionMain;
    public List<string> conditionKey;
    public List<string> conditionReputation;

    public int priorityInitial;
    public int priorityReputation;
    public int[] priorityTime = new int[2];
    public Incident(string incidentName, IncidentType incidentType, int remainingTimes, string incidentPlace,
        string conditionMain, string conditionKey, string conditionReputation,
        int priorityInitial, int priorityReputation, string priorityTime)
    {
        this.incidentName = incidentName;
        this.incidentType = incidentType;
        this.remainingTimes = remainingTimes;
        this.incidentPlace = incidentPlace;
        this.hadDone = false;

        this.conditionMain = new List<string>(conditionMain.Split(';'));
        this.conditionKey = new List<string>(conditionKey.Split(';'));
        this.conditionReputation = new List<string>(conditionReputation.Split(';'));

        this.priorityInitial = priorityInitial;
        this.priorityReputation = priorityReputation;


        string[] s = priorityTime.Replace("(", string.Empty).Replace(")", string.Empty).Split(',');

        if (int.TryParse(s[0], out this.priorityTime[0]) &&
            int.TryParse(s[1], out this.priorityTime[1]))
        {

        }
        else if (s[0].Length==0)
        {
            Debug.Log("null in priorityTime");
        }
        else
            Debug.Log("wrong format in priorityTime");
    }
    public Incident(IncidentEntity i)
    {
        IncidentType type = TryGetIncidentType(i.type);
        this.incidentName = i.incident;
        this.incidentType = type;
        this.remainingTimes = IncidentTool.StringToInt(i.times);
        this.incidentPlace = "平民区";
        this.hadDone = false;

        this.conditionMain = new List<string>(i.condition_main.Split(';'));
        this.conditionKey = new List<string>(i.condition_key.Split(';'));
        this.conditionReputation = new List<string>(i.condition_reputation.Split(';'));

        this.priorityInitial = IncidentTool.StringToInt(i.priority_initial);
        this.priorityReputation = IncidentTool.StringToInt(i.priority_reputation);


        string[] s = i.priority_time.Replace("(", string.Empty).Replace(")", string.Empty).Split(',');

        if (int.TryParse(s[0], out this.priorityTime[0]) &&
            int.TryParse(s[1], out this.priorityTime[1]))
        {

        }
        else if (s[0].Length==0)
        {
            Debug.Log("null in priorityTime");
        }
        else
            Debug.Log("wrong format in priorityTime "+s);
    }
    public bool CheckCondition()
    {
        Debug.LogWarning("待完成");
        return true;
    }

    public void FinishedIncident()
    {
        if (this.remainingTimes > 0)
        {
            this.remainingTimes--;
            hadDone = true;
        }
        if (this.remainingTimes <= 0)
        {
            IncidentSystem.Instance.RemoveIncidentsFromNotFinished(this.incidentName);
        }
    }
    public IncidentType TryGetIncidentType(string s)
    {
        if (s.Equals("main")) return IncidentType.Main;
        if (s.Equals("branch")) return IncidentType.Branch;
        if (s.Equals("daily")) return IncidentType.Daily;
        Debug.LogError("incorrect incidenttype:" + s);
        return IncidentType.Daily;
    }
}
