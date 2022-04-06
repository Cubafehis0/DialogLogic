using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPlaceSystem
{
    /// <summary>
    /// 将事件添加到相应地点
    /// </summary>
    /// <param name="incident"></param>
    /// <param name="placeName"></param>
    public void AddIncidentToPlace(string incident, string placeName);

    public void AddIncidentToPlace(List<Incident> incident, List<string> placeName);
    public void AddPlace(PlaceIncident placeIncident);
}
public class PlaceSystem : MonoBehaviour, IPlaceSystem
{
    private Dictionary<string, PlaceIncident> placesDic = new Dictionary<string, PlaceIncident>();

    private static IPlaceSystem instance = null;
    public static IPlaceSystem Instance
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
    public void AddPlace(PlaceIncident placeIncident)
    {
        if (!placesDic.ContainsKey(placeIncident.name))
        {
            placesDic.Add(placeIncident.name, placeIncident);
        }
    }
    public void AddIncidentToPlace(string incident,string placeName)
    {    
        if (placesDic != null)
        {
            if(placesDic.TryGetValue(placeName, out PlaceIncident placeIncident))
            {
                placeIncident.AddIncident(incident);
            }
           
        }
    }


    public void AddIncidentToPlace(List<Incident> incident, List<string> placeName)
    {
        
    }
}
