using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RelicGameManager : MonoBehaviour
{
    private static RelicGameManager instance;
    public static RelicGameManager Instance { get { return instance; } }

    private RelicStaticLib staticLib;
    public RelicStaticLib StaticLib { get => staticLib;}


    private RelicLib relicLib;
    public RelicLib RelicLib { get => relicLib; }

    public SelectRelicUI selectRelicUI;

    public Text sanityText;
    private int sanity;
    public int Sanity
    {
        set
        {
            sanity = value;
            sanityText.text = string.Format("San÷µ£∫{0:d}", sanity);
        }
        get
        { 
            return sanity;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Sanity = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadRelicLib(Path.Combine(Application.streamingAssetsPath, "RelicLib.xml"));
        //ShowRelicLib();
    }

    private void LoadRelicLib(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        staticLib = new RelicStaticLib(XmlUtilty.Deserialize<RelicLibInfo>(fs));
        relicLib = new RelicLib();
    }

    private void ShowRelicLib()
    {
        foreach (Relic relic in staticLib)
        {
            Debug.Log(relic.relicInfo.Name);
            Debug.Log(relic.relicInfo.Description);
            Debug.Log(relic.relicInfo.Rarity);
            Debug.Log(relic.relicInfo.Category);
        }
    }
}