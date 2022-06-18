using CardGame.Recorder;

using System;
using UnityEngine;
using ModdingAPI;

public class Card
{
    public string id;
    public CardPlayerState player;
    public CardInfo info;

    [SerializeField]
    private bool temporaryActivate = false;
    [SerializeField]
    private bool permanentActivate = false;

    public int GetFinalCost()
    {
        if (Activated) return 0;
        int ret = info.BaseCost;
        foreach (var modifer in player.Modifiers)//有缺陷
        {
            CostModifier m = modifer.CostModifier;
            if (m != null && (m.Condition?.Invoke() ?? true))
            {
                ret = m.num.Invoke();
            }
        }
        return ret;
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
    public int GetProp(string name)
    {
        throw new NotImplementedException();
    }
}