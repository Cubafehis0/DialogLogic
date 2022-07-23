using System.Collections.Generic;
using System.Xml.Serialization;

public class MissionInfo
{
    public struct Condition
    {
        public Incident incident;
        public IncidentEnd incidentEnd;
    }

    [XmlIgnore]
    public MissionState missionState = MissionState.Unopened;

    public List<Condition> start;
    public List<Condition> end;

    [XmlElement(ElementName = "name")]
    public string missionName;

    //[XmlElement(ElementName = "type")]
    //public MissionType missionType;

    [XmlElement(ElementName = "description")]
    public string description;

    [XmlArray(ElementName = "ConditionsStart")]
    [XmlArrayItem(ElementName = "conditionStart")]
    public List<string> conditionStart;

    [XmlArray(ElementName = "ConditionsEnd")]
    [XmlArrayItem(ElementName = "conditionEnd")]
    public List<string> conditionEnd;

    //[XmlElement(ElementName = "promulgator")]
    ////·¢²¼Õß
    //public string promulgator;

    //[XmlArray(ElementName = "relevantIncident")]
    //public List<string> relevantIncidentName;
    //[XmlIgnore]
    //public List<Incident> relevantIncident;

    //[XmlElement(ElementName = "triggerDate")]
    //public int triggerDate;

    //[XmlElement(ElementName = "triggerIncident")]
    //private List<string> triggerIncidentName;
    //[XmlIgnore]
    //public List<Incident> triggerIncident;

    //[XmlElement(ElementName = "conditionIncident")]
    //private List<string> conditionIncidentName;
    //[XmlIgnore]
    //public List<Incident> conditionIncident;

    //[XmlElement(ElementName = "conditionKey")]
    //public List<string> conditionKey;

    //[XmlElement(ElementName = "conditionFavor")]
    //public int conditionFavor;

    //[XmlElement(ElementName = "conditionReputation")]
    //public List<int> conditionReputation;

    //[XmlElement(ElementName = "deadline")]
    //public int deadline;

    //[XmlElement(ElementName = "deadIncident")]
    //private List<string> deadIncidentName;
    //[XmlIgnore]
    //public List<Incident> deadIncident;

    //[XmlElement(ElementName = "missionKey")]
    //public List<string> missionKey;

    //[XmlElement(ElementName = "missionIncident")]
    //private List<string> missionIncidentName;
    //[XmlIgnore]
    //public List<Incident> missionIncident;

    [XmlIgnore]
    public Award award;

    public MissionInfo()
    { }

    public MissionInfo(MissionInfo origin)
    {
        missionName = origin.missionName;
        //missionType = origin.missionType;
        description = origin.description;
        conditionStart = origin.conditionStart;
        conditionEnd = origin.conditionEnd;
        start = StringToCondition(conditionStart);
        end = StringToCondition(conditionEnd);
        //promulgator = origin.promulgator;

        //relevantIncidentName = origin.relevantIncidentName;
        //relevantIncident = SetIncident(relevantIncidentName);

        //triggerDate = origin.triggerDate;
        //triggerIncidentName = origin.triggerIncidentName;
        //triggerIncident = SetIncident(triggerIncidentName);

        //conditionIncidentName = origin.conditionIncidentName;
        //conditionIncident = SetIncident(conditionIncidentName);
        //conditionKey = origin.conditionKey;
        //conditionFavor = origin.conditionFavor;
        //conditionReputation = origin.conditionReputation;

        //deadline = origin.deadline;
        //deadIncidentName = origin.deadIncidentName;

        //missionKey = origin.missionKey;
        //missionIncidentName = origin.missionIncidentName;
        //missionIncident = SetIncident(missionIncidentName);
    }

    public override string ToString()
    {
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        s.Append("conditionStart:");
        foreach (string c in conditionStart)
            s.Append(c + " ");
        s.Append("conditionEnd:");
        foreach (string c in conditionEnd)
            s.Append(c + " ");
        return "name:" + missionName + " " + "description:" + description + " " + s;
    }

    private static List<Incident> SetIncident(List<string> names)
    {
        if (names == null || names.Count == 0)
            return null;
        List<Incident> incidents = new List<Incident>();
        foreach (string name in names)
            incidents.Add(SetIncident(name));
        return incidents;
    }

    private static Incident SetIncident(string name)
    {
        GameManager.Instance.Map.TryFindIncident(name, out Incident res);
        return res;
    }

    private static List<Condition> StringToCondition(List<string> s)
    {
        List<Condition> c = new List<Condition>();
        foreach (string res in s)
        {
            c.Add(StringToCondition(res));
        }
        return c;
    }

    private static Condition StringToCondition(string s)
    {
        string[] res = s.Split(':');
        Condition c;
        c.incident = SetIncident(res[0]);
        c.incidentEnd = IncidentEnd.EndA;
        if (res.Length > 1)
        {
            c.incidentEnd = res[1] switch
            {
                "EndA" => IncidentEnd.EndA,
                "EndB" => IncidentEnd.EndB,
                "EndC" => IncidentEnd.EndC,
                _ => throw new System.NotImplementedException(),
            };
        }
        return c;
    }
}