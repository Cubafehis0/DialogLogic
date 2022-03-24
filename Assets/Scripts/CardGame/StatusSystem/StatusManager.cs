
using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
public class StatusManager : MonoBehaviour
{
    /// <summary>
    /// 供effect与游戏流程中的同步使用
    /// </summary>
    private static Dictionary<string, Status> statusDictionary = new Dictionary<string, Status>();

    [SerializeField]
    private List<StatusCounter> statusList = new List<StatusCounter>();

    public void AddStatusCounter(CardPlayerState player, Status status, int value)
    {
        foreach (StatusCounter s in statusList)
        {
            if ((s.status == status) && s.status.Stackable)
            {
                s.value += value;
                if (s.value <= 0 && !s.status.AllowNegative)
                {
                    Context.PushPlayerContext(player);
                    s.status.OnRemove.Execute();
                    player.DisableGameScript(s.status);
                    Context.PopPlayerContext();
                    statusList.Remove(s);
                }
                return;
            }
        }
        if (value <= 0 && !status.AllowNegative) return;
        StatusCounter st = new StatusCounter { status = status, value = value };
        statusList.Add(st);
        Context.PushPlayerContext(player);
        player.EnableGameScript(status);
        status.OnAdd.Execute();
        Context.PopPlayerContext();
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
            if (sig.value == 0 || (sig.value < 0 && !sig.status.AllowNegative))
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
            if (sig.value == 0 || (sig.value < 0 && !sig.status.AllowNegative))
            {
                var player = CardPlayerState.Instance;
                Context.PushPlayerContext(player);
                sig.status.OnRemove.Execute();
                player.DisableGameScript(sig.status);
                Context.PopPlayerContext();
                statusList.Remove(sig);
            }
        }
    }
}





