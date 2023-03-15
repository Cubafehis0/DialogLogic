using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;


public class StatusController : MonoBehaviour, IStatusManager
{
    private ModifierGroup modifiers = new ModifierGroup();
    private List<StatusCounter> statusList = new List<StatusCounter>();
    public ModifierGroup Modifiers { get => modifiers; }

    public void AddAnonymousPersonalityModifier(Personality personality, int timer)
    {
        if (timer <= 0)
        {
            modifiers.AddAnonymousPersonality(personality);
        }
        else
        {
            Status anonymous = new Status()
            {
                Modifier = new Modifier()
                {
                    PersonalityLinear = () => new Personality(personality),
                },
                DecreaseOnTurnEnd = 1,
            };
            AddStatusCounter(anonymous, timer);
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
                SpeechLinear = () => new SpeechArt(speechArt),
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
                CostModifier = new CostModifier(costModifier.Condition, costModifier.num),
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
                Focus = () => speechType,
            },
            DecreaseOnTurnEnd = timer < 0 ? 0 : 1,
        };
        AddStatusCounter(anonymous, Mathf.Max(1, timer));
        //Status需要GC
    }


    public void AddStatusCounter(string name, int value)
    {
        AddStatusCounter(StaticStatusLibrary.GetByName(name), value);
    }

    public void AddStatusCounter(Status status, int value)
    {
        //Context.PushPlayerContext(player);
        StatusCounter st = new StatusCounter { Status = status, Value = value };
        //foreach (var modifier in modifiers)
        //{
        //    if (modifier.OnBuff != null)
        //    {
        //        Context.statusCounterStack.Push(st);
        //        modifier.OnBuff.Execute();
        //        Context.statusCounterStack.Pop();
        //    }
        //}
        StatusCounter s = statusList.Find(x => x.Status == status && x.Status.Stackable);
        if (s != null)
        {
            s.Value += value;
            if (s.Value <= 0 && !s.Status.AllowNegative)
            {
                s.Status.OnRemove?.Invoke();
                if (status.Modifier != null) modifiers.Remove(s.Status.Modifier);
                statusList.Remove(s);
            }
        }
        else
        {
            if (value > 0 || status.AllowNegative)
            {
                statusList.Add(st);
                if (status.Modifier != null) modifiers.Add(status.Modifier);
                status.OnAdd?.Invoke();
            }

        }
        //Context.PopPlayerContext();
    }


    public int GetStatusValue(string name)
    {
        Status status = StaticStatusLibrary.GetByName(name);
        return GetStatusValue(status);
    }

    public int GetStatusValue(Status status)
    {
        foreach (StatusCounter s in statusList)
        {
            if (s.Status == status)
            {
                return s.Value;
            }
        }
        return 0;
    }


    public void OnTurnStart()
    {
        List<StatusCounter> counters = new List<StatusCounter>(statusList);
        foreach (var counter in counters)
        {
            counter.Value -= counter.Status.DecreaseOnTurnStart;
            if (!counter.Status.AllowNegative && counter.Value <= 0)
            {
                counter.Status.OnRemove?.Invoke();
                modifiers.Remove(counter.Status.Modifier);
                statusList.Remove(counter);
            }
        }
    }

    public void OnTurnEnd()
    {
        List<StatusCounter> counters = new List<StatusCounter>(statusList);
        foreach (var counter in counters)
        {
            counter.Value -= counter.Status.DecreaseOnTurnEnd;
            if (!counter.Status.AllowNegative && counter.Value <= 0)
            {
                counter.Status.OnRemove?.Invoke();
                modifiers.Remove(counter.Status.Modifier);
                statusList.Remove(counter);
            }
        }
    }
}





