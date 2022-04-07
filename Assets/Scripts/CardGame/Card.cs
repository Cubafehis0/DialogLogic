using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ExpressionAnalyser;
using CardGame.Recorder;

public class Card : MonoBehaviour
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
            if (value == true) CardRecorder.Instance.AddRecordEntry(new CardLogEntry { LogType = ActionTypeEnum.ActivateCard });
        }
    }

    public bool PermanentActivate 
    { 
        get => permanentActivate;
        set
        {
            permanentActivate = value;
            if (value == true) CardRecorder.Instance.AddRecordEntry(new CardLogEntry { LogType = ActionTypeEnum.ActivateCard });
        }
    }

    public void Construct(Card prefab)
    {
        //浅拷贝
        info = prefab.info;
    }

    public void Construct(CardInfo info)
    {
        this.info = info;
    }
}