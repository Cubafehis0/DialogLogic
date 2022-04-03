using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using System.Xml;
using System.Reflection;
using System;
using System.Xml.Serialization;
using System.IO;
using SemanticTree.PlayerEffect;
using SemanticTree;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset[] xmlFiles;
    // Start is called before the first frame update

    private List<Common> commons = new List<Common>();
    void Start()
    {
        foreach (var file in xmlFiles)
        {
            commons.Add(Deserialize<Common>(file.text));
        }
        //commons.ForEach(x => x.Declare());
        //commons.ForEach(x => x.Define());
        
        //CardGameManager.Instance.StartGame();
    }

    private T Deserialize<T>(string s)
    {
        XmlSerializer ser = new XmlSerializer(typeof(T));
        using var stream = StreamString2Stream(s);
        return (T)ser.Deserialize(stream);
    }

    private static Stream StreamString2Stream(string s)
    {
        MemoryStream stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}