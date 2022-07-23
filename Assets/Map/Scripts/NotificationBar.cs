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
    private List<GameObject> missionPrefabs = new List<GameObject>();

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
        GameManager.Instance.MissionSystem.UpdateMissionSystem();
        foreach (Mission mission in missions)
        {
            GameObject gb = Instantiate(missionPrefab, expandPanelContent.transform);
            gb.GetComponentInChildren<Button>().onClick.AddListener(delegate { OnClickMissionButton(mission); });
            gb.GetComponent<MissionPrefab>().SetName(mission.missionName);
            gb.GetComponent<MissionPrefab>().SetDescription(mission.description);
            missionPrefabs.Add(gb);
        }
    }

    private void ClearMissions()
    {
        foreach (GameObject gb in missionPrefabs)
        {
            gb.SetActive(false);
        }
        missionPrefabs.Clear();
    }

    private void OnClickMissionButton(Mission mission)
    {
        if (mission.CheckFinshedCondition())
        {
            ClearMissions();
            missions.Remove(mission);
            mission.MissionFinished();
            ShowMissions();
        }
    }
}