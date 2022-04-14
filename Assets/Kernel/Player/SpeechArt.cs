using System;
using System.Xml.Serialization;



[Serializable]
public class SpeechArt
{
    [XmlElement(ElementName = "normal")]
    public int Normal { get; set; } = 0;
    [XmlIgnore]
    public bool NormalSpecified { get => Normal != 0; }

    [XmlElement(ElementName = "cheat")]
    public int Cheat { get; set; }
    [XmlIgnore]
    public bool CheatSpecified { get => Cheat != 0; }

    [XmlElement(ElementName = "threaten")]
    public int Threat { get; set; }
    [XmlIgnore]
    public bool ThreatSpecified { get => Threat != 0; }

    [XmlElement(ElementName = "persuade")]
    public int Persuade { get; set; }
    [XmlIgnore]
    public bool PersuadeSpecified { get => Persuade != 0; }

    public int this[SpeechType type]
    {
        get
        {
            return type switch
            {
                SpeechType.Normal => Normal,
                SpeechType.Cheat => Cheat,
                SpeechType.Threaten => Threat,
                SpeechType.Persuade => Persuade,
                _ => throw new IndexOutOfRangeException(),
            };
        }
        set
        {
            switch (type)
            {
                case SpeechType.Normal:
                    Normal = value;
                    break;
                case SpeechType.Cheat:
                    Cheat = value;
                    break;
                case SpeechType.Threaten:
                    Threat = value;
                    break;
                case SpeechType.Persuade:
                    Persuade = value;
                    break;
            }
        }
    }
    public SpeechArt()
    {
        Normal = Cheat = Threat = Persuade = 0;
    }
    public SpeechArt(int normal, int cheat, int threat, int persuade)
    {
        this.Normal = normal;
        this.Cheat = cheat;
        this.Threat = threat;
        this.Persuade = persuade;
    }
    //重载加号运算符
    public SpeechArt(SpeechArt origin)
    {
        this.Normal = origin.Normal;
        this.Cheat = origin.Cheat;
        this.Threat = origin.Threat;
        this.Persuade = origin.Persuade;
    }
    public static SpeechArt operator +(SpeechArt a, SpeechArt b)
    {
        return new SpeechArt
        {
            Normal = a.Normal + b.Normal,
            Cheat = a.Cheat + b.Cheat,
            Threat = a.Threat + b.Threat,
            Persuade = a.Persuade + b.Persuade
        };
    }
    //重载
    public static SpeechArt operator -(SpeechArt a, SpeechArt b)
    {
        return new SpeechArt
        {
            Normal = a.Normal - b.Normal,
            Cheat = a.Cheat - b.Cheat,
            Threat = a.Threat - b.Threat,
            Persuade = a.Persuade - b.Persuade
        };
    }
}