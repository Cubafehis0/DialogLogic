using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;
using System;
using System.Xml.Serialization;
using CardGame.Recorder;

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
    private Player player = null;
    [SerializeField]
    private int energy = 0;
    [SerializeField]
    private int pressure = 0;
    [SerializeField]
    private Personality additionalPersonality = new Personality();
    [SerializeField]
    private int drawNum = 5;
    [SerializeField]
    private uint handCardMaxNum = 10;
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
    //��ͬ�ж������ĸ���
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };
    public ModifierGroup Modifiers = new ModifierGroup();

    public static CardPlayerState Instance { get => instance; }
    public Personality FinalPersonality
    {
        get => player.PlayerInfo.Personality + AdditionalPersonality + Modifiers.PersonalityLinear;
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
    public Player Player { get => player; }
    public Personality AdditionalPersonality { get => additionalPersonality; set => additionalPersonality = value; }

    private void Awake()
    {
        instance = this;
    }

    public void AddModifier(Modifier script)
    {
        if (script.OnPlayCard != null) OnPlayCard.AddListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnPlayCard.AddListener(script.OnTurnStart.Execute);
        Modifiers.Add(script);
    }

    public void RemoveModifier(Modifier script)
    {
        if (script.OnPlayCard != null) OnPlayCard.RemoveListener(script.OnPlayCard.Execute);
        if (script.OnTurnStart != null) OnPlayCard.RemoveListener(script.OnTurnStart.Execute);
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
                //��������
                return;
            }
            if (drawPile.Count == 0)
            {
                //���ƶ�Ϊ��
                if (discardPile.Count == 0)
                {
                    //û���ƿɳ�
                    return;
                }
                //ϴ��
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
    /// ����
    /// </summary>
    /// <param name="cardID">����id</param>
    /// <returns>�Ƿ�ɹ�����</returns>
    public void PlayCard(Card card)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardΪ��");
        if (card.info == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardδ����");
        if (Energy < card.FinalCost)
        {
            //��������
            Debug.Log("��������");
        }
        else
        {
            Context.PushPlayerContext(this);
            Context.PushCardContext(card);
            if (card.Activated || (card.info.Requirements?.Value ?? true))
            {
                //Ŀ����Ч�ҿ���ʹ��
                Energy -= card.FinalCost;
                Debug.Log("ʹ�ÿ��ƣ� " + card.info.Title);
                CardLogEntry log = new CardLogEntry
                {
                    Name = card.info.Name,
                    IsActive = card.Activated,
                    LogType = CardLogEntryEnum.PlayCard,
                    Turn = CardGameManager.Instance.turn,
                    CardCategory = card.info.category,
                };
                CardRecorder.Instance.AddRecordEntry(log);
                OnPlayCard.Invoke();
                if (card.info.Effects == null) Debug.Log("��Ч��");
                card.info.Effects.Execute();
                Hand.MigrateTo(card, discardPile);
            }
            else
            {
                Debug.Log("�޷�ʹ�ÿ��ƣ�" + card.info.Title);
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

    public void SelectChoice(ChoiceSlot slot)
    {
        //�ж��Ƿ��ѡ
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
    /// ���ж��Ƿ��ѡ��ǿ��ѡ��ѡ��
    /// </summary>
    /// <param name="slot"></param>
    public void RawSelectChoice(ChoiceSlot slot)
    {
        int dis = Personality.MaxDistance(FinalPersonality, slot.Choice.JudgeValue);
        SpeechArt speech = FinalSpeechArt;
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
        Debug.Log("�ҵĻغϣ��鿨������");
        OnStartTurn.Invoke();
        Energy = 4;
        Draw((uint)drawNum);
    }

    public void EndTurn()
    {
        Debug.Log("�غϽ���");
        OnEndTurn.Invoke();
        DiscardAll();
        onPersonalityChange.Invoke();
        foreach (Card card in Hand)
            card.TemporaryActivate = false;
    }

    public void OnStartGame()
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
}
