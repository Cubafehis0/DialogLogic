
using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
using SemanticTree.PlayerSemantics;

public class StatusManager : MonoBehaviour
{
    /// <summary>
    /// 供effect与游戏流程中的同步使用
    /// </summary>
    private static Dictionary<string, Status> statusDictionary = new Dictionary<string, Status>();

    private List<StatusCounter> statusList = new List<StatusCounter>();

    public void AddStatusCounter(CardPlayerState player, string name, int value)
    {
        if (!statusDictionary.ContainsKey(name))
        {
            Debug.LogError("状态不存在");
            return;
        }
        AddStatusCounter(player, statusDictionary[name], value);
    }

    public void AddStatusCounter(CardPlayerState player, Status status, int value)
    {
        foreach (StatusCounter s in statusList)
        {
            if ((s.status == status) && s.status.Stackable)
            {
                s.value += value;
                if (s.value < 0 && !s.status.AllowNegative)
                {
                    PlayerNode.PushPlayerContext(player);
                    s.status.OnRemove?.Execute();
                    if (status.OnAfterPlayCard != null) player.OnPlayCard.RemoveListener(status.OnAfterPlayCard.Execute);
                    if (status.OnTurnStart != null) player.OnPlayCard.RemoveListener(status.OnAfterPlayCard.Execute);
                    PlayerNode.PopPlayerContext();
                    statusList.Remove(s);
                    return;
                }

            }
        }
        StatusCounter st = new StatusCounter { status = status, value = value };
        statusList.Add(st);
        PlayerNode.PushPlayerContext(player);
        if (status.OnAfterPlayCard != null) player.OnPlayCard.AddListener(status.OnAfterPlayCard.Execute);
        if (status.OnTurnStart != null) player.OnPlayCard.AddListener(status.OnAfterPlayCard.Execute);
        status.OnAdd?.Execute();
        PlayerNode.PopPlayerContext();
    }

    public static Status GetStatus(string name)
    {
        return statusDictionary.ContainsKey(name) ? statusDictionary[name] : null;
    }

    public int GetStatusValue(string name)
    {
        return statusDictionary.ContainsKey(name) ? GetStatusValue(statusDictionary[name]) : 0;
    }

    public int GetStatusValue(Status status)
    {
        foreach (StatusCounter s in statusList)
        {
            if (s.status == status)
            {
                return s.value;
            }
        }
        return 0;
    }


    private void Start()
    {
        CardPlayerState.Instance.OnStartTurn.AddListener(OnStartTurn);
        CardPlayerState.Instance.OnEndTurn.AddListener(OnEndTurn);
    }


    public void OnStartTurn()
    {
        for (int i = statusList.Count - 1; i >= 0; i--)
        {
            StatusCounter sig = statusList[i];
            sig.value -= sig.status.DecreaseOnTurnStart;
            if (sig.value == 0 || (sig.value < 0 && sig.status.AllowNegative == false))
            {
                statusList.Remove(sig);
            }
        }
    }

    public void OnEndTurn()
    {
        for (int i = statusList.Count - 1; i >= 0; i--)
        {
            StatusCounter sig = statusList[i];
            sig.value -= sig.status.DecreaseOnTurnEnd;
            if (sig.value == 0 || (sig.value < 0 && sig.status.AllowNegative == false))
            {
                statusList.Remove(sig);
            }
        }
    }
}





