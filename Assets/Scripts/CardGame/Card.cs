using CardGame.Recorder;

using System;
using UnityEngine;
using ModdingAPI;

public class Card : CardBase
{
    public CardPlayerState player;
    public CardInfo info;
    [SerializeField]
    private bool temporaryActivate = false;
    [SerializeField]
    private bool permanentActivate = false;

    public override void PreCalculateCost()
    {
        cost = info.BaseCost;
        if (Activated)
        {
            cost = 0;
            return;
        }
        foreach (var modifer in player.Modifiers)//有缺陷
        {
            CostModifier m = modifer.CostModifier;
            if (m != null && (m.Condition?.Invoke() ?? true))
            {
                cost = m.num.Invoke();
            }
        }
    }

    public override bool CheckCanPlay(out string failmsg)
    {
        if (!Activated && info.Requirements != null)
        {
            failmsg = "未达到要求";
            return info.Requirements.Invoke();
        }
        else
        {
            failmsg = null;
            return true;
        }
    }

    public override void Excute(GameObject target)
    {
        base.Excute(target);
        if (info.Effects == null) Debug.Log("空效果");
        else
        {
            info.Effects.Invoke();
            CardLogEntry log = new CardLogEntry
            {
                Name = info.Name,
                IsActive = Activated,
                LogType = ActionTypeEnum.PlayCard,
                Turn = CardGameManager.Instance.TurnManager.Turn,
                CardCategory = info.category,
            };
            CardGameManager.Instance.CardRecorder.AddRecordEntry(log);
        }
    }

    public override void OnDraw()
    {
        base.OnDraw();
        if (info.DrawEffects != null)
        {
            info.DrawEffects.Invoke();
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
    public int GetProp(string name)
    {
        throw new NotImplementedException();
    }

    public override void Construct<T>(T arg)
    {
        if(arg is CardInfo info)
        {
            this.info = new CardInfo(info);
            temporaryActivate = false;
            permanentActivate = false;
        }
        else  if(arg is Card origin)
        {
            this.info = new CardInfo(origin.info);
            temporaryActivate = origin.temporaryActivate;
            permanentActivate = origin.permanentActivate;
        }
        else
        {
            throw new ArgumentException();
        }
    }
}