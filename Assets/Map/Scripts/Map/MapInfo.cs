using System.Collections.Generic;
using System.Xml.Serialization;

public class MapInfo
{
    [XmlArray(ElementName = "places")]
    [XmlArrayItem(ElementName = "place")]
    public List<PlaceInfo> places = new List<PlaceInfo>();
}
