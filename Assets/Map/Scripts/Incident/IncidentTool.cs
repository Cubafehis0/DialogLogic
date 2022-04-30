using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class IncidentTool
{
    public static Incident Pickup(List<Incident> incidents)
    {
        return PickupMain(incidents) ??
                PickupBranch(incidents) ??
                RandowDailyIncident(PickupAllDaily(incidents)) ??
                null;
    }

    public static Incident PickupMain(List<Incident> incidents)
    {
        var res = from incident in incidents
                  where incident.incidentType == IncidentType.Main
                  && incident.Finished == false
                  && CheckPrerequisite(incident)
                  select incident;
        if (res.Count() > 1)
        {
            Debug.LogWarning("主线不是单线");
        }
        return res.Count() > 0 ? res.First() : null;
    }

    public static Incident PickupBranch(List<Incident> incidents)
    {
        var res = from incident in incidents
                  where incident.incidentType == IncidentType.Branch
                  && incident.Finished == false
                  && CheckPrerequisite(incident)
                  orderby CalculateIncidentPriority(incident) descending
                  select incident;
        return res.Count() > 0 ? res.First() : null;
    }

    public static IEnumerable<Incident> PickupAllDaily(List<Incident> incidents)
    {
        return from incident in incidents
               where incident.incidentType == IncidentType.Daily
               select incident;
    }

    public static bool IsFinishedAllIncidents(List<Incident> incidents)
    {
        return incidents.TrueForAll(x => x.Finished);
    }


    public static int CalculateIncidentPriority(Incident incident)
    {
        int ans = incident.priorityInitial;
        Debug.LogError("待实现");
        return ans;
    }

    private static Incident RandowDailyIncident(IEnumerable<Incident> dailyIncidents)
    {
        if (dailyIncidents == null || dailyIncidents.Count() == 0) return null;
        Debug.LogError("待实现");
        return dailyIncidents.First();
    }

    private static bool CheckPrerequisite(Incident incident)
    {
        foreach (string name in incident.prerequisites)
        {
            if (GameManager.Instance.Map.TryFindIncident(name, out incident))
            {
                if (!incident.Finished) return false;
            }
            else
            {
                Debug.LogWarning("未识别的先决条件");
            }
        }
        return true;
    }
}
