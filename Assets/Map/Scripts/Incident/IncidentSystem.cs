using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IIncidentSystem
{
    public void CheckIncident();
    public Incident GetIncidentByName(string incidentName);
    public void RemoveIncidentsFromNotFinished(string name);
    public void RemoveIncidentsFromAll(string name);
    public bool IsFinishedAllIncidents(List<string> incidents);
    public bool IsFinishedIncident(string name);
    public void AddIncident(Incident incident);
    public void AddIncident(List<Incident> incidents);
}
public class IncidentSystem : MonoBehaviour, IIncidentSystem
{
    private Dictionary<string, Incident> incidentsAll = new Dictionary<string, Incident>();
    private Dictionary<string, Incident> incidentsNotFinshed = new Dictionary<string, Incident>();
    private Dictionary<string, Incident> incidentsFinshed = new Dictionary<string, Incident>();

    private static IIncidentSystem instance = null;
    public static IIncidentSystem Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void CheckIncident()
    {
        Dictionary<string, Incident>.ValueCollection incidents = this.incidentsNotFinshed.Values;
        foreach (Incident incident in incidents)
        {
            if (incident.CheckCondition())
            {
                PlaceSystem.Instance.AddIncidentToPlace(incident.incidentName, incident.incidentPlace);
            }
        }
    }
    public Incident GetIncidentByName(string incidentName)
    {
        if (incidentsAll.TryGetValue(incidentName, out Incident incident))
        {
            return incident;
        }
        Debug.LogWarning("didn't find incident named " + incidentName);
        return null;
    }
    public void AddIncident(Incident incident)
    {
        if (!incidentsAll.ContainsKey(incident.incidentName))
        {
            incidentsAll.Add(incident.incidentName, incident);
            if (incident.remainingTimes > 0)
                incidentsNotFinshed.Add(incident.incidentName, incident);
            else 
                incidentsFinshed.Add(incident.incidentName, incident);
        }

    }

    public void AddIncident(List<Incident> incidents)
    {
        foreach(Incident incident in incidents)
        {
            AddIncident(incident);
        }
    }
    public void RemoveIncidentsFromNotFinished(string name)
    {
        if (incidentsNotFinshed.TryGetValue(name, out Incident incident))
        {
            incidentsFinshed.Add(name, incident);
            incidentsNotFinshed.Remove(name);
        }
        else Debug.LogError("未找到事件" + name);
    }

    public void RemoveIncidentsFromAll(string name)
    {
        if (incidentsAll.ContainsKey(name))
        {
            incidentsFinshed.Remove(name);
            incidentsNotFinshed.Remove(name);
            incidentsAll.Remove(name);
        }
        else Debug.LogError("未找到事件" + name);
    }


    public bool IsFinishedAllIncidents(List<string> incidents)
    {
        foreach (string name in incidents)
        {
            if (incidentsFinshed.ContainsKey(name))
            {

            }
            else
                return false;
        }
        return true;
    }

    public bool IsFinishedIncident(string name)
    {
        return incidentsFinshed.ContainsKey(name);
    }


}
