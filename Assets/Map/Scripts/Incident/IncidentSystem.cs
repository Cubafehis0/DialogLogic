using System.Collections.Generic;
using UnityEngine;

public interface IIncidentSystem
{
    public void AddIncident(Incident incident);

    public void AddIncident(List<Incident> incidents);

    public void AddIncidentEntity(List<IncidentEntity> incidentEntities);

    public void CheckIncident();

    public string GetIncident();

    public Incident GetIncidentByName(string incidentName);

    public void InitIncidentSystem(List<IncidentEntity> incidentEntities);

    public bool IsFinishedAllIncidents(List<string> incidents);

    public bool IsFinishedIncident(string name);

    public void RemoveIncidentsFromAll(string name);

    public void RemoveIncidentsFromNotFinished(string name);
}

public class IncidentSystem : MonoBehaviour, IIncidentSystem
{
    private static IIncidentSystem instance = null;
    private Dictionary<string, Incident> incidentsAll = new Dictionary<string, Incident>();
    private Dictionary<string, Incident> incidentsFinshed = new Dictionary<string, Incident>();
    private Dictionary<string, Incident> incidentsNotFinished = new Dictionary<string, Incident>();

    public static IIncidentSystem Instance
    {
        get => instance;
    }

    public void AddIncident(Incident incident)
    {
        if (!incidentsAll.ContainsKey(incident.incidentName))
        {
            incidentsAll.Add(incident.incidentName, incident);
            if (incident.remainingTimes > 0)
                incidentsNotFinished.Add(incident.incidentName, incident);
            else
                incidentsFinshed.Add(incident.incidentName, incident);
        }
    }

    public void AddIncident(List<Incident> incidents)
    {
        foreach (Incident incident in incidents)
        {
            AddIncident(incident);
        }
    }

    public void AddIncidentEntity(List<IncidentEntity> incidentEntities)
    {
        foreach (IncidentEntity incidentEntity in incidentEntities)
        {
            Incident incident = new Incident(incidentEntity);
            incidentsAll.Add(incident.incidentName, incident);
            incidentsNotFinished.Add(incident.incidentName, incident);
        }
    }

    public void CheckIncident()
    {
        Dictionary<string, Incident>.ValueCollection incidents = this.incidentsNotFinished.Values;
        foreach (Incident incident in incidents)
        {
            if (incident.CheckCondition())
            {
                PlaceSystem.Instance.AddIncidentToPlace(incident.incidentName, incident.incidentPlace);
            }
        }
    }

    public string GetIncident()
    {
        List<Incident> list = new List<Incident>();
        foreach (Incident incident in incidentsNotFinished.Values)
        {
            if (incident.CheckCondition())
            {
                list.Add(incident);
            }
        }
        return IncidentTool.CalculateIncidents(list).incidentName;
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

    public void InitIncidentSystem(List<IncidentEntity> incidentEntities)
    {
        AddIncidentEntity(incidentEntities);
    }

    public bool IsFinishedAllIncidents(List<string> incidents)
    {
        Debug.LogWarning("test");
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

    public void RemoveIncidentsFromAll(string name)
    {
        if (incidentsAll.ContainsKey(name))
        {
            incidentsFinshed.Remove(name);
            incidentsNotFinished.Remove(name);
            incidentsAll.Remove(name);
        }
        else Debug.LogError("未找到事件" + name);
    }

    public void RemoveIncidentsFromNotFinished(string name)
    {
        if (incidentsNotFinished.TryGetValue(name, out Incident incident))
        {
            incidentsFinshed.Add(name, incident);
            incidentsNotFinished.Remove(name);
            PlaceSystem.Instance.RemoveIncidentToPlace(name, incident.incidentPlace);
        }
        else Debug.LogError("未找到事件" + name);
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
}