using System;
using System.Xml.Serialization;

[Serializable]
public class RelicInfo
{
    [XmlElement(ElementName = "name")]
    public string Name;

    [XmlElement(ElementName = "description")]
    public string Description;

    [XmlElement(ElementName = "rarity")]
    public int Rarity;

    [XmlElement(ElementName = "category")]
    public PersonalityType Category;
}