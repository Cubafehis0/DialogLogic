using CardGame.Recorder;
using ModdingAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : CardControllerBase, ITurnEnd, ITurnStart
{
    [SerializeField]
    private int baseEnergy;
    [SerializeField]
    private int baseDraw;

    public void OnTurnStart()
    {
        Debug.Log("我的回合，抽卡！！！");
        Energy = baseEnergy;
        Draw(baseDraw);
    }

    public void OnTurnEnd()
    {
        //ClearTemporaryActivateFlags();
    }
}
