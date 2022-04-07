using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;
using System;
using System.Xml.Serialization;
using CardGame.Recorder;
using System.Linq;

public class CardPlayerState : MonoBehaviour, IPlayerStateChange
{
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private ChooseSystem chooseSystem = null;
    [SerializeField]
    public CardManager cardManager = null;

    [SerializeField]
    private bool drawBan = false;


    private UnityEvent onPersonalityChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnEnergyChange = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayCard = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStartTurn = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEndTurn = new UnityEvent();
    //��ͬ�ж������ĸ���
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };

    public ModifierGroup Modifiers = new ModifierGroup();

    public ChooseSystem ChooseSystem { get => chooseSystem; }

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

    public StatusManager StatusManager => GetComponent<StatusManager>();
    public int DrawNum { get => player.PlayerInfo.DrawNum; set => player.PlayerInfo.DrawNum = value; }

    public UnityEvent OnValueChange => onPersonalityChange;
    public bool DrawBan { get => drawBan; set => drawBan = value; }
    public Player Player { get => player; }

    private void Awake()
    {
        cardManager.Hand.OnAdd.AddListener(x => Modifiers.Add(x.info.handModifier));
        cardManager.Hand.OnRemove.AddListener(x => Modifiers.Remove(x.info.handModifier));
    }

    public void AddModifier(Modifier script)
    {
        if (script.OnPlayCard != null) OnPlayCard.AddListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnStartTurn.AddListener(script.OnTurnStart.Execute);
        Modifiers.Add(script);
    }

    public void RemoveModifier(Modifier script)
    {
        if (script.OnPlayCard != null) OnPlayCard.RemoveListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnStartTurn.RemoveListener(script.OnTurnStart.Execute);
        Modifiers.Remove(script);
    }













    public void RandomReveal(int num)
    {
        throw new System.NotImplementedException();
    }

    public void RandomReveal(SpeechType type, int num)
    {
        throw new System.NotImplementedException();
    }

    public bool CanChoose(ChoiceSlot slot)
    {
        //�ж��Ƿ��ѡ
        return !(slot.Locked || (FocusSpeechType != null && FocusSpeechType == slot.Choice.SpeechType));
    }

    /// <summary>
    /// ���ж��Ƿ��ѡ��ǿ��ѡ��ѡ��
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
        Debug.Log("�ҵĻغϣ��鿨������");
        OnStartTurn.Invoke();
        Energy = 4;
        cardManager.Draw((uint)player.PlayerInfo.DrawNum);
    }

    public void EndTurn()
    {
        Debug.Log("�غϽ���");
        OnEndTurn.Invoke();
        onPersonalityChange.Invoke();
        foreach (Card card in cardManager.Hand)
        {
            card.TemporaryActivate = false;
        }
        foreach (Card card in cardManager.DiscardPile)
        {
            card.TemporaryActivate = false;
        }
        foreach (Card card in cardManager.DrawPile)
        {
            card.TemporaryActivate = false;
        }
    }

    public void Init()
    {
        Debug.Log("��ҳ�ʼ��");
        foreach (string name in Player.PlayerInfo.CardSet)
        {
            Card newCard = CardGameManager.Instance.GetCardCopy(StaticCardLibrary.Instance.GetByName(name));
            newCard.player = this;
            cardManager.DrawPile.Add(newCard);
        }
        cardManager.DrawPile.Shuffle();
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
            "hand_count" => cardManager.Hand.Count,
            "draw_count" => cardManager.DrawPile.Count,
            "discard_count" => cardManager.DiscardPile.Count,
            "normal_count" => chooseSystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Normal).Count(),
            "threat_count" => chooseSystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Threaten).Count(),
            "persuade_count" => chooseSystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Persuade).Count(),
            "cheat_count" => chooseSystem.Choices.Select(x => x.Choice.SpeechType == SpeechType.Cheat).Count(),
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
