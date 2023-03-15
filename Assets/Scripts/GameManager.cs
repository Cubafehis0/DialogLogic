using ModdingAPI;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite conditonCover;
    [SerializeField]
    private PlayerPacked localPlayer;

    public Incident currentIncident = null;
    private Map map = null;



    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public DynamicLibrary CardObjectLibrary { get; set; }
    public Sprite ConditonCover { get => conditonCover; set => conditonCover = value; }
    public PlayerPacked LocalPlayer { get => localPlayer; set => localPlayer = value; }
    public Map Map { get => map; }
    public string CurrentStory { get => currentIncident.incidentName; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Context.Console = GetComponent<GameConsole>();
        Context.Query = GetComponent<GameQuery>();
        Context.Rule = new Kernel.GameRule();
        LoadAllCommon(Path.Combine(Application.streamingAssetsPath, "TestCardLib"));
        LoadGameConfig(Path.Combine(Application.streamingAssetsPath, "TestGameConfig.xml"));
        Loadmaps(Path.Combine(Application.streamingAssetsPath, "Map.xml"));
        localPlayer.Health = localPlayer.MaxHealth;
    }

    private void Update()
    {
        instance = this;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void LoadAllCommon(string path)
    {
        JasperMod.Loader.LoadAllCommon(path);
    }
    private void LoadGameConfig(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        GameConfig config = XmlUtilties.XmlUtilty.Deserialize<GameConfig>(fs);
        LocalPlayer.Load(config.PlayerInfo);
    }

    private void Loadmaps(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        map = new Map(XmlUtilties.XmlUtilty.Deserialize<MapInfo>(fs));
    }

    public bool EnterPlace(Place place)
    {
        Incident incident = IncidentTool.Pickup(place.incidents);
        if (incident != null)
        {
            EnterIncident(incident);
            return true;
        }
        else
        {
            Debug.Log("无可用事件");
            return false;
        }
    }

    public void EnterIncident(Incident incident)
    {
        currentIncident = incident;
        SceneManager.LoadScene("ControllerSampleScene");
    }

    public void CompleteCurrentIncident()
    {
        currentIncident.remainingTimes--;
        SceneManager.LoadScene("PlaceScene");
    }
}