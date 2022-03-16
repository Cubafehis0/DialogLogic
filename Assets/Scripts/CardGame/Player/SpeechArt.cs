using System;
using UnityEngine;

public enum SpeechType
{
    Normal,
    Cheat,
    Threaten,
    Persuade
}

[Serializable]
public struct SpeechArt
{
    [SerializeField]
    private int normal;
    [SerializeField]
    private int cheat;
    [SerializeField]
    private int threat;
    [SerializeField]
    private int persuade;
    public int this[SpeechType type]
    {
        get
        {
            return type switch
            {
                SpeechType.Normal => normal,
                SpeechType.Cheat => cheat,
                SpeechType.Threaten => threat,
                SpeechType.Persuade => persuade,
                _ => throw new IndexOutOfRangeException(),
            };
        }
        set
        {
            switch (type)
            {
                case SpeechType.Normal:
                    normal = value;
                    break;
                case SpeechType.Cheat:
                    cheat = value;
                    break;
                case SpeechType.Threaten:
                    threat = value;
                    break;
                case SpeechType.Persuade:
                    persuade = value;
                    break;
            }
        }
    }
    public SpeechArt(int normal, int cheat, int threat, int persuade)
    {
        this.normal = normal;
        this.cheat = cheat;
        this.threat = threat;
        this.persuade = persuade;
    }
    public static SpeechArt operator +(SpeechArt a, SpeechArt b)
    {
        SpeechArt ret;
        ret.normal = a.normal + b.normal;
        ret.cheat = a.cheat + b.cheat;
        ret.threat = a.threat + b.threat;
        ret.persuade = a.persuade + b.persuade;
        return ret;
    }
}