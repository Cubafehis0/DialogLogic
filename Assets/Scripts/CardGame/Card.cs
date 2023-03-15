using CardGame.Recorder;

using System;
using UnityEngine;
using ModdingAPI;

[Serializable]
public class Card : CardBase
{
    public string Name;
    public string Title;
    public string ConditionDesc;
    public string EffectDesc;
    public string Meme;
    public int BaseCost;
    public bool Exhaust;
    public CardType category;
    public Modifier handModifier;
    public Func<bool> Requirements;
    public Action DrawEffects;
    public Action Effects;

    [SerializeField]
    private bool temporaryActivate = false;
    [SerializeField]
    private bool permanentActivate = false;


    public override bool CheckCanPlay(GameObject player, out string failmsg)
    {
        var cardController = player.GetComponent<CardController>();
        if (cardController.Energy < cardController.GetFinalCost(this))
        {
            failmsg = "能量不足";
            return false;
        }

        if (!Activated && Requirements != null)
        {
            failmsg = "未达到要求";
            return Requirements.Invoke();
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
        if (Effects == null) Debug.Log("空效果");
        else
        {
            Effects.Invoke();
            CardLogEntry log = new CardLogEntry
            {
                Name = Name,
                IsActive = Activated,
                LogType = ActionTypeEnum.PlayCard,
                Turn = CardGameManager.Instance.TurnManager.Turn,
                CardCategory = category,
            };
            CardGameManager.Instance.CardRecorder.AddRecordEntry(log);
        }
    }

    public override void OnDraw()
    {
        base.OnDraw();
        if (DrawEffects != null)
        {
            DrawEffects.Invoke();
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
    public int GetProp(string name)
    {
        throw new NotImplementedException();
    }

    public override void Construct<T>(T arg)
    {
        if (arg is CardInfo info)
        {
            Name = info.Name;
            Title = info.Title;
            ConditionDesc = info.ConditionDesc;
            EffectDesc = info.EffectDesc;
            Meme = info.Meme;
            BaseCost = info.BaseCost;
            Exhaust = info.Exhaust;
            category = info.category;
            handModifier = info.handModifier;
            Requirements = info.Requirements;
            DrawEffects = info.DrawEffects;
            Effects = info.Effects;
            temporaryActivate = false;
            permanentActivate = false;
        }
        else if (arg is Card origin)
        {
            Name = origin.Name;
            Title = origin.Title;
            ConditionDesc = origin.ConditionDesc;
            EffectDesc = origin.EffectDesc;
            Meme = origin.Meme;
            BaseCost = origin.BaseCost;
            Exhaust = origin.Exhaust;
            category = origin.category;
            handModifier = origin.handModifier;
            Requirements = origin.Requirements;
            DrawEffects = origin.DrawEffects;
            Effects = origin.Effects;
            temporaryActivate = origin.temporaryActivate;
            permanentActivate = origin.permanentActivate;
            temporaryActivate = origin.temporaryActivate;
            permanentActivate = origin.permanentActivate;
        }
        else
        {
            throw new ArgumentException();
        }
    }
}