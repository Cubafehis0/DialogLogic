using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class MissionPrefab : MonoBehaviour
{
    [SerializeField]
    public Text missionName;
    [SerializeField]
    public Text description;

    public void SetDescription(string s)
    {
        description.text = s;
    }
    public void SetName(string s)
    {
        missionName.text = s;
    }
}
