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

public class CardPlayerState : MonoBehaviour, IPlayerStateChange, IPersonalityGet
{
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private ChooseSystem chooseSystem = null;
    [SerializeField]
    private bool drawBan = false;

    private Pile<Card> hand = new Pile<Card>();
    private Pile<Card> drawPile = new Pile<Card>();
    private Pile<Card> discardPile = new Pile<Card>();
    private UnityEvent onPersonalityChange = new UnityEvent();

    private static CardPlayerState instance = null;

    [HideInInspector]
    public UnityEvent OnEnergyChange = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPlayCard = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStartTurn = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEndTurn = new UnityEvent();
    //不同判定补正的概率
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };
    public ModifierGroup Modifiers = new ModifierGroup();
    public static CardPlayerState Instance { get => instance; }

    public ChooseSystem ChooseSystem { get => chooseSystem; }

    public Personality FinalPersonality
    {
        get
        {
            var res = player.PlayerInfo.Personality + Modifiers.PersonalityLinear;
            //foreach (var card in Hand)
            //{
            //    res += card.info.handModifier.PersonalityLinear;
            //}
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
    public Pile<Card> Hand { get => hand; }
    public Pile<Card> DrawPile { get => drawPile; }
    public Pile<Card> DiscardPile { get => discardPile; }
    public StatusManager StatusManager => GetComponent<StatusManager>();
    public int DrawNum { get => player.PlayerInfo.DrawNum; set => player.PlayerInfo.DrawNum = value; }
    public bool IsHandFull => Hand.Count == player.PlayerInfo.MaxCardNum;
    public UnityEvent OnValueChange => onPersonalityChange;
    public bool DrawBan { get => drawBan; set => drawBan = value; }
    public Player Player { get => player; }

    private void Awake()
    {
        instance = this;
        Hand.OnAdd.AddListener(x => Modifiers.Add(x.info.handModifier));
        Hand.OnRemove.AddListener(x => Modifiers.Remove(x.info.handModifier));
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

    public bool CanDraw()
    {
        if (DrawBan) return false;
        if (IsHandFull) return false;
        if (drawPile.Count == 0 && discardPile.Count == 0) return false;
        return true;
    }

    public void Draw(uint num)
    {
        if (DrawBan) return;
        for (int i = 0; i < num; i++)
        {
            if (IsHandFull)
            {
                //手牌满了
                return;
            }
            if (drawPile.Count == 0)
            {
                //抽牌堆为空
                if (discardPile.Count == 0)
                {
                    //没有牌可抽
                    return;
                }
                //洗牌
                Discard2Draw();
            }
            drawPile.MigrateTo(drawPile[0], Hand);
        }
    }



    public void Draw2Full()
    {
        for (int i = 0; i < 20; i++)
        {
            if (!CanDraw()) break;
            Draw(1);
        }
    }

    public void DiscardCard(Card cid)
    {
        Hand.MigrateTo(cid, discardPile);
    }

    public void DiscardAll()
    {
        Hand.MigrateAllTo(discardPile);
    }

    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="cardID">出牌id</param>
    /// <returns>是否成功出牌</returns>
    public void PlayCard(Card card)
    {
        if (!CardGameManager.Instance.WaitGUI)
        {
            StartCoroutine(PlayCardEnumerator(card));
        }
    }

    public IEnumerator PlayCardEnumerator(Card card)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard card为空");
        if (card.info == null) throw new ArgumentNullException("CardPlayerState.PlayCard card未构建");
        if (Energy < card.FinalCost)
        {
            //能量不足
            Debug.Log("能量不足");
        }
        else
        {
            Context.PushPlayerContext(this);
            Context.PushCardContext(card);
            if (card.Activated || (card.info.Requirements?.Value ?? true))
            {
                //目标有效且可以使用
                Energy -= card.FinalCost;
                Debug.Log("使用卡牌： " + card.info.Title);
                CardLogEntry log = new CardLogEntry
                {
                    Name = card.info.Name,
                    IsActive = card.Activated,
                    LogType = ActionTypeEnum.PlayCard,
                    Turn = CardGameManager.Instance.Turn,
                    CardCategory = card.info.category,
                };
                CardRecorder.Instance.AddRecordEntry(log);
                Hand.MigrateTo(card, discardPile);
                OnPlayCard.Invoke();
                if (card.info.Effects == null) Debug.Log("空效果");
                card.info.Effects.Execute();
                yield return new WaitUntil(() => CardGameManager.Instance.WaitGUI == false);
            }
            else
            {
                Debug.Log("无法使用卡牌：" + card.info.Title);
            }
            Context.PopCardContext();
            Context.PopPlayerContext();
        }
    }

    public void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
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
        Draw((uint)player.PlayerInfo.DrawNum);
    }

    public void EndTurn()
    {
        Debug.Log("回合结束");
        OnEndTurn.Invoke();
        DiscardAll();
        onPersonalityChange.Invoke();
        foreach (Card card in Hand)
            card.TemporaryActivate = false;
    }

    public void Init()
    {
        Debug.Log("init");
        foreach (string name in Player.PlayerInfo.CardSet)
        {
            Card newCard = CardGameManager.Instance.GetCardCopy(StaticCardLibrary.Instance.GetByName(name));
            drawPile.Add(newCard);
        }

        drawPile.Shuffle();
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
            "inner" => FinalPersonality.Inner,
            "outside" => FinalPersonality.Outside,
            "logic" => FinalPersonality.Logic,
            "spritial" => FinalPersonality.Spritial,
            "moral" => FinalPersonality.Moral,
            "immoral" => FinalPersonality.Immoral,
            "roundabout" => FinalPersonality.Roundabout,
            "aggressive" => FinalPersonality.Aggressive,
            "hand_count" => Hand.Count,
            "draw_count" => DrawPile.Count,
            "discard_count" => DiscardPile.Count,
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
        return name switch
        {
            "preach_total" =>
            (from x in CardRecorder.Instance.cardLogs
             where x.Name == name
             && x.LogType == ActionTypeEnum.PlayCard
             select x).Count(),
            "preach_thisturn" =>
            (from x in CardRecorder.Instance.cardLogs
             where x.Name == name
         && x.LogType == ActionTypeEnum.PlayCard
         && x.Turn == CardGameManager.Instance.Turn
             select x).Count(),
            "activate_count" => CardRecorder.Instance.QueryTotalActive(),
            "logic_combo" => CardRecorder.Instance.QueryCombo(CardCategory.Lgc),
            "immoral_combo" => CardRecorder.Instance.QueryCombo(CardCategory.Imm),
            "spirital_combo" => CardRecorder.Instance.QueryCombo(CardCategory.Spt),
            "moral_combo" => CardRecorder.Instance.QueryCombo(CardCategory.Mrl),
            _ => throw new PropNotFoundException()
        };
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
