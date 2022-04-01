using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardInfoTest
{
    public static string CardInfoTestPath = Path.Combine(Application.dataPath, "Common", "test","CardInfoTest.xml");
    // A Test behaves as an ordinary method
    public static void ArrangeCardEvr()
    {
        GameObject gameObject=new GameObject();
        var playerState=gameObject.AddComponent<CardPlayerState>();
    }
    public List<string> GetAllCardName(XmlNode xmlNode)
    {
        XmlNodeList list = xmlNode.ChildNodes;
        List<string> res = new List<string>();
        foreach(XmlNode n in list)
        {
            res.Add(n["title"].InnerText);
        }
        return res;
    }
    [Test]
    public void CardInfoTestSimplePasses()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CardInfoTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
