using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogIterator
{
    List<CardLog> logList;
    IEnumerator enumerator;
    public CardLogIterator(Stack<CardLog> cardLogs, CardLogManager.CardLogFindScope scope, uint turn, CardLogType cardLogType = CardLogType.PlayCard)
    {
        logList = new List<CardLog>();
        var e = cardLogs.GetEnumerator();
        while (e.MoveNext())
        {
            CardLog log = e.Current;
            if (log.LogType != cardLogType) continue;
            if (scope == CardLogManager.CardLogFindScope.ThisTurn)
            {
                if (log.Turn < turn) break;
            }
            else if (scope == CardLogManager.CardLogFindScope.LastCard)
            {
                if (logList.Count >= 1) break;
            }
            else if (scope == CardLogManager.CardLogFindScope.LastTurn)
            {
                if (log.Turn == turn) continue;
                else if (log.Turn + 1 < turn) break;
            }
            logList.Add(log);
        }
    }
    public bool MoveNext()
    {
        if (enumerator == null)
        {
            enumerator = logList.GetEnumerator();
        }
        return enumerator.MoveNext();
    }
    public CardLog Current { get => enumerator.Current as CardLog; }
}
