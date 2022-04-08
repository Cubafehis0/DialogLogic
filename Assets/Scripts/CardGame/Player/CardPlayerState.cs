using Ink2Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using CardGame.Recorder;
using System.Linq;

public class CardPlayerState : MonoBehaviour, IPlayerStateChange,ICardController
{
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private ChooseGUISystem chooseGUISystem = null;
    [SerializeField]
    private CardController cardManager = null;
    [SerializeField]
    private StatusManager statusManager = null;

    private UnityEvent onPersonalityChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnEnergyChange = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStartTurn = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEndTurn = new UnityEvent();
    //不同判定补正的概率
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };

    public ModifierGroup Modifiers = new ModifierGroup();

    public ChooseGUISystem ChooseGUISystem { get => chooseGUISystem; }

    public Personality FinalPersonality
    {
        get
        {
            var res = player.PlayerInfo.Personality + Modifiers.PersonalityLinear;
            return res;
        }
    }
    public SpeechArt FinalSpeechArt
    {
        get => Modifiers.SpeechLinear;
    }
    public SpeechType? FocusSpeechType
    {
        get => Modifiers.Focus;
    }
    public int Energy
    {
        get => player.PlayerInfo.Energy;
        set
        {
            player.PlayerInfo.Energy = value;
            OnEnergyChange.Invoke();
        }
    }

    public int Pressure { get => player.PlayerInfo.Pressure; set => player.PlayerInfo.Pressure = value; }

    public StatusManager StatusManager => statusManager;
    public int DrawNum { get => player.PlayerInfo.DrawNum; set => player.PlayerInfo.DrawNum = value; }

    public UnityEvent OnValueChange => onPersonalityChange;
    public Player Player { get => player; }

    public IReadonlyPile<Card> DiscardPile => ((ICardController)cardManager).DiscardPile;

    public IReadonlyPile<Card> DrawPile => ((ICardController)cardManager).DrawPile;

    public IReadonlyPile<Card> Hand => ((ICardController)cardManager).Hand;

    public bool IsHandFull => ((ICardController)cardManager).IsHandFull;

    public bool DrawBan { get => ((ICardController)cardManager).DrawBan; set => ((ICardController)cardManager).DrawBan = value; }

    public void AddModifier(Modifier script)
    {
        if (script.OnPlayCard != null && cardManager) cardManager.OnPlayCard.AddListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnStartTurn.AddListener(script.OnTurnStart.Execute);
        Modifiers.Add(script);
    }

    public void RemoveModifier(Modifier script)
    {
        if (script.OnPlayCard != null && cardManager) cardManager.OnPlayCard.RemoveListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnStartTurn.RemoveListener(script.OnTurnStart.Execute);
        Modifiers.Remove(script);
    }



    public bool CanChoose(ChoiceSlot slot)
    {
        //判断是否可选
        return !(slot.Locked || (FocusSpeechType != null && FocusSpeechType == slot.Choice.SpeechType));
    }

    /// <summary>
    /// 不判断是否可选，强制选择选项
    /// </summary>
    /// <param name="slot"></param>
    public bool JudgeChooseSuccess(ChoiceSlot slot)
    {
        int dis = Personality.MaxDistance(FinalPersonality, slot.Choice.JudgeValue);
        SpeechArt speech = FinalSpeechArt;
        int modifier = slot.Choice.SpeechType switch
        {
            SpeechType.Normal => speech[SpeechType.Normal],
            SpeechType.Cheat => speech[SpeechType.Cheat],
            SpeechType.Threaten => speech[SpeechType.Threaten],
            SpeechType.Persuade => speech[SpeechType.Persuade],
            _ => 0,
        };
        int randomEPS = MyMath.GetRandomJudge(jp);
        return dis <= randomEPS + modifier;
    }



    public void StartTurn()
    {
        Debug.Log("我的回合，抽卡！！！");
        OnStartTurn.Invoke();
        Energy = 4;
        cardManager.Draw((uint)player.PlayerInfo.DrawNum);
    }

    public void EndTurn()
    {
        Debug.Log("回合结束");
        OnEndTurn.Invoke();
        onPersonalityChange.Invoke();
        cardManager.ClearTemporaryActivateFlags();
    }

    public void Init()
    {
        Debug.Log("玩家初始化");
        cardManager.AddCardSet2DrawPile(Player.PlayerInfo.CardSet);
    }

    public void StateChange(Personality delta, int turn)
    {
        if (delta == null)
        {
            Debug.LogWarning("StateChange null");
            return;
        }
        StatusManager.AddAnonymousPersonalityModifier(delta, turn);
    }

    public int GetPlayerProp(string name)
    {
        try
        {
            return GetBaseProp(name);
        }
        catch (PropNotFoundException) { }
        try
        {
            return GetRecorderProp(name);
        }
        catch (PropNotFoundException) { }
        return GetStatusProp(name);
    }

    private int GetBaseProp(string name)
    {
        int? pileVar = cardManager.GetPileProp(name);
        if (pileVar.HasValue) return pileVar.Value;
        return name switch
        {
            "inner" => FinalPersonality[PersonalityType.Inside],
            "outside" => FinalPersonality[PersonalityType.Outside],
            "logic" => FinalPersonality[PersonalityType.Logic],
            "spritial" => FinalPersonality[PersonalityType.Passion],
            "moral" => FinalPersonality[PersonalityType.Moral],
            "immoral" => FinalPersonality[PersonalityType.Unethic],
            "roundabout" => FinalPersonality[PersonalityType.Detour],
            "aggressive" => FinalPersonality[PersonalityType.Strong],
            "normal_count" => chooseGUISystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Normal).Count(),
            "threat_count" => chooseGUISystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Threaten).Count(),
            "persuade_count" => chooseGUISystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Persuade).Count(),
            "cheat_count" => chooseGUISystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Cheat).Count(),
            "focus_count" => FocusSpeechType.HasValue ? 1 : 0,
            _ => throw new PropNotFoundException(),
        };
    }

    private int GetRecorderProp(string name)
    {
        return CardRecorder.Instance[name];
    }

    private int GetStatusProp(string name)
    {
        return StatusManager.GetStatusValue(name);
    }

    #region ICardManager接口
    public void AddCard(PileType pileType, Card card)
    {
        ((ICardController)cardManager).AddCard(pileType, card);
    }

    public void AddCardSet2DrawPile(List<string> cardset)
    {
        ((ICardController)cardManager).AddCardSet2DrawPile(cardset);
    }

    public bool CanDraw()
    {
        return ((ICardController)cardManager).CanDraw();
    }

    public void ClearTemporaryActivateFlags()
    {
        ((ICardController)cardManager).ClearTemporaryActivateFlags();
    }

    public void Discard2Draw()
    {
        ((ICardController)cardManager).Discard2Draw();
    }

    public void DiscardAll()
    {
        ((ICardController)cardManager).DiscardAll();
    }

    public void DiscardCard(Card cid)
    {
        ((ICardController)cardManager).DiscardCard(cid);
    }

    public void Draw(uint num)
    {
        ((ICardController)cardManager).Draw(num);
    }

    public void Draw2Full()
    {
        ((ICardController)cardManager).Draw2Full();
    }

    public int? GetPileProp(string name)
    {
        return ((ICardController)cardManager).GetPileProp(name);
    }

    public void PlayCard(Card card)
    {
        ((ICardController)cardManager).PlayCard(card);
    }
    #endregion

    [Serializable]
    public class PropNotFoundException : Exception
    {
        public PropNotFoundException() { }
        public PropNotFoundException(string message) : base(message) { }
        public PropNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected PropNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
