using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICardOperationSubject//����
{
    UnityEvent<Card,uint> OnDrawCard{get;}
    UnityEvent<Card,uint> OnPlayCard{get;}
    UnityEvent<Card,uint> OnDiscardCard{get;}
}
public interface ICardLog
{
    void Register(ICardOperationSubject subject);
    void Remove(ICardOperationSubject subject);
}
/// <summary>
/// ��������־���Թ�����ʹ��
/// </summary>
public class CardLogManager : MonoBehaviour,ICardLog
{
    Stack<CardLog> cardLogs;
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
        cardLogs = new Stack<CardLog>();
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
    public void OnDrawCard(Card card,uint turn)
    {
        cardLogs.Push(new CardLog(turn,CardLogType.DrawCard, card));
    }
    public void OnPlayCard(Card card,uint turn)
    {
        cardLogs.Push(new CardLog(turn, CardLogType.PlayCard, card));
    }
    public void OnDiscardCard(Card card,uint turn)
    {
        cardLogs.Push(new CardLog(turn, CardLogType.DiscardCard, card));
    }

    /// <summary>
    /// �Ƿ�ʹ�ù���������ΧŹcardType�Ŀ���
    /// </summary>
    /// <param name="scope">���ҷ�Χ</param>
    /// <param name="currentTurns">��ǰ�غ�</param>
    /// <param name="cardType"></param>
    public bool HasUseSpcTypeCard(CardLogFindScope scope,uint currentTurn,CardType cardType)
    {
        CardLogIterator iterator = new CardLogIterator(cardLogs, scope, currentTurn);
        return Find(iterator, l => l.CardType == cardType);
    }

    //ʵ���йز��ҵĹ���
    //���ҵķ�Χ�� ���غ� ���ֶ�ս ��һ�ſ��� �ϻغ�

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
    /// �Ƿ�����ҵ����ϸ��������Ŀ��Ƽ�¼
    /// </summary>
    /// <param name="scope">���ҵķ�Χ</param>
    /// <param name="currentTurn">��ǰ�غ���</param>
    /// <param name="match">����</param>
    /// <returns></returns>
    public bool CanFind(CardLogFindScope scope,uint currentTurn,Predicate match,CardLogType logType=CardLogType.PlayCard)
    {
        CardLogIterator iterator = new CardLogIterator(cardLogs, scope, currentTurn,logType);
        return Find(iterator, match);
    }
    /// <summary>
    /// �ҵ����ϸ��������Ŀ��Ƽ�¼�ĸ���
    /// </summary>
    /// <param name="scope">���ҵķ�Χ</param>
    /// <param name="currentTurn">��ǰ�غ���</param>
    /// <param name="match">����</param>
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

