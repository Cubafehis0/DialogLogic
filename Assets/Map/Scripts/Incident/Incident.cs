using System;
using UnityEngine;

public enum IncidentEnd
{
    NoEnd, EndA, EndB, EndC
};

[Serializable]
public class Incident : IncidentInfo
{
    [SerializeField]
    public int remainingTimes;

    [SerializeField]
    public IncidentEnd incidentEnd = IncidentEnd.NoEnd;

    public bool Finished
    {
        //get { return true; }
        get { return remainingTimes == 0; }
    }

    public IncidentEnd IncidentEnd
    {
        get => incidentEnd;
    }

    public Incident()
    { }

    public Incident(IncidentInfo info) : base(info)
    {
        remainingTimes = repeatTimes;
    }
}