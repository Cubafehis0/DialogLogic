using System;
using System.Collections.Generic;

/// <summary>
/// signal类用于标记玩家的“状态”
/// </summary>
public class Signal
{
    public bool decreaseOnTurnStart = false;
    public bool decreaseOnTurnEnd = false;  //是否回合结束时清除 
    public bool decreaseOnUse = false;
    public bool stackable = false;
    public int value = 0; //持续回合数
    public string effectKey = null; //对应效果的名称
    public int arg = 0;
}
