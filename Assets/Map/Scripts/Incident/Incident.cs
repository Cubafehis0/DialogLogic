using System;
using UnityEngine;

[Serializable]
public class Incident : IncidentInfo
{
    [SerializeField]
    public int remainingTimes;

    public bool Finished
    {
        get { return remainingTimes == 0; }
    }

    public Incident() { }
    public Incident(IncidentInfo info) : base(info)
    {
        remainingTimes = repeatTimes;
    }
}
