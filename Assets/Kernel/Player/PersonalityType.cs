using System.Xml.Serialization;

public enum PersonalityType
{
    [XmlEnum(Name ="inside")]
    Inside = 6,
    [XmlEnum(Name = "outside")]
    Outside = 7,
    [XmlEnum(Name = "logic")]
    Logic = 2,
    [XmlEnum(Name = "passion")]
    Passion = 3,
    [XmlEnum(Name = "moral")]
    Moral = 0,
    [XmlEnum(Name = "unethic")]
    Unethic = 1,
    [XmlEnum(Name = "detour")]
    Detour = 4,
    [XmlEnum(Name = "strong")]
    Strong = 5
}

