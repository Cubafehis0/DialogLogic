using ModdingAPI;
using UnityEngine;
using System.Collections.Generic;
using System;
[Serializable]
public class CardBase
{
    public string id;


    public virtual bool CheckCanPlay(GameObject player, out string failmsg)
    {
        failmsg = null;
        return true;
    }

    public virtual void Excute(GameObject target) 
    {

        Debug.Log($"对{target.name}使用卡牌：{id}");
    }

    public virtual void OnDraw()
    {

    }

    public virtual void Construct<T>(T arg) 
    {
        
    }
    
}