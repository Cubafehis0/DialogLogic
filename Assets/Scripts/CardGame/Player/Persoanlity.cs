using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;



public enum PersonalityType
{
    Inside,
    Outside,
    Logic,
    Passion,
    Moral,
    Unethic,
    Detour,
    Strong
}

[Serializable]
public class Personality
{
    [SerializeField]
    private int p0;
    [SerializeField]
    private int p1;
    [SerializeField]
    private int p2;
    [SerializeField]
    private int p3;

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => p0,
                1 => p1,
                2 => p2,
                3 => p3,
                _ => throw new System.IndexOutOfRangeException(),
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    p0 = value;
                    break;
                case 1:
                    p1 = value;
                    break;
                case 2:
                    p2 = value;
                    break;
                case 3:
                    p3 = value;
                    break;
                default:
                    throw new System.IndexOutOfRangeException();
            }
        }
    }

    public int this[PersonalityType type]
    {
        get
        {
            switch (type)
            {
                case PersonalityType.Inside:
                    return p0;
                case PersonalityType.Outside:
                    return -p0;
                case PersonalityType.Logic:
                    return p1;
                case PersonalityType.Passion:
                    return -p1;
                case PersonalityType.Moral:
                    return p2;
                case PersonalityType.Unethic:
                    return -p2;
                case PersonalityType.Detour:
                    return p3;
                case PersonalityType.Strong:
                    return -p3;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        set
        {
            switch (type)
            {
                case PersonalityType.Inside:
                    p0 = value;
                    break;
                case PersonalityType.Outside:
                    p0 = -value;
                    break;
                case PersonalityType.Logic:
                    p1 = value;
                    break;
                case PersonalityType.Passion:
                    p1 = -value;
                    break;
                case PersonalityType.Moral:
                    p2 = value;
                    break;
                case PersonalityType.Unethic:
                    p2 = -value;
                    break;
                case PersonalityType.Detour:
                    p3 = value;
                    break;
                case PersonalityType.Strong:
                    p3 = -value;
                    break;
                default:
                    break;
            }
        }
    }

    public Personality()
    {
        p0 = p1 = p2 = p3 = 0;
    }
    public Personality(int p0, int p1, int p2, int p3)
    {
        this.p0 = p0;
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    public Personality(List<int> l)
    {
        this.p0 = l[0];
        this.p1 = l[1];
        this.p2 = l[2];
        this.p3 = l[3];
    }

    [XmlElement(ElementName ="inner")]
    public int Inner
    {
        get => p0;
        set => p0 = value;
    }

    [XmlElement(ElementName = "outside")]
    public int Outside
    {
        get => -p0;
        set => p0 = -value;
    }
    [XmlElement(ElementName = "logic")]
    public int Logic
    {
        get => p1;
        set => p1 = value;
    }
    [XmlElement(ElementName = "spritial")]
    public int Spritial
    {
        get => -p1;
        set => p1 = -value;
    }
    [XmlElement(ElementName = "moral")]
    public int Moral
    {
        get => p2;
        set => p2 = value;
    }
    [XmlElement(ElementName = "immoral")]
    public int Immoral
    {
        get => -p2;
        set => p2 = -value;
    }

    [XmlElement(ElementName = "roundabout")]
    public int Roundabout
    {
        get => p3;
        set => p3 = value;
    }
    [XmlElement(ElementName = "aggressive")]
    public int Aggressive
    {
        get => -p3;
        set => p3 = -value;
    }

    public static Personality operator +(Personality a, Personality b)
    {
        Personality ret = new Personality
        {
            p0 = a.p0 + b.p0,
            p1 = a.p1 + b.p1,
            p2 = a.p2 + b.p2,
            p3 = a.p3 + b.p3
        };
        return ret;
    }

    public static int MaxDistance(Personality a, Personality b)
    {
        int ret = 0;
        ret = Mathf.Max(Mathf.Abs(a.p0 - b.p0), ret);
        ret = Mathf.Max(Mathf.Abs(a.p1 - b.p1), ret);
        ret = Mathf.Max(Mathf.Abs(a.p2 - b.p2), ret);
        ret = Mathf.Max(Mathf.Abs(a.p3 - b.p3), ret);
        return ret;
    }

    public static bool InBound(Personality a, Personality b)
    {
        for (int j = 0; j < 4; j++)
        {
            if (b[j] > 0)
            {
                if (a[j] < b[j])
                    return false;
            }
            else if (b[j] < 0)
            {
                if (a[j] > b[j])
                    return false;
            }
        }
        return true;
    }

    private static HashSet<PersonalityType> positiveSet = new HashSet<PersonalityType>() {
        PersonalityType.Outside,
        PersonalityType.Logic,
        PersonalityType.Moral,
        PersonalityType.Strong
    };

    public static HashSet<PersonalityType> PositiveSet { get => new HashSet<PersonalityType>(positiveSet); }
}

