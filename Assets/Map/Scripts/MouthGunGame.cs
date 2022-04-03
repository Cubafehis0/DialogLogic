using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GameManager管理类
/// </summary>
public class MouthGunGame : MonoBehaviour
{
    private static MouthGunGame instance = null;
    public MouthGunGame Instance
    {
        get => instance;
    }

    //djc:
    //要么改为属性get，要么写成SerializeField Mono然后拖进来
    //下面这么写全都是null
    public IIncidentSystem incidentSystem = IncidentSystem.Instance;
    public IPlaceSystem placeSystem = PlaceSystem.Instance;
    public IMissionSystem missionSystem = MissionSystem.Instance;
}
