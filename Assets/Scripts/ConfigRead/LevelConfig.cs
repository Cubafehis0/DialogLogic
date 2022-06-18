using ModdingAPI;
using System;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class LevelConfig : ScriptableObject
{
    [XmlElement(ElementName = "player_character")]
    public Personality playerCharacter;

    [XmlElement(ElementName = "max_hand_num")]
    public int maxCardNum;

    [XmlElement(ElementName = "draw_num")]
    public int drawCardNum;
}
