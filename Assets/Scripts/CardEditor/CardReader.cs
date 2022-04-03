using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class CardReader
{
    public static CardReader Instance
    {
        get
        {
            if(instance == null)    
                instance = new CardReader();
            return instance;
        }
    }

    private static CardReader instance;
    private CardReader()
    {

    }
    public List<CardInfo> GetCardInfos(TextAsset[] xmlFiles)
    {
        List<CardInfo> list = new List<CardInfo>();
        foreach (var file in xmlFiles)
        {
            Common common = Deserialize<Common>(file.text);
            list.AddRange(common.CardInfos);
        }
        return list;
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
    private T Deserialize<T>(string s)
    {
        XmlSerializer ser = new XmlSerializer(typeof(T));
        using var stream = StreamString2Stream(s);
        return (T)ser.Deserialize(stream);
    }

}
