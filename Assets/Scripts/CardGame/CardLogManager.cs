using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICardOperationSubject//报社
{
    UnityEvent<ICardLogProperty, uint> OnDrawCard{get;}
    UnityEvent<ICardLogProperty, uint> OnPlayCard{get;}
    UnityEvent<ICardLogProperty, uint> OnDiscardCard{get;}
}
public interface ICardLog
{
    void Register(ICardOperationSubject from);
    void Remove(ICardOperationSubject from);
}
/// <summary>
/// 管理卡牌日志，以供查阅使用
/// </summary>
public class CardLogManager : MonoBehaviour,ICardLog
{
    private Stack<CardLog> cardLogs = new Stack<CardLog>();
    [SerializeField]
    ICardOperationSubject subject;
    public enum CardLogFindScope
    {
        ThisTurn,
        LastTurn,
        ThisBattle,
        LastCard
    }

    public static CardLogManager Instance { get; set; }
    private void Awake()
    {
        Destroy(Instance);
        Instance = this;
    }
    private void OnDisable()
    {
        Remove(subject);
    }

    private void OnEnable()
    {
        Register(subject);
    }
    public void Register(ICardOperationSubject subject)
    {
        subject.OnDrawCard.AddListener(OnDrawCard);
        subject.OnDiscardCard.AddListener(OnDiscardCard);
        subject.OnPlayCard.AddListener(OnPlayCard);
    }
    public void Remove(ICardOperationSubject subject)
    {
        subject.OnDrawCard.RemoveListener(OnDrawCard);
        subject.OnDiscardCard.RemoveListener(OnDiscardCard);
        subject.OnPlayCard.RemoveListener(OnPlayCard);
    }
    public void OnDrawCard(ICardLogProperty card,uint turn)
    {
        cardLogs.Push(new CardLog(turn,CardLogType.DrawCard, card));
    }
    public void OnPlayCard(ICardLogProperty card,uint turn)
    {
        cardLogs.Push(new CardLog(turn, CardLogType.PlayCard, card));
    }
    public void OnDiscardCard(ICardLogProperty card,uint turn)
    {
        cardLogs.Push(new CardLog(turn, CardLogType.DiscardCard, card));
    }

    /// <summary>
    /// 是否使用过卡牌类型为cardType的卡牌
    /// </summary>
    /// <param name="scope">查找范围</param>
    /// <param name="currentTurns">当前回合</param>
    /// <param name="cardType"></param>
    public bool HasUseSpcTypeCard(CardLogFindScope scope,uint currentTurn,CardType cardType)
    {
        CardLogIterator iterator = new CardLogIterator(cardLogs, scope, currentTurn);
        return Find(iterator, l => l.CardType == cardType);
    }

    //实现有关查找的功能
    //查找的范围： 本回合 本局对战 上一张卡牌 上回合

    public delegate bool Predicate(CardLog log);
    bool Find(CardLogIterator iterator,Predicate match)
    {
        while (iterator.MoveNext())
        {
            if (match(iterator.Current))
                return true;
        }
        return false;
    }
    /// <summary>
    /// 是否可以找到符合给定条件的卡牌记录
    /// </summary>
    /// <param name="scope">查找的范围</param>
    /// <param name="currentTurn">当前回合数</param>
    /// <param name="match">条件</param>
    /// <returns></returns>
    public bool CanFind(CardLogFindScope scope,uint currentTurn,Predicate match,CardLogType logType=CardLogType.PlayCard)
    {
        CardLogIterator iterator = new CardLogIterator(cardLogs, scope, currentTurn,logType);
        return Find(iterator, match);
    }
    /// <summary>
    /// 找到符合给定条件的卡牌记录的个数
    /// </summary>
    /// <param name="scope">查找的范围</param>
    /// <param name="currentTurn">当前回合数</param>
    /// <param name="match">条件</param>
    /// <returns></returns>
    public uint GetCntMeetCdt(CardLogFindScope scope, uint currentTurn, Predicate match, CardLogType logType = CardLogType.PlayCard)
    {
        CardLogIterator iterator = new CardLogIterator(cardLogs, scope, currentTurn,logType);
        return GetCnt(iterator, match);
    }
    uint GetCnt(CardLogIterator iterator,Predicate match)
    {
        uint res = 0;
        while(iterator.MoveNext())
        {
            if (match(iterator.Current))
                res++;
        }
        return res;
    }
}

