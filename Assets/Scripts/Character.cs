using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class Character
{
    public int[] data = new int[4];

    public int Moral
    {
        get
        {
            return data[2];
        }
        set
        {
            data[2] = value;
        }
    }
    public int Unethic
    {
        get
        {
            return -data[2];
        }
        set
        {
            data[2] = -value;
        }
    }

    public int Inside
    {
        get
        {
            return data[0];
        }
        set
        {
            data[0] = value;
        }
    }
    public int Outside
    {
        get
        {
            return -data[0];
        }
        set
        {
            data[0] = -value;
        }
    }
    
    public int Ration
    {
        get
        {
            return data[1];
        }
        set
        {
            data[1] = value;
        }
    }
    public int Passion
    {
        get
        {
            return -data[1];
        }
        set
        {
            data[1] = -value;
        }
    }

    public int Detour
    {
        get
        {
            return data[3];
        }
        set
        {
            data[3] = value;
        }    
    }
    public int Strong
    {
        get
        {
            return -data[3];
        }
        set
        {
            data[3] = -value;
        }
    }
}
[Serializable]
public class Player : Character
{
    public int energy = 0;
    public int[] originData = new int[] { 0,0,0,0}; 
    public List<uint> cardSet = new List<uint>();
    public List<uint> handCard = new List<uint>();
}

