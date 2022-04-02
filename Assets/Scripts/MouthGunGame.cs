using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthGunGame : MonoBehaviour
{
    private static MouthGunGame instance = null;
    public MouthGunGame Instance
    {
        get => instance;
    }

    public IIncidentSystem incidentSystem = IncidentSystem.Instance;
    public IPlaceSystem placeSystem = PlaceSystem.Instance;
    public IMissionSystem missionSystem = MissionSystem.Instance;

    
}
