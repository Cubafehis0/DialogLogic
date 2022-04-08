using System.Collections.Generic;
using System;
using System.Xml.Serialization;

[Serializable]
public class IncidentInfo
{
    [XmlElement(ElementName = "name", IsNullable = false)]
    public string incidentName;

    [XmlElement(ElementName = "type")]
    public IncidentType incidentType;

    [XmlElement(ElementName = "story", IsNullable = false)]
    public string story;

    [XmlElement(ElementName = "times")]
    public int repeatTimes;

    [XmlIgnore]
    public string incidentPlace;

    [XmlArray(ElementName = "prerequisite")]
    public List<string> conditionMain;

    [XmlElement(ElementName = "priority")]
    public int priorityInitial;

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
        repeatTimes = origin.repeatTimes;
        incidentPlace = origin.incidentPlace;
        conditionMain = origin.conditionMain;
        priorityInitial = origin.priorityInitial;
        bonusStartTime = origin.bonusStartTime;
        bonusEndTime = origin.bonusEndTime;
    }
}
