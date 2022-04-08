using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SemanticTree;


public class ConfigRead : MonoBehaviour
{
    public static void LoadAll()
    {
        LoadGameConfig(Path.Combine(Application.streamingAssetsPath, "GameConfig.xml"));
        LoadAllCommon(Path.Combine(Application.streamingAssetsPath, "CardLib"));
    }

    private static void LoadAllCommon(string path)
    {
        string[] files = Directory.GetFiles(path);
        List<Common> commons = new List<Common>();
        foreach (string file in files)
        {
            if (file.EndsWith(".xml"))
            {
                using FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                commons.Add(GameManager.Deserialize<Common>(fs));
            }
        }
        commons.ForEach(x => x.Declare());
        commons.ForEach(x => x.Define());
    }

    private static void LoadGameConfig(string path)
    {
        using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        GameConfig GameConfig = GameManager.Deserialize<GameConfig>(fs);
        CardGameManager.Instance.SetGameConfig(GameConfig);
    }
}
