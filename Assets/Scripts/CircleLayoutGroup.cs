using System.Collections.Generic;
using UnityEngine;

public class CircleLayoutGroup : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> children;
    [SerializeField]
    private List<GameObject> nowActiveChildren;
    [SerializeField]
    private float cricleRadius;
    [SerializeField]

    private void Awake()
    {
        UpdateLayout();
    }
    public void UpdateLayout()
    {
        GetActiveGameObject();
        SetChildrenPos(CalculatePos(nowActiveChildren.Count));
    }

    private void OnValidate()
    {
        UpdateLayout();
    }

    private void SetChildrenPos(List<Vector2> pos)
    {
        for (int i = 0; i < nowActiveChildren.Count; i++)
        {
            nowActiveChildren[i].transform.position = pos[i];
            //Debug.Log($"pos{i}:{pos[i]}");
        }
    }
    private void GetActiveGameObject()
    {
        List<GameObject> gbList = new List<GameObject>();
        foreach (GameObject gb in children)
        {
            if (gb.activeSelf)
            {
                gbList.Add(gb);
            }
        }
        nowActiveChildren = gbList;
    }
    private List<Vector2> CalculatePos(int num)
    {
        List<Vector2> posList = new List<Vector2>();
        Vector2 gbpos;
        float angle = 2f / num * Mathf.PI;
        Vector2 pos = transform.position;
        for (int i = 0; i < num; i++)
        {
            gbpos.x = cricleRadius * Mathf.Cos(angle * i) + pos.x;
            gbpos.y = cricleRadius * Mathf.Sin(angle * i) + pos.y;
            posList.Add(gbpos);
        }
        return posList;
    }

}
