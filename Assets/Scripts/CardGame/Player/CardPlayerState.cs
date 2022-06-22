using Ink2Unity;
using ModdingAPI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardPlayerState : MonoBehaviour, IPlayerStateChange
{
    [SerializeField]
    protected PlayerPacked player;
    [SerializeField]
    protected StatusManagerPacked statusManager = null;
    [SerializeField]
    protected ModifierGroup modifiers = new ModifierGroup();
    [SerializeField]
    private ChooseGUISystem chooseGUISystem = null;
    [SerializeField]
    private CardController cardController = null;

    private UnityEvent onPersonalityChange = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnEnergyChange = new UnityEvent();
    //不同判定补正的概率
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };


    public ModifierGroup Modifiers { get => modifiers; }
    public PlayerPacked Player { get => player; private set => player = value; }
    public ChooseGUISystem ChooseGUISystem { get => chooseGUISystem; }

    public Personality GetFinalPersonality()
    {
        var res = Player.PlayerInfo.Personality + Modifiers.PersonalityLinear + statusManager.Modifiers.PersonalityLinear;
        return res;
    }

    public SpeechArt FinalSpeechArt
    {
        get => Modifiers.SpeechLinear + statusManager.Modifiers.SpeechLinear;
    }
    public SpeechType? FocusSpeechType
    {
        get => Modifiers.Focus ?? statusManager.Modifiers.Focus;
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

    public StatusManagerPacked StatusManager => statusManager;
    public int DrawNum { get => Player.PlayerInfo.DrawNum; set => Player.PlayerInfo.DrawNum = value; }

    public UnityEvent OnValueChange => onPersonalityChange;

    public CardController CardController { get => cardController; set => cardController = value; }

    public void Init(PlayerPacked player)
    {
        Debug.Log("玩家初始化");
        this.player = player;
        cardController.AddCard<Card>(PileType.DrawDeck, Player.PlayerInfo.CardSet);
    }

    private void Awake()
    {
        if (cardController) cardController.OnPlayCard.AddListener(modifiers.OnPlayCard);
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
        Debug.Log("我的回合，抽卡！！！");
        Energy = player.PlayerInfo.BaseEnergy;
        cardController.Draw(Player.PlayerInfo.DrawNum);
        modifiers.OnTurnStart();
    }

    public void EndTurn()
    {
        Debug.Log("回合结束");
    }

    public void AddAnonymousPersonalityModifier(Personality personality, int timer = -1, DMGType type = DMGType.Normal)
    {
        if (timer == 0) return;
        if (type == DMGType.Magic)
        {
            personality.Strengthen(Strength);
            Strength = 0;
        }
        if (timer < 0)
        {
            modifiers.AddAnonymousPersonality(personality);
        }
        else
        {
            statusManager.AddAnonymousPersonalityModifier(personality, timer);
        }
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
