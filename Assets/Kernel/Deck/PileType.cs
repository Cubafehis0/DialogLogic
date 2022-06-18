using System.Xml.Serialization;

public enum PileType
{
    [XmlEnum(Name = "hand")]
    Hand = 1,
    [XmlEnum(Name = "draw_pile")]
    DrawDeck = 2,
    [XmlEnum(Name = "discard_pile")]
    DiscardDeck = 4,
    [XmlEnum(Name = "all")]
    All = 7
}
