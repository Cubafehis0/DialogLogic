using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using CardEffects;


public class Effect
{
    public delegate bool Fun(int a);
    public Fun fun;
}
public class Signal
{
    public bool flag;  //是否回合结束时清除 
    public int lastTurns; //持续次数
    public string effectKey; //对应效果的名称
    public int arg; //参数
    public List<int> args; //额外参数

    public Signal(bool flag, int lastTurns, string effectKey, int arg,List<int> args = null)
    {
        this.flag = flag;
        this.lastTurns = lastTurns;
        this.effectKey = effectKey;
        this.arg = arg;
        this.args = null;
    }
}
public class EffectManager
{
    private EffectHolder effectHolder = new EffectHolder();
    private static EffectManager _instance = null;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EffectManager();
            }

            return _instance;
        }
    }

    private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();

    private Dictionary<string, Effect> InitDict()
    {
        Dictionary<string, Effect> res = new Dictionary<string, Effect>();
        var t = typeof(EffectHolder);
        foreach (var m in t.GetMethods())
        {
            Effect eff = new Effect();
            eff.fun = (Effect.Fun) Delegate.CreateDelegate(typeof(Effect.Fun), effectHolder,m);
            effects.Add(m.Name,eff);
        }
        return res;
    }
    public bool Execute(string key, int scale = 0)
    {
        Effect eff = null;
        if (effects.TryGetValue(key,out eff))
        {
            return eff.fun(scale);
        }
        else
        {
            eff = new Effect();
            var t = typeof(EffectHolder);
            var m = t.GetMethod(key);
            if (m == null)
            {
                Debug.LogError(String.Format("对应的Effect{0}不存在，请检查配置！",key));
                return true;
            }
            eff.fun = (Effect.Fun) Delegate.CreateDelegate(typeof(Effect.Fun), effectHolder, t.GetMethod(key));
            effects.Add(key,eff);
            return eff.fun(scale);
        }
    }
    public bool Execute(string key, Character chara)
    {
        return true;
    }
}