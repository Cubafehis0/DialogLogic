using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using SemanticTree;

public class TestConfigRead : MonoBehaviour
{
    [SerializeField]
    TextAsset text;
    XmlDocument xmlDoc;
    [SerializeField]
    CardPlayerState cardPlayer;
    [SerializeField]
    DialogSystem dialogSystem;
    // Start is called before the first frame update
    void Awake()
    {
        xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.LoadXml(text.text);
            ParseAndConfig();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
    void ParseAndConfig()
    {

        XmlNodeList node = xmlDoc.DocumentElement.ChildNodes;
        //±éÀú½Úµã
        foreach (XmlElement ele in node)
        {
            XmlNodeList list = ele.ChildNodes;
            switch (ele.Name)
            {
                case "InkStory":
                    string inkStoryName = ele.InnerText;
                    dialogSystem.SetInkStoryAsset(inkStoryName);
                    break;
                case "player":
                    cardPlayer.Player.PlayerInfo = TestClass.Deserialize<PlayerInfo>(ele.OuterXml);
                    break;
                case "enemy":
                    Personality enemyPersonality = XmlUtil.GetPersonality(ele["personality"]);

                    break;
            }
        }
    }
}