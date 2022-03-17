using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
public class TestConfigRead : MonoBehaviour
{
    [SerializeField]
    string levelName;
    string filePath;
    XmlDocument xmlDoc;
    CardPlayerState cardPlayer;
    StaticCardLibrary cardLibrary;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        xmlDoc = new XmlDocument();
        cardLibrary = StaticCardLibrary.Instance;
        cardPlayer = CardPlayerState.Instance;
        filePath = Application.dataPath + "/GameConfig/" + levelName + ".xml";
        if (File.Exists(filePath))
        {
            xmlDoc.Load(filePath);
            ParseAndConfig();
        }
    }
    void ParseAndConfig()
    {
        
        XmlNodeList node = xmlDoc.SelectSingleNode("pass").ChildNodes;
        //±éÀú½Úµã
        foreach (XmlElement ele in node)
        {
            XmlNodeList list = ele.ChildNodes;
            switch (ele.Name)
            {
                case "player":
                    foreach(XmlElement e in list)
                    {
                        switch(e.Name)
                        {
                            case "character":
                                int[] character = XmlUtil.GetCharacter(e);
                                Debug.Log(character);
                                //cardPlayer.Character.Personality = character;
                                break;
                            case "maxCardNum":
                                int maxCardNum = XmlUtil.GetInt(e);
                                Debug.Log(maxCardNum);
                                //cardPlayer.maxCardNum = maxCardNum;
                                break;
                            case "drawCardNum":
                                int drawCardNum = XmlUtil.GetInt(e);
                                Debug.Log(drawCardNum);
                                //cardPlayer.drawCardNum = drawCardNum;
                                break;
                            case "health":
                                int health = XmlUtil.GetInt(e);
                                Debug.Log(health);
                                //cardPlayer.Player.Health = health;
                                break;
                            case "energy":
                                int energy = XmlUtil.GetInt(e);
                                Debug.Log(energy);
                                //cardPlayer.Energy = energy;
                                break;
                        }       
                    }
                    break;
                case "cardDeck":
                    foreach(XmlElement e in list)
                    {
                        XmlUtil.GetCardInfo(e, out string name, out int num);
                        int cardID = 0;
                        //int cardID=cardLibrary.GetCardIDbyName(name);
                        if(cardID>=0)
                        {
                            for (int i = 0; i < num; i++)
                            {
                                //cardPlayer.Player.CardSet.Add(cardID);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
