using System;
using System.Collections.Generic;

public class Signal
{
    public bool flag;  //是否回合结束时清除 
    public int lastTurns; //持续次数
    public string effectKey; //对应效果的名称
    public int arg; //参数
    public List<int> args; //额外参数

    public Signal(bool flag, int lastTurns, string effectKey, int arg, List<int> args = null)
    {
        this.flag = flag;
        this.lastTurns = lastTurns;
        this.effectKey = effectKey;
        this.arg = arg;
        this.args = null;
    }
}
