using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObject : MonoBehaviour
{
    [SerializeField]
    private List<PlaceObject> mapObjects;

    private Map map = null;

    private void Start()
    {
        map = GameManager.Instance.map;
        for(int i = 0; i < mapObjects.Count; i++)
        {
            mapObjects[i].gameObject.SetActive(i < map.placesDic.Count);
            if (i < map.placesDic.Count)
            {
                mapObjects[i].place = new Place(map.placesDic[i]);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var o = EventSystem.current.currentSelectedGameObject;
            Debug.Log(o);
            if (o == null) return;
            var c = o.GetComponent<PlaceObject>();
            if (c == null) return;
            c.place.Enter();
        }
    }
}
