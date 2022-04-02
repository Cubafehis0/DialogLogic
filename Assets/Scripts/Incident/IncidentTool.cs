using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IncidentTool
{
    struct DailyIncident
    {
        public int priority;
        public Incident incident;
    }
    public static Incident CalculateIncidents(List<string> incidents)
    {
        Incident nowIncident = null, mainIncident = null, branchIncident = null;
        List<DailyIncident> dailyIncidents = new List<DailyIncident>();
        int nowPriority = 0, branchPriority = 0;

        foreach (string incidentName in incidents)
        {
            nowIncident = IncidentSystem.Instance.GetIncidentByName(incidentName);
            if (nowIncident != null)
            {
                nowPriority = IncidentTool.CalculateIncidentPriority(nowIncident);
                if (nowIncident.incidentType == IncidentType.Main)
                {
                    mainIncident = nowIncident;
                    return mainIncident;
                }
                else if (nowIncident.incidentType == IncidentType.Branch)
                {
                    if (branchPriority <= nowPriority)
                    {
                        branchPriority = nowPriority;
                        branchIncident = nowIncident;
                    }
                }
                else
                {
                    DailyIncident dailyIncident;
                    dailyIncident.priority = nowPriority;
                    dailyIncident.incident = nowIncident;
                    dailyIncidents.Add(dailyIncident);
                }
            }
        }
        if (mainIncident != null)
            return mainIncident;
        else if (branchIncident != null)
            return branchIncident;
        else
        {
            return RandowDailyIncident(dailyIncidents);
        }

    }

    public static int CalculateIncidentPriority(Incident incident)
    {
        int ans = incident.priorityInitial;

        Debug.LogError("待实现");
        return ans;
    }

    private static Incident RandowDailyIncident(List<DailyIncident> dailyIncidents)
    {
        if (dailyIncidents.Count == 0)
            return null;
        else
        {
            Debug.LogError("待实现");
            return dailyIncidents.ToArray()[0].incident;
        }
    }
}
