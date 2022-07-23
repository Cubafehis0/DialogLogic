using System.Xml;
using System;
using System.Xml.Serialization;

[Serializable]
public class GameConfig
{
    [XmlElement(ElementName ="InkStory")]
    public string StoryName;
    [XmlElement(ElementName ="player")]
    public PlayerInfo PlayerInfo;
    [XmlElement(ElementName = "enemy")]
    public Personality enemyPersonality;
}
