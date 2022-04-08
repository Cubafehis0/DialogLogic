using System.Collections;
using UnityEngine;


public class Incident : IncidentInfo
{

    private int remainingTimes;

    public bool Finished
    {
        get { return remainingTimes == 0; }
    }

    public Incident() { }
    public Incident(IncidentInfo info) : base(info) 
    {
        remainingTimes = repeatTimes;
    }



    public bool CheckCondition()
    {
        Debug.LogWarning("´ýÍê³É");
        return true;
    }

    public void FinishedIncident()
    {
        if (remainingTimes > 0 && --remainingTimes == 0)
        {
            //IncidentSystem.Instance.RemoveIncidentsFromNotFinished(this.incidentName);
        }
    }
}
