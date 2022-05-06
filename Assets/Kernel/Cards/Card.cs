using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ExpressionAnalyser;
using CardGame.Recorder;

public class Card
{

    public CardPlayerState player;
    //卡牌基本信息
    public CardInfo info;

    [SerializeField]
    private bool temporaryActivate = false;
    [SerializeField]
    private bool permanentActivate = false;

    public int FinalCost
    {
        get
        {
            if (Activated) return 0;
            int ret = info.BaseCost;
            foreach (var modifer in player.Modifiers)//有缺陷
            {
                CostModifier m = modifer.CostModifier;
                if (m != null && (m.Condition?.Value ?? true))
                {
                    ret = m.exp.Value;
                }
            }
            return ret;
        }
    }
    public bool Activated { get => TemporaryActivate || PermanentActivate; }
    public bool TemporaryActivate
    {
        get => temporaryActivate;
        set
        {
            temporaryActivate = value;
            if (value == true) CardGameManager.Instance.CardRecorder.AddRecordEntry(new CardLogEntry { LogType = ActionTypeEnum.ActivateCard });
        }
    }

    public bool PermanentActivate
    {
        get => permanentActivate;
        set
        {
            permanentActivate = value;
            if (value == true) CardGameManager.Instance.CardRecorder.AddRecordEntry(new CardLogEntry { LogType = ActionTypeEnum.ActivateCard });
        }
    }

    public static int CardCount;

    public Card()
    {
        CardCount++;

    }

    public Card(CardInfo info) : this()
    {
        //Debug.Log(string.Format("生成Card类,Title={0},当前Card数量:{1}", info.Title, CardCount));
        this.info = new CardInfo(info);
        temporaryActivate = false;
        permanentActivate = false;
    }

    public Card(Card origin) : this()
    {
        //Debug.Log(string.Format("生成Card拷贝,Title={0},当前Card数量:{1}",origin.info.Title, CardCount));
        this.info = new CardInfo(origin.info);
        temporaryActivate = origin.temporaryActivate;
        permanentActivate = origin.permanentActivate;
    }

    public void Construct()
    {
        info.Construct();
    }

    public int GetProp(string name)
    {
        throw new NotImplementedException();
    }
}