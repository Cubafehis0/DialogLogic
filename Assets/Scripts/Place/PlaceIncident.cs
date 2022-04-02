using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlaceIncident
{
    public string GetIncident();
    public void AddIncident(string incidentName);
    public void DeleteIncident(string incidentName);

}
public  class PlaceIncident : IPlaceIncident
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
        //throw new System.NotImplementedException();
        return "ControllerSampleScene";
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
    public void DeleteIncident(string incidentName)
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


