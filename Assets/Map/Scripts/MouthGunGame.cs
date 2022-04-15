using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GameManagerπ‹¿Ì¿‡
/// </summary>
public class MouthGunGame : MonoBehaviour
{
    [SerializeField]
    private IncidentEntityTable incidentEntityTable;
    [SerializeField]
    private MissionEntityTable missionEntityTable;
    private static MouthGunGame instance = null;
    public MouthGunGame Instance
    {
        get => instance;
    }
    public IIncidentSystem incidentSystem;
    public IPlaceSystem placeSystem;
    public IMissionSystem missionSystem;
    private void Start()
    {
        incidentSystem = IncidentSystem.Instance;
        placeSystem = PlaceSystem.Instance;
        missionSystem = MissionSystem.Instance;
        incidentSystem.InitIncidentSystem(incidentEntityTable.Sheet1);
        missionSystem.InitMissionSystem(missionEntityTable.Sheet1);
    }
    private void Update()
    {
        missionSystem.UpdateMissionSystem();

    }
}
