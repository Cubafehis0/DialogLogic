using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class IncidentInfo
{
    [XmlElement(ElementName = "name", IsNullable = false)]
    public string incidentName;

    [XmlElement(ElementName = "type")]
    public IncidentType incidentType;

    //[XmlElement(ElementName = "story", IsNullable = false)]
    //public string story;

    [XmlElement(ElementName = "times")]
    public int repeatTimes;

    [XmlArray(ElementName = "prerequisite")]
    public List<string> prerequisites;

    [XmlElement(ElementName = "priority")]
    public int priorityInitial;

    [XmlElement(ElementName = "target")]
    public string target;

    [XmlElement(ElementName = "bonus_start")]
    public int? bonusStartTime;
    [XmlIgnore]
    public bool bonusStartTimeSpecified { get => bonusStartTime != null; }

    [XmlElement(ElementName = "bonus_end")]
    public int? bonusEndTime;
    [XmlIgnore]
    public bool bonusEndTimeSpecified { get => bonusEndTime != null; }

    public IncidentInfo() { }
    public IncidentInfo(IncidentInfo origin)
    {
        incidentName = origin.incidentName;
        incidentType = origin.incidentType;
        target = origin.target;
        repeatTimes = origin.repeatTimes;
        prerequisites = origin.prerequisites;
        priorityInitial = origin.priorityInitial;
        bonusStartTime = origin.bonusStartTime;
        bonusEndTime = origin.bonusEndTime;
    }
}
