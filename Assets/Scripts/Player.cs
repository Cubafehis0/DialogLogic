using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int[] data;
    public int Detour;
    public int Inside;
    public int Logic;
    public int Moral;
    public int Outside;
    public int Passion;
    public int Strong;
    public int Unethic;

    private int www = 10;
    public Player()
    {
        data = new int[] { www, www, www, www };
        Detour = www;
        Inside = www;
        Logic = www;
        Moral = www;
        Outside = www;
        Passion = www;
        Strong = www;
        Unethic = www;

    }
}
