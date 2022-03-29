using SemanticTree;
using SemanticTree.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;




[Serializable]
public class CardInfo
{
    [XmlElement(ElementName = "name", IsNullable = true)]
    public string Name;

    [XmlElement(ElementName = "title", IsNullable = true)]
    public string Title;

    [XmlElement(ElementName = "condition_desc", IsNullable = true)]
    public string ConditionDesc;

    [XmlElement(ElementName = "effect_desc", IsNullable = true)]
    public string EffectDesc;

    [XmlElement(ElementName = "meme", IsNullable = true)]
    public string Meme;

    [XmlElement(ElementName = "cost")]
    public int BaseCost;

    [XmlElement(ElementName = "category")]
    public int category;

    [XmlElement(ElementName = "in_hand_personality")]
    public Personality Personality;

    [XmlElement(ElementName = "requirements")]
    public AllNode Requirements;

    [XmlElement(ElementName = "on_draw")]
    public EffectList DrawEffects;

    [XmlElement(ElementName = "on_play_card")]
    public EffectList Effects;

    public void Construct()
    {
        Requirements?.Construct();
        DrawEffects?.Construct();
        Effects?.Construct();
    }
}
