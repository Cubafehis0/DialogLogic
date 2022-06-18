using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class RelicLibInfo
{
    [XmlArray(ElementName = "relics")]
    [XmlArrayItem(ElementName = "relic")]
    public List<RelicInfo> relics = new List<RelicInfo>();
}