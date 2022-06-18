using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class NotificationBar : MonoBehaviour
{
    //djc:prefab里好像没有赋值？
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text incidentText;
    [SerializeField]
    private GameObject expandPanel;
    [SerializeField]
    private Transform expandPanelContent;
    [SerializeField]
    private GameObject missionPrefab;


    [SerializeField]
    private List<Mission> missions = new List<Mission>();

    private List<GameObject> missionObjects = new List<GameObject>();
    private static NotificationBar instance = null;
    public static NotificationBar Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        expandPanel.SetActive(false);
    }

    public void ExpandMissionPanel()
    {
        if (expandPanel.activeSelf)
        {
            expandPanel.SetActive(false);
            ClearMissions();
        }
        else
        {
            expandPanel.SetActive(true);
            ShowMissions();
        }

    }

    public void AddMissionToExpandButton(Mission mission)
    {
        if (!missions.Contains(mission))
            missions.Add(mission);

    }

    private void ShowMissions()
    {
        for (int i = missionObjects.Count; i < missions.Count; i++)
        {
            GameObject gb = Instantiate(missionPrefab, expandPanelContent);
            gb.GetComponent<Button>().onClick.AddListener(OnClickMissionButton);
            missionObjects.Add(gb);
        }
        for (int i = 0; i < missionObjects.Count; i++)
        {
            missionObjects[i].SetActive(i < missions.Count);
        }
    }

    private void ClearMissions()
    {
        var childCount = expandPanelContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            Destroy(expandPanelContent.transform.GetChild(i).gameObject);
    }

    private void OnClickMissionButton()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if (go != null && missionObjects.Contains(go))
            OnClickMissionButton(missionObjects.IndexOf(go));
    }

    private void OnClickMissionButton(int index)
    {
        OnClickMissionButton(missions[index]);
    }

    private void OnClickMissionButton(Mission mission)
    {
        if (mission.missionState == MissionState.Opened)
        {
            mission.missionState = MissionState.UnderWay;
            mission.CheckMissionState();
        }
        if (mission.missionState == MissionState.CanDeliver)
        {
            mission.missionState = MissionState.Finished;
            missions.Remove(mission);
            mission.CheckMissionState();
        }
    }
}
