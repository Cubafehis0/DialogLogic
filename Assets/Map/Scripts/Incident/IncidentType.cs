using System.Xml.Serialization;

public enum IncidentType
{
    [XmlEnum(Name = "main")]
    Main,
    [XmlEnum(Name = "branch")]
    Branch,
    [XmlEnum(Name = "daily")]
    Daily
}