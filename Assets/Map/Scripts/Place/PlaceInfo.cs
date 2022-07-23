using System.Collections.Generic;
using System.Xml.Serialization;

public class PlaceInfo
{
    [XmlElement(ElementName = "name", IsNullable = false)]
    public string name;
    [XmlArray(ElementName = "incidents")]
    [XmlArrayItem(ElementName = "incident")]
    public List<IncidentInfo> incidents = new List<IncidentInfo>();
}


