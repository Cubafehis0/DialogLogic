using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;
using System;

[Serializable]
public class Timer<T>
{
    public T value;
    public int? cd;

    public Timer(T value, int? cd)
    {
        this.value = value;
        this.cd = cd;
    }
}


public class CardPlayerState : MonoBehaviour, IPlayerStateChange, IPersonalityGet
{
    [SerializeField]
    private Personality basePersonality = new Personality(0, 0, 0, 0);
    [SerializeField]
    private SpeechArt baseSpeechArt = new SpeechArt(0, 0, 0, 0);
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private int energy = 0;
    [SerializeField]
    private int pressure = 0;
    [SerializeField]
    private int drawNum = 5;
    [SerializeField]
    private uint handCardMaxNum = 10;
    [SerializeField]
    private bool drawBan = false;
    [SerializeField]
    private SpeechType? baseSpeechType = null;

    #region 玩家基本信息配置方法
    public void SetBasePersonality(Personality personality)
    {
        this.basePersonality= personality;
    }
    public void SetBaseEnergy(int energy)
    {
        this.energy = energy;
    }
    public void SetDrawCardNum(int drawNum)
    {
        this.drawNum = drawNum;
    }
    public void SetMaxCardNum(uint handCardMaxNum)
    {
        this.handCardMaxNum = handCardMaxNum;
    }
    public void SetBasePressure(int basePressure)
    {
        throw new Exception("没有包含对应属性");
        //this.basePressure = basePressure;
    }
    public void SetMaxPressure(int maxPressure)
    {
        throw new Exception("没有包含对应属性");
        //this.maxPressure = maxPressure;
    }
    public void SetHealth(int health)
    {
        throw new Exception("没有包含对应属性");
        //this.health = health;
    }
    #endregion

    private Pile<Card> hand = new Pile<Card>();
    private Pile<Card> drawPile = new Pile<Card>();
    private Pile<Card> discardPile = new Pile<Card>();
    private UnityEvent onPersonalityChange = new UnityEvent();

    private static CardPlayerState instance = null;
    [SerializeField]
    private List<Timer<Personality>> personalityModifiers = new List<Timer<Personality>>();
    [SerializeField]
    private List<Timer<SpeechArt>> speechModifiers = new List<Timer<SpeechArt>>();
    [SerializeField]
    private List<Timer<SpeechType>> focusSpeechModifiers = new List<Timer<SpeechType>>();
    public List<Timer<CostModifier>> costModifers = new List<Timer<CostModifier>>();

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
    private List<GameScript> scriptList = new List<GameScript>();

    public static CardPlayerState Instance { get => instance; }
    public Personality FinalPersonality
    {
        get
        {
            Personality ret = basePersonality;
            foreach (var modifer in personalityModifiers)
            {
                ret += modifer.value;
            }
            return ret;
        }
    }
    public SpeechArt FinalSpeechArt
    {
        get
        {
            SpeechArt ret = baseSpeechArt;
            foreach (var modifer in speechModifiers)
            {
                ret += modifer.value;
            }
            return ret;
        }
    }
    public SpeechType? FocusSpeechType
    {
        get
        {
            SpeechType? ret = baseSpeechType;
            foreach (var modifier in focusSpeechModifiers)
            {
                ret = modifier.value;
            }
            return ret;
        }
    }
    public Personality Personality { get => FinalPersonality; }
    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            OnEnergyChange.Invoke();
        }
    }

    public int Pressure { get => pressure; set => pressure = value; }
    public Pile<Card> Hand { get => hand; }
    public Pile<Card> DrawPile { get => drawPile; }
    public Pile<Card> DiscardPile { get => discardPile; }
    public StatusManager StatusManager => GetComponent<StatusManager>();
    public int DrawNum { get => (int)drawNum; set => drawNum = value; }
    public bool IsHandFull => Hand.Count == handCardMaxNum;
    public UnityEvent OnValueChange => onPersonalityChange;
    public bool DrawBan { get => drawBan; set => drawBan = value; }
    public Player Player { get => player;}

    private void Awake()
    {
        instance = this;
    }

    public void EnableGameScript(GameScript script)
    {
        if (script.OnAfterPlayCard != null) OnPlayCard.AddListener(script.OnAfterPlayCard.Execute);
        if (script.OnTurnStart != null) OnPlayCard.AddListener(script.OnTurnStart.Execute);
        scriptList.Add(script);
    }

    public void DisableGameScript(GameScript script)
    {
        if (script.OnAfterPlayCard != null) OnPlayCard.RemoveListener(script.OnAfterPlayCard.Execute);
        if (script.OnTurnStart != null) OnPlayCard.RemoveListener(script.OnTurnStart.Execute);
        scriptList.Remove(script);
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
        throw new System.NotImplementedException();
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
        if (Energy < card.FinalCost)
        {
            //能量不足
            Debug.Log("能量不足");
        }
        else
        {
            //目标有效且可以使用
            Energy -= card.FinalCost;
            //调用Play时已经检查并扣除费用
            Debug.Log("使用卡牌： " + card.name);

            Context.PushPlayerContext(this);
            Context.PushCardContext(card);
            OnPlayCard.Invoke();
            if (card.Effects == null) Debug.Log("空效果");

            card.Effects.Execute();
            Context.PopCardContext();
            Context.PopPlayerContext();
            Hand.MigrateTo(card, discardPile);
        }
    }

    public void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
    }

    public void DiscardCard(uint cid)
    {
        throw new System.NotImplementedException();
    }

    public void AddPersonalityModifer(Personality personality, int? timer)
    {
        personalityModifiers.Add(new Timer<Personality>(personality, timer));
        onPersonalityChange.Invoke();
    }
    public void AddSpeechModifer(SpeechArt speechArt, int? timer)
    {
        speechModifiers.Add(new Timer<SpeechArt>(speechArt, timer));
    }
    public void AddFocusModifer(SpeechType speechArt, int? timer)
    {
        focusSpeechModifiers.Add(new Timer<SpeechType>(speechArt, timer));
    }

    public void AddCostModifer(CostModifier modifier, int? timer)
    {
        costModifers.Add(new Timer<CostModifier>(modifier, timer));
    }

    public void RemoveCostModifer(CostModifier modifier)
    {
        //有缺陷
        costModifers.RemoveAll(item => item.value == modifier);
    }

    public void RandomReveal(int num)
    {
        throw new System.NotImplementedException();
    }

    public void RandomReveal(SpeechType type, int num)
    {
        throw new System.NotImplementedException();
    }

    public void SelectChoice(ChoiceSlot slot)
    {
        //判断是否可选
        if (slot.Locked || (FocusSpeechType != null && FocusSpeechType == slot.SlotType))
        {
            return;
        }
        else
        {
            RawSelectChoice(slot);
        }
    }

    /// <summary>
    /// 不判断是否可选，强制选择选项
    /// </summary>
    /// <param name="slot"></param>
    public void RawSelectChoice(ChoiceSlot slot)
    {
        int dis = Personality.MaxDistance(FinalPersonality, slot.Choice.JudgeValue);
        SpeechArt speech = Instance.FinalSpeechArt;
        int modifier = slot.SlotType switch
        {
            SpeechType.Normal => speech[SpeechType.Normal],
            SpeechType.Cheat => speech[SpeechType.Cheat],
            SpeechType.Threaten => speech[SpeechType.Threaten],
            SpeechType.Persuade => speech[SpeechType.Persuade],
            _ => 0,
        };
        int randomEPS = MyMath.GetRandomJudge(jp);
        DialogSystem.Instance.ForceSelectChoice(slot.Choice, dis <= randomEPS + modifier);
    }



    public void StartTurn()
    {
        Debug.Log("我的回合，抽卡！！！");
        OnStartTurn.Invoke();
        Energy = 4;
        Draw((uint)drawNum);
    }

    public void EndTurn()
    {
        Debug.Log("回合结束");
        OnEndTurn.Invoke();
        DiscardAll();
        personalityModifiers.ForEach(it => it.cd--);
        personalityModifiers.RemoveAll(it => it.cd <= 0);
        onPersonalityChange.Invoke();
        speechModifiers.ForEach(it => it.cd--);
        speechModifiers.RemoveAll(it => it.cd <= 0);
        focusSpeechModifiers.ForEach(it => it.cd--);
        focusSpeechModifiers.RemoveAll(it => it.cd <= 0);
        foreach (Card card in Hand)
            card.TemporaryActivate = false;
    }

    public void OnStartGame()
    {
        Debug.Log("init");
        foreach (string name in Player.CardSet)
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
        basePersonality += delta;
    }
}
