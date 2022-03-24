using System;
using System.Xml.Serialization;



[Serializable]
public class SpeechArt
{
    [XmlElement(ElementName = "normal")]
    public int Normal { get; set; }

    [XmlElement(ElementName = "cheat")]
    public int Cheat { get; set; }

    [XmlElement(ElementName = "threaten")]
    public int Threat { get; set; }

    [XmlElement(ElementName = "persuade")]
    public int Persuade { get; set; }

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
    //���ؼӺ������
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
    //����
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