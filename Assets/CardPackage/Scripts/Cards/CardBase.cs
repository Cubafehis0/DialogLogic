using ModdingAPI;
using UnityEngine;
using System.Collections.Generic;


public class CardBase
{
    public string id;
    public int cost;
    public bool exhaust;
    public CardBase() { }
    public CardBase(object o) { }

    public virtual bool CheckCanPlay(out string failmsg)
    {
        failmsg = null;
        return true;
    }
    public virtual void PreCalculateCost() { }

    public virtual void Excute(GameObject target) 
    {
        Debug.Log($"对{target.name}使用卡牌： ");
    }

    public virtual void OnDraw()
    {

    }

    public virtual void Construct<T>(T arg) 
    {
        
    }
    
}