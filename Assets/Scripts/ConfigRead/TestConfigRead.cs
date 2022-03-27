using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;

public class TestConfigRead : MonoBehaviour
{
    [SerializeField]
    TextAsset text;
    XmlDocument xmlDoc;
    [SerializeField]
    CardPlayerState cardPlayer;
    [SerializeField]
    DialogSystem dialogSystem;
    StaticCardLibrary cardLibrary;
    // Start is called before the first frame update
    void Awake()
    {
        cardLibrary = StaticCardLibrary.Instance;
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
                    Personality personality = XmlUtil.GetPersonality(ele["personality"]);
                    cardPlayer.SetBasePersonality(personality);
                    uint maxCardNum = (uint)XmlUtil.GetInt(ele["maxCardNum"]);
                    cardPlayer.SetMaxCardNum(maxCardNum);
                    int drawCardNum = XmlUtil.GetInt(ele["drawCardNum"]);
                    cardPlayer.SetDrawCardNum(drawCardNum);
                    int basePressure = XmlUtil.GetInt(ele["basePressure"]);
                    cardPlayer.SetBasePressure(basePressure);
                    int maxPressure = XmlUtil.GetInt(ele["maxPressure"]);
                    cardPlayer.SetMaxPressure(maxPressure);
                    int health = XmlUtil.GetInt(ele["health"]);
                    cardPlayer.SetHealth(health);
                    int energy = XmlUtil.GetInt(ele["energy"]);
                    cardPlayer.SetBaseEnergy(energy);
                    break;
                case "enemy":
                    Personality enemyPersonality = XmlUtil.GetPersonality(ele["personality"]);

                    break;
                case "cardDeck":
                    foreach (XmlElement e in list)
                    {
                        XmlUtil.ParseCardInfo(e, out string name, out int num);
                        for (int i = 0; i < num; i++)
                        {
                            cardPlayer.Player.CardSet.Add(name);
                        }
                    }
                    break;
            }
        }
    }
}