using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObject : MonoBehaviour
{
    public Map placesDic = null;

    public void OnEnable()
    {
        if (placesDic == null) enabled = false;
    }

    public void Construct(MapInfo mapInfo)
    {
        placesDic = new Map(mapInfo);
        enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var o = EventSystem.current.currentSelectedGameObject;
            if (o == null) return;
            var c = o.GetComponent<PlaceObject>();
            if (c == null) return;
            c.placeIncident.Enter();
        }
    }
}
