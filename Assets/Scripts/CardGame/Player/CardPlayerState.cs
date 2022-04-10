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
    private ChooseGUISystem chooseGUISystem = null;
    [SerializeField]
    private CardController cardController = null;
    [SerializeField]
    private StatusManager statusManager = null;

    private UnityEvent onPersonalityChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnEnergyChange = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStartTurn = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEndTurn = new UnityEvent();
    //��ͬ�ж������ĸ���
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };

    public ModifierGroup Modifiers = new ModifierGroup();

    public Player Player { get; private set; }

    public ChooseGUISystem ChooseGUISystem { get => chooseGUISystem; }

    public Personality FinalPersonality
    {
        get
        {
            var res = Player.PlayerInfo.Personality + Modifiers.PersonalityLinear;
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
        get => Player.PlayerInfo.Energy;
        set
        {
            Player.PlayerInfo.Energy = value;
            OnEnergyChange.Invoke();
        }
    }

    public int Pressure { get => Player.PlayerInfo.Pressure; set => Player.PlayerInfo.Pressure = value; }

    public StatusManager StatusManager => statusManager;
    public int DrawNum { get => Player.PlayerInfo.DrawNum; set => Player.PlayerInfo.DrawNum = value; }

    public UnityEvent OnValueChange => onPersonalityChange;

    public IReadonlyPile<Card> DiscardPile => ((ICardController)cardController).DiscardPile;

    public IReadonlyPile<Card> DrawPile => ((ICardController)cardController).DrawPile;

    public IReadonlyPile<Card> Hand => ((ICardController)cardController).Hand;

    public bool IsHandFull => ((ICardController)cardController).IsHandFull;

    public bool DrawBan { get => ((ICardController)cardController).DrawBan; set => ((ICardController)cardController).DrawBan = value; }

    public void Init(Player player)
    {
        Debug.Log("��ҳ�ʼ��");
        Player = player;
        cardController.AddCardSet2DrawPile(Player.PlayerInfo.CardSet);
    }

    public void AddModifier(Modifier script)
    {
        if (script.OnPlayCard != null && cardController) cardController.OnPlayCard.AddListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnStartTurn.AddListener(script.OnTurnStart.Execute);
        Modifiers.Add(script);
    }

    public void RemoveModifier(Modifier script)
    {
        if (script.OnPlayCard != null && cardController) cardController.OnPlayCard.RemoveListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnStartTurn.RemoveListener(script.OnTurnStart.Execute);
        Modifiers.Remove(script);
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
        cardController.Draw((uint)Player.PlayerInfo.DrawNum);
    }

    public void EndTurn()
    {
        Debug.Log("�غϽ���");
        OnEndTurn.Invoke();
        cardController.ClearTemporaryActivateFlags();
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
        int? pileVar = cardController.GetPileProp(name);
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

    #region ICardManager�ӿ�
    public void AddCard(PileType pileType, Card card)
    {
        ((ICardController)cardController).AddCard(pileType, card);
    }

    public void AddCardSet2DrawPile(List<string> cardset)
    {
        ((ICardController)cardController).AddCardSet2DrawPile(cardset);
    }

    public bool CanDraw()
    {
        return ((ICardController)cardController).CanDraw();
    }

    public void ClearTemporaryActivateFlags()
    {
        ((ICardController)cardController).ClearTemporaryActivateFlags();
    }

    public void Discard2Draw()
    {
        ((ICardController)cardController).Discard2Draw();
    }

    public void DiscardAll()
    {
        ((ICardController)cardController).DiscardAll();
    }

    public void DiscardCard(Card cid)
    {
        ((ICardController)cardController).DiscardCard(cid);
    }

    public void Draw(uint num)
    {
        ((ICardController)cardController).Draw(num);
    }

    public void Draw2Full()
    {
        ((ICardController)cardController).Draw2Full();
    }

    public int? GetPileProp(string name)
    {
        return ((ICardController)cardController).GetPileProp(name);
    }

    public void PlayCard(Card card)
    {
        ((ICardController)cardController).PlayCard(card);
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
