using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GameManager������
/// </summary>
public class MouthGunGame : MonoBehaviour
{
    private static MouthGunGame instance = null;
    public MouthGunGame Instance
    {
        get => instance;
    }

    //djc:
    //Ҫô��Ϊ����get��Ҫôд��SerializeField MonoȻ���Ͻ���
    //������ôдȫ����null
    public IIncidentSystem incidentSystem = IncidentSystem.Instance;
    public IPlaceSystem placeSystem = PlaceSystem.Instance;
    public IMissionSystem missionSystem = MissionSystem.Instance;
}
