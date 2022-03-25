using SemanticTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;




[Serializable]
public class CardInfo
{
    [XmlElement(ElementName = "name")]
    public string Name;

    [XmlElement(ElementName = "title")]
    public string Title;

    [XmlElement(ElementName = "condition_desc")]
    public string ConditionDesc;

    [XmlElement(ElementName = "effect_desc")]
    public string EffectDesc;

    [XmlElement(ElementName = "meme")]
    public string Meme;

    [XmlElement(ElementName = "cost")]
    public int BaseCost;

    [XmlElement(ElementName = "category")]
    public int category;

    [XmlElement(ElementName = "in_hand_personality")]
    public Personality Personality;

    [XmlElement(ElementName = "on_play_card")]
    public EffectList Effects;
}
