using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
[Serializable]
public class SpeechTypeSpriteDictionary : SerializableDictionary<SpeechType, Sprite> { }

[Serializable]
public class PersonalityTypeSpriteDictionary : SerializableDictionary<PersonalityType, Sprite> { }

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpeechTypeSpriteDictionary choiceSprites;
    [SerializeField]
    private Sprite conditonCover;
    [SerializeField]
    private PersonalityTypeSpriteDictionary conditionIcons;
    [SerializeField]
    private Player localPlayer;
    [SerializeField]
    private string currentStory = null;

    private Map map = null;



    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public PersonalityTypeSpriteDictionary ConditionIcons { get => conditionIcons; set => conditionIcons = value; }
    public SpeechTypeSpriteDictionary ChoiceSprites { get => choiceSprites; set => choiceSprites = value; }
    public Sprite ConditonCover { get => conditonCover; set => conditonCover = value; }
    public Player LocalPlayer { get => localPlayer; set => localPlayer = value; }
    public Map Map { get => map; set => map = value; }
    public string CurrentStory { get => currentStory; set => currentStory = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            localPlayer=GetComponent<Player>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadAllCommon(Path.Combine(Application.streamingAssetsPath, "CardLib"));
        LoadGameConfig(Path.Combine(Application.streamingAssetsPath, "GameConfig.xml"));
        Loadmaps(Path.Combine(Application.streamingAssetsPath, "Map.xml"));
    }

    private void Update()
    {
        instance = this;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void LoadAllCommon(string path)
    {
        string[] files = Directory.GetFiles(path);
        List<Common> commons = new List<Common>();
        foreach (string file in files)
        {
            if (file.EndsWith(".xml"))
            {
                using FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                commons.Add(XmlUtilty.Deserialize<Common>(fs));
            }
        }
        foreach (Common common in commons)
        {
            StaticCardLibrary.Instance.DeclareCard(common.CardInfos);
            foreach (Status status in common.Statuss)
            {
                StaticStatusLibrary.DeclareStatus(status.Name, status);
            }
        }
        StaticCardLibrary.Instance.Construct();
        foreach (Common common in commons)
        {
            foreach (Status status in common.Statuss)
            {
                status.Construct();
            }
        }
    }
    private void LoadGameConfig(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        GameConfig config = XmlUtilty.Deserialize<GameConfig>(fs);
        LocalPlayer.PlayerInfo = config.PlayerInfo;
    }

    private void Loadmaps(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        Map = new Map(XmlUtilty.Deserialize<MapInfo>(fs));
    }

    public void EnterPlace(Place place)
    {
        EnterIncident(IncidentTool.Pickup(place.incidents));
    }

    public void EnterIncident(Incident incident)
    {
        CurrentStory = incident.incidentName;
        SceneManager.LoadScene("ControllerSampleScene");
    }

    public void EnterStory(string story)
    {
        CurrentStory = story; 
        SceneManager.LoadScene("ControllerSampleScene");
    }

}