using Ink2Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using CardGame.Recorder;
using System.Linq;
using SemanticTree;

public class CardPlayerState : CardActorState, IPlayerStateChange, ICardController
{
    [SerializeField]
    private ChooseGUISystem chooseGUISystem = null;
    [SerializeField]
    private CardController cardController = null;

    private UnityEvent onPersonalityChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnEnergyChange = new UnityEvent();
    //��ͬ�ж������ĸ���
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };

    public ChooseGUISystem ChooseGUISystem { get => chooseGUISystem; }

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

    public int Strength;

    public int Pressure { get => Player.PlayerInfo.Pressure; set => Player.PlayerInfo.Pressure = value; }

    public StatusManager StatusManager => statusManager;
    public int DrawNum { get => Player.PlayerInfo.DrawNum; set => Player.PlayerInfo.DrawNum = value; }

    public UnityEvent OnValueChange => onPersonalityChange;

    public IReadonlyPile<Card> DiscardPile => ((ICardController)cardController).DiscardPile;

    public IReadonlyPile<Card> DrawPile => ((ICardController)cardController).DrawPile;

    public IReadonlyPile<Card> Hand => ((ICardController)cardController).Hand;

    public bool IsHandFull()
    {
        return ((ICardController)cardController).IsHandFull();
    }

    public bool DrawBan { get => ((ICardController)cardController).DrawBan; set => ((ICardController)cardController).DrawBan = value; }

    public void Init(PlayerPacked player)
    {
        Debug.Log("��ҳ�ʼ��");
        this.player = player;
        cardController.AddCardSet2DrawPile(Player.PlayerInfo.CardSet);
    }

    private void Awake()
    {
        if(cardController) cardController.OnPlayCard.AddListener(modifiers.OnPlayCard);
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
        int dis = Personality.MaxDistance(GetFinalPersonality(), slot.Choice.JudgeValue);
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
        Energy = player.PlayerInfo.BaseEnergy;
        cardController.Draw((uint)Player.PlayerInfo.DrawNum);
        Context.PushPlayerContext(this);
        modifiers.OnTurnStart();
    }

    public void EndTurn()
    {
        Debug.Log("�غϽ���");
        cardController.ClearTemporaryActivateFlags();
        Context.PopPlayerContext();
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
        if (cardController)
        {
            int? pileVar = cardController.GetPileProp(name);
            if (pileVar.HasValue) return pileVar.Value;
        }
        return name switch
        {
            "inner" => GetFinalPersonality()[PersonalityType.Inside],
            "outside" => GetFinalPersonality()[PersonalityType.Outside],
            "logic" => GetFinalPersonality()[PersonalityType.Logic],
            "spirital" => GetFinalPersonality()[PersonalityType.Passion],
            "moral" => GetFinalPersonality()[PersonalityType.Moral],
            "immoral" => GetFinalPersonality()[PersonalityType.Unethic],
            "roundabout" => GetFinalPersonality()[PersonalityType.Detour],
            "aggressive" => GetFinalPersonality()[PersonalityType.Strong],
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
        return CardGameManager.Instance.CardRecorder[name];
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
