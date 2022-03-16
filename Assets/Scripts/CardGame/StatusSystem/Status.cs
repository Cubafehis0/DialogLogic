using System;
using System.Collections.Generic;
using SemanticTree;
public class Status
{
    //可能替换为StatusInfo结构体
    private string name;
    private int decreaseOnTurnStart = 0; //回合开始自动减少
    private int decreaseOnTurnEnd = 0;  //回合结束自动减少
    private bool stackable = true;   //
    private bool allowNegative = false;


    public IEffectNode OnAdd = null;
    public IEffectNode OnRemove = null;
    public IEffectNode OnAfterPlayCard = null;
    public IEffectNode OnTurnStart = null;

    public string Name { get => name; }
    public int DecreaseOnTurnStart { get => decreaseOnTurnStart; }
    public int DecreaseOnTurnEnd { get => decreaseOnTurnEnd; }
    public bool Stackable { get => stackable; }
    public bool AllowNegative { get => allowNegative; }

    private static Status drawBan = null;
    public static Status DrawBan
    {
        get
        {
            if (drawBan == null)
            {
                drawBan = new Status()
                {
                    name = "drawBan",
                    decreaseOnTurnEnd = 1,
                    OnAdd = SemanticTreeClass.TestSetDrawBanNode(true),
                    OnRemove = SemanticTreeClass.TestSetDrawBanNode(false)
                };

            }
            return drawBan;
        }
    }

    private static Status freeCard = null;
    public static Status FreeCard
    {
        get
        {
            if (freeCard == null)
            {
                freeCard = new Status()
                {
                    name = "freeCard",
                    decreaseOnTurnEnd = 99,
                    OnAdd = SemanticTreeClass.FreeCardOnAdd,
                    OnRemove = SemanticTreeClass.FreeCardOnRemove,
                    OnAfterPlayCard = SemanticTreeClass.FreeCardOnAfterPlayCard,
                };
            }
            return freeCard;
        }
    }

    private static Status outPlusByPreachStatus = null;
    public static Status OutPlusByPreachStatus
    {
        get
        {
            if (outPlusByPreachStatus == null)
            {
                outPlusByPreachStatus = new Status()
                {
                    name = "outPlusByPreach",
                    OnAfterPlayCard = SemanticTreeClass.TestOutPlusByPreachNode,
                };
            }
            return outPlusByPreachStatus;
        }
    }

    //【持续】回合开始时，获得一张【说教】牌
    private static Status addPreachEveryRound = null;
    public static Status AddPreachEveryRound
    {
        get
        {
            if (addPreachEveryRound == null)
            {
                addPreachEveryRound = new Status()
                {
                    name = "addPreachEveryRound",
                    OnTurnStart = SemanticTreeClass.TestAddPreachEveryRoundOnTurnStart,
                };
            }
            return addPreachEveryRound;
        }
    }

    //【持续】回合开始时，每持有一点外感便随机揭示x个判定
    //未完成
    private static Status revealByOut = null;
    public static Status RevealByOut
    {
        get
        {
            if (revealByOut == null)
            {
                revealByOut = new Status()
                {
                    name = "revealByOut",
                    OnTurnStart = SemanticTreeClass.TestRevealByOutOnTurnStart,
                };
            }
            return revealByOut;
        }
    }

}

public class StatusCounter
{
    public Status status = null;
    public int value = 0;
}