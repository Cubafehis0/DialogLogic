using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObject : MonoBehaviour
{
    [SerializeField]
    private List<PlaceObject> placeObjects;

    private Map map = null;

    private void Start()
    {
        map = GameManager.Instance.Map;
        for(int i = 0; i < placeObjects.Count; i++)
        {
            placeObjects[i].gameObject.SetActive(i < map.placesDic.Count);
            if (i < map.placesDic.Count)
            {
                placeObjects[i].place = map.placesDic[i];
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var o = EventSystem.current.currentSelectedGameObject;
            if (o == null) return;
            var c = o.GetComponent<PlaceObject>();
            if (c == null) return;
            GameManager.Instance.EnterPlace(c.place);
        }
    }
}
