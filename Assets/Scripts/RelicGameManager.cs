using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RelicGameManager : MonoBehaviour
{
    private static RelicGameManager instance;
    public static RelicGameManager Instance { get { return instance; } }


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
        //using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //staticLib = new RelicStaticLib(XmlUtilties.XmlUtilty.Deserialize<RelicLibInfo>(fs));
        //relicLib = new RelicLib();
    }

    private void ShowRelicLib()
    {
        foreach (Relic relic in StaticRelicLibrary.Instance)
        {
            Debug.Log(relic.Name);
            Debug.Log(relic.Description);
            Debug.Log(relic.Rarity);
            Debug.Log(relic.Category);
        }
    }
}