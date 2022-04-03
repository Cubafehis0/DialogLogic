using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotificationBar : MonoBehaviour
{
    //djc:prefab里好像没有赋值？
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text incidentText;
    [SerializeField]
    private Button expandButton;
    [SerializeField]
    private GameObject expandPanel;
    [SerializeField]
    private GameObject expandPanelContent;
    [SerializeField]
    private GameObject missionPrefab;


    [SerializeField]
    private List<Mission> missions = new List<Mission>();

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
        expandButton.onClick.AddListener(OnClickExpandButton);
    }

    private void OnClickExpandButton()
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
        foreach (Mission mission in missions)
        {
            GameObject gb = Instantiate(missionPrefab, expandPanelContent.transform);
            gb.GetComponent<Button>().onClick.AddListener(delegate { OnClickMissionButton(mission); });
        }
    }

    private void ClearMissions()
    {
        var childCount = expandPanelContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            Destroy(expandPanelContent.transform.GetChild(i).gameObject);
    }

    private void OnClickMissionButton(Mission mission)
    {
        if (mission.missionState == MissionState.Opened)
        {
            mission.missionState = MissionState.UnderWay;
            mission.CheckMissionState();
        }
        if(mission.missionState == MissionState.CanDeliver)
        {
            mission.missionState = MissionState.Finished;
            missions.Remove(mission);
            mission.CheckMissionState();
        }
    }
}
