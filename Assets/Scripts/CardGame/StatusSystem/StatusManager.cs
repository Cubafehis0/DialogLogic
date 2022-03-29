
using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;

[RequireComponent(typeof(CardPlayerState))]
public class StatusManager : MonoBehaviour
{
    /// <summary>
    /// 供effect与游戏流程中的同步使用
    /// </summary>
    private static Dictionary<string, Status> statusDictionary = new Dictionary<string, Status>();

    [SerializeField]
    private List<StatusCounter> statusList = new List<StatusCounter>();

    public void AddAnonymousPersonalityModifier(Personality personality, int timer)
    {
        if (timer == 0) return;
        Status anonymous = new Status()
        {
            Modifier = new Modifier()
            {
                PersonalityLinear = new Personality(personality),
            },
            DecreaseOnTurnEnd = timer < 0 ? 0 : 1,
        };
        AddStatusCounter(GetComponent<CardPlayerState>(), anonymous, Mathf.Max(1, timer));
        //Status需要GC
    }
    public void AddAnonymousSpeechModifer(SpeechArt speechArt, int timer)
    {
        if (timer == 0) return;
        Status anonymous = new Status()
        {
            Modifier = new Modifier()
            {
                SpeechLinear = new SpeechArt(speechArt),
            },
            DecreaseOnTurnEnd = timer < 0 ? 0 : 1,
        };
        AddStatusCounter(GetComponent<CardPlayerState>(), anonymous, Mathf.Max(1, timer));
        //Status需要GC
    }
    public void AddAnonymousCostModifer(CostModifier costModifier, int timer)
    {
        if (timer == 0) return;
        Status anonymous = new Status()
        {
            Modifier = new Modifier()
            {
                CostModifier = new CostModifier(costModifier),
            },
            DecreaseOnTurnEnd = timer < 0 ? 0 : 1,
        };
        AddStatusCounter(GetComponent<CardPlayerState>(), anonymous, Mathf.Max(1, timer));
        //Status需要GC
    }
    public void AddAnonymousFocusModifer(SpeechType speechType, int timer)
    {
        if (timer == 0) return;
        Status anonymous = new Status()
        {
            Modifier = new Modifier()
            {
                Focus = speechType,
            },
            DecreaseOnTurnEnd = timer < 0 ? 0 : 1,
        };
        AddStatusCounter(GetComponent<CardPlayerState>(), anonymous, Mathf.Max(1, timer));
        //Status需要GC
    }


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
                    player.RemoveModifier(s.status.Modifier);
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
        player.AddModifier(status.Modifier);
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
                player.RemoveModifier(sig.status.Modifier);
                Context.PopPlayerContext();
                statusList.Remove(sig);
            }
        }
    }
}





