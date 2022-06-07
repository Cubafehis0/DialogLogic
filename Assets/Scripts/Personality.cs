using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;



public enum PersonalityType
{
    [XmlEnum(Name = "inside")]
    Inside,
    [XmlEnum(Name = "outside")]
    Outside,
    [XmlEnum(Name = "logic")]
    Logic,
    [XmlEnum(Name = "passion")]
    Passion,
    [XmlEnum(Name = "moral")]
    Moral,
    [XmlEnum(Name = "unethic")]
    Unethic,
    [XmlEnum(Name = "detour")]
    Detour,
    [XmlEnum(Name = "strong")]
    Strong
}

[Serializable]
public class Personality
{
    [SerializeField]
    private int inner;
    [SerializeField]
    private int logic;
    [SerializeField]
    private int moral;
    [SerializeField]
    private int roundabout;

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => inner,
                1 => logic,
                2 => moral,
                3 => roundabout,
                _ => throw new System.IndexOutOfRangeException(),
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    inner = value;
                    break;
                case 1:
                    logic = value;
                    break;
                case 2:
                    moral = value;
                    break;
                case 3:
                    roundabout = value;
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
                    return inner;
                case PersonalityType.Outside:
                    return -inner;
                case PersonalityType.Logic:
                    return logic;
                case PersonalityType.Passion:
                    return -logic;
                case PersonalityType.Moral:
                    return moral;
                case PersonalityType.Unethic:
                    return -moral;
                case PersonalityType.Detour:
                    return roundabout;
                case PersonalityType.Strong:
                    return -roundabout;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        set
        {
            switch (type)
            {
                case PersonalityType.Inside:
                    inner = value;
                    break;
                case PersonalityType.Outside:
                    inner = -value;
                    break;
                case PersonalityType.Logic:
                    logic = value;
                    break;
                case PersonalityType.Passion:
                    logic = -value;
                    break;
                case PersonalityType.Moral:
                    moral = value;
                    break;
                case PersonalityType.Unethic:
                    moral = -value;
                    break;
                case PersonalityType.Detour:
                    roundabout = value;
                    break;
                case PersonalityType.Strong:
                    roundabout = -value;
                    break;
                default:
                    break;
            }
        }
    }

    public Personality()
    {
        inner = logic = moral = roundabout = 0;
    }
    public Personality(int p0, int p1, int p2, int p3)
    {
        this.inner = p0;
        this.logic = p1;
        this.moral = p2;
        this.roundabout = p3;
    }

    public Personality(Personality origin)
    {
        this.inner = origin.inner;
        this.logic = origin.logic;
        this.moral = origin.moral;
        this.roundabout = origin.roundabout;
    }
    public Personality(List<int> l)
    {
        this.inner = l[0];
        this.logic = l[1];
        this.moral = l[2];
        this.roundabout = l[3];
    }

    [XmlElement(ElementName = "inner")]
    public int Inner
    {
        get => inner;
        set => inner = value;
    }
    [XmlIgnore]
    public bool InnerSpecified
    {
        get { return Inner != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    [XmlElement(ElementName = "outside")]
    public int Outside
    {
        get => -inner;
        set => inner = -value;
    }
    [XmlIgnore]
    public bool OutsideSpecified
    {
        get { return Outside != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    [XmlElement(ElementName = "logic")]
    public int Logic
    {
        get => logic;
        set => logic = value;
    }
    [XmlIgnore]
    public bool LogicSpecified
    {
        get { return Logic != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    [XmlElement(ElementName = "spritial")]
    public int Spiritial
    {
        get => -logic;
        set => logic = -value;
    }
    [XmlIgnore]
    public bool SpritialSpecified
    {
        get { return Spiritial != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    [XmlElement(ElementName = "moral")]
    public int Moral
    {
        get => moral;
        set => moral = value;
    }
    [XmlIgnore]
    public bool MoralSpecified
    {
        get { return Moral != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    [XmlElement(ElementName = "immoral")]
    public int Immoral
    {
        get => -moral;
        set => moral = -value;
    }
    [XmlIgnore]
    public bool ImmoralSpecified
    {
        get { return Immoral != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }


    [XmlElement(ElementName = "roundabout")]
    public int Roundabout
    {
        get => roundabout;
        set => roundabout = value;
    }
    [XmlIgnore]
    public bool RoundaboutSpecified
    {
        get { return Roundabout != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    [XmlElement(ElementName = "aggressive")]
    public int Aggressive
    {
        get => -roundabout;
        set => roundabout = -value;
    }
    [XmlIgnore]
    public bool AggressiveSpecified
    {
        get { return Aggressive != 0; }
        [Obsolete("仅供xml识别，无需设置")]
        set { }
    }

    public void Strengthen(int value)
    {
        if (value <= 0) return;
        Inner += Inner > 0 ? value : 0;
        Outside += Outside > 0 ? value : 0;
        Logic += Logic > 0 ? value : 0;
        Spiritial += Spiritial > 0 ? value : 0;
        Moral += Moral > 0 ? value : 0;
        Immoral += Immoral > 0 ? value : 0;
        Roundabout += Roundabout > 0 ? value : 0;
        Aggressive += Aggressive > 0 ? value : 0;
    }

    public static Personality operator +(Personality a, Personality b)
    {
        Personality ret = new Personality
        {
            inner = a.inner + b.inner,
            logic = a.logic + b.logic,
            moral = a.moral + b.moral,
            roundabout = a.roundabout + b.roundabout
        };
        return ret;
    }

    public static int MaxDistance(Personality a, Personality b)
    {
        int ret = 0;
        ret = Mathf.Max(Mathf.Abs(a.inner - b.inner), ret);
        ret = Mathf.Max(Mathf.Abs(a.logic - b.logic), ret);
        ret = Mathf.Max(Mathf.Abs(a.moral - b.moral), ret);
        ret = Mathf.Max(Mathf.Abs(a.roundabout - b.roundabout), ret);
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

