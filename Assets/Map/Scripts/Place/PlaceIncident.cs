using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaceIncident
{
    [SerializeField]
    protected List<string> incidents = new List<string>();

    public string name;

    public PlaceIncident(string name)
    {
        this.name = name;
        PlaceSystem.Instance.AddPlace(this);
    }

    public string GetIncident()
    {
        Debug.Log(IncidentTool.CalculateIncidents(incidents));
        return IncidentTool.CalculateIncidents(incidents).incidentName;
    }

    public void AddIncident(string incidentName)
    {
        if (incidents.Contains(incidentName))
        {
            Debug.Log(name + " contains incident:" + incidentName);
        }
        else
        {
            incidents.Add(incidentName);
            Debug.Log(name + " add incident:" + incidentName);
        }
    }
    public void RemoveIncident(string incidentName)
    {
        if (incidents.Contains(incidentName))
        {
            incidents.Remove(incidentName);
        }
        else
        {
            Debug.Log(name + " doesn't contain incident:" + incidentName);
        }
    }

    public void CheckPlaceIncidents()
    {

    }
}


