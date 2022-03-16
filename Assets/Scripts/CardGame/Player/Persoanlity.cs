using System;
using System.Collections.Generic;
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
public struct Personality
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
            return type switch
            {
                PersonalityType.Inside => Inside,
                PersonalityType.Outside => Outside,
                PersonalityType.Logic => Logic,
                PersonalityType.Passion => Passion,
                PersonalityType.Moral => Moral,
                PersonalityType.Unethic => Unethic,
                PersonalityType.Detour => Detour,
                PersonalityType.Strong => Strong,
                _ => throw new System.IndexOutOfRangeException(),
            };
        }
        set
        {
            switch (type)
            {
                case PersonalityType.Inside:
                    Inside = value;
                    break;
                case PersonalityType.Outside:
                    Outside = value;
                    break;
                case PersonalityType.Logic:
                    Logic = value;
                    break;
                case PersonalityType.Passion:
                    Passion = value;
                    break;
                case PersonalityType.Moral:
                    Moral = value;
                    break;
                case PersonalityType.Unethic:
                    Unethic = value;
                    break;
                case PersonalityType.Detour:
                    Detour = value;
                    break;
                case PersonalityType.Strong:
                    Strong = value;
                    break;
                default:
                    break;
            }
        }
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

    public int Inside
    {
        get => p0;
        set => p0 = value;
    }
    public int Outside
    {
        get => -p0;
        set => p0 = -value;
    }
    public int Logic
    {
        get => p1;
        set => p1 = value;
    }
    public int Passion
    {
        get => -p1;
        set => p1 = -value;
    }

    public int Moral
    {
        get => p2;
        set => p2 = value;
    }
    public int Unethic
    {
        get => -p2;
        set => p2 = -value;
    }

    public int Detour
    {
        get => p3;
        set => p3 = value;
    }
    public int Strong
    {
        get => -p3;
        set => p3 = -value;
    }

    public static Personality operator +(Personality a, Personality b)
    {
        Personality ret=new Personality(0,0,0,0);
        ret.p0 = a.p0 + b.p0;
        ret.p1 = a.p1 + b.p1;
        ret.p2 = a.p2 + b.p2;
        ret.p3 = a.p3 + b.p3;
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

