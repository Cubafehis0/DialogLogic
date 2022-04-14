using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public enum CardCategory
{
    [XmlEnum(Name ="none")]
    None,
    [XmlEnum(Name = "logic")]
    Lgc,
    [XmlEnum(Name = "spirital")]
    Spt,
    [XmlEnum(Name = "moral")]
    Mrl,
    [XmlEnum(Name = "immoral")]
    Imm,
    [XmlEnum(Name = "roundabout")]
    Rdb,
    [XmlEnum(Name = "aggressive")]
    Ags,
}