using System;
using System.Xml.Serialization;
using SemanticTree;
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

    [XmlElement(ElementName = "init_num")]
    public int InitNum;

    [XmlElement(ElementName = "on_pick_up")]
    public EffectList OnPickUp;

    [XmlElement(ElementName = "binding_modifier")]
    public Modifier modifier;

    [XmlElement(ElementName = "on_value_change")]
    public EffectList OnValueChange;
}