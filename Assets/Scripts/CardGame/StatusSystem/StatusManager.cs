
using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
using UnityEngine.Events;

public enum DMGType
{
    Normal,
    Magic
}

[RequireComponent(typeof(CardPlayerState))]
public class StatusManager : MonoBehaviour
{
    public UnityEvent<StatusCounter> OnBuff = new UnityEvent<StatusCounter>();
    [SerializeField]
    private List<StatusCounter> statusList = new List<StatusCounter>();

    private CardPlayerState player;

    public void AddAnonymousPersonalityModifier(Personality personality, int timer, DMGType type = DMGType.Normal)
    {
        if (timer == 0) return;
        if (timer < 0)
        {
            player.Player.PlayerInfo.Personality += personality;
            if (type == DMGType.Magic)
            {
                player.Player.PlayerInfo.Personality.Strengthen(player.Strength);
                player.Strength = 0;
            }
        }
        else
        {
            Status anonymous = new Status()
            {
                Modifier = new Modifier()
                {
                    PersonalityLinear = new Personality(personality),
                },
                DecreaseOnTurnEnd = timer < 0 ? 0 : 1,
            };
            AddStatusCounter(anonymous, Mathf.Max(1, timer));
            //Status需要GC
        }
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
        AddStatusCounter(anonymous, Mathf.Max(1, timer));
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
        AddStatusCounter(anonymous, Mathf.Max(1, timer));
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
        AddStatusCounter(anonymous, Mathf.Max(1, timer));
        //Status需要GC
    }


    public void AddStatusCounter(Status status, int value)
    {
        Context.PushPlayerContext(player);
        StatusCounter st = new StatusCounter { status = status, value = value };
        foreach (var modifier in player.Modifiers)
        {
            if (modifier.OnBuff != null)
            {
                Context.statusCounterStack.Push(st);
                modifier.OnBuff.Execute();
                Context.statusCounterStack.Pop();
            }
        }
        StatusCounter s = statusList.Find(x => x.status == status && x.status.Stackable);
        if (s != null)
        {
            s.value += value;
            if (s.value <= 0 && !s.status.AllowNegative)
            {
                s.status.OnRemove?.Execute();
                if (status.Modifier != null) player.RemoveModifier(s.status.Modifier);
                statusList.Remove(s);
            }
        }
        else
        {
            if (value > 0 || status.AllowNegative)
            {
                statusList.Add(st);
                if (status.Modifier != null) player.AddModifier(status.Modifier);
                status.OnAdd?.Execute();
            }

        }
        Context.PopPlayerContext();
    }


    public int GetStatusValue(string name)
    {
        Status status = StaticStatusLibrary.GetByName(name) ?? throw new SemanticException();
        return GetStatusValue(status);
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

    private void Awake()
    {
        player = GetComponent<CardPlayerState>();
    }

    private void Start()
    {
        player.OnTurnStart.AddListener(OnStartTurn);
        player.OnTurnEnd.AddListener(OnEndTurn);
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
                Context.PushPlayerContext(player);
                sig.status.OnRemove?.Execute();
                player.RemoveModifier(sig.status.Modifier);
                Context.PopPlayerContext();
                statusList.Remove(sig);
            }
        }
    }
}





