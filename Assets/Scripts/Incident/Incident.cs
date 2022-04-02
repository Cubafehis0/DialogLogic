using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incident
{
    public string incidentName;
    public IncidentType incidentType;
    public int remainingTimes;
    public string incidentPlace;

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

        this.conditionMain = new List<string>(conditionMain.Split(';'));
        this.conditionKey = new List<string>(conditionKey.Split(';'));
        this.conditionReputation = new List<string>(conditionReputation.Split(';'));

        this.priorityInitial = priorityInitial;
        this.priorityReputation = priorityReputation;


        string[] s = priorityTime.Replace("(", string.Empty).Replace(")", string.Empty).Split(',');

        if (int.TryParse(s[0], out this.priorityTime[0]) && int.TryParse(s[1], out this.priorityTime[1]))
        {

        }
        else
            Debug.Log("wrong format in priorityTime");
    }

    public bool CheckCondition()
    {
        Debug.LogWarning("´ýÍê³É");
        return true;
    }

    public void FinishedIncident()
    {
        if (this.remainingTimes > 0)
            this.remainingTimes--;
        if (this.remainingTimes <= 0)
        {
            IncidentSystem.Instance.RemoveIncidentsFromNotFinished(this.incidentName);
        }
    }
}
