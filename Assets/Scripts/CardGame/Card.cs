using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class Card
{
    public uint id;
    public List<string> hold_effect;
    public List<string> condition;
    public List<string> effect;
    public List<string> post_effect;
    
    public List<int> hold_effect_scale;
    public List<int> condition_scale;
    public List<int> effect_scale;
    public List<int> post_effect_scale;
    public Card(uint id, string hold_effect,string hold_effect_scale,string condition,string condition_scale,string effect, string effect_scale,string post_effect,string post_effect_scale)
    {
        this.id = id;
        this.hold_effect = new List<string>(hold_effect.Split(';'));
        this.condition = new List<string>(condition.Split(';'));
        this.effect = new List<string>(effect.Split(';'));
        this.post_effect = new List<string>(post_effect.Split(';'));

        this.hold_effect_scale = new List<int>(hold_effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.condition_scale = new List<int>(condition_scale.Split(';').Select(x => Int32.Parse(x)));
        this.effect_scale = new List<int>(effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.post_effect_scale = new List<int>(post_effect_scale.Split(';').Select(x => Int32.Parse(x)));
    }

    public bool Play()
    {
        bool res = true;
        if (Examine(condition,condition_scale))
        {
            res = Examine(effect,effect_scale);
            Examine(post_effect,post_effect_scale);
            return res;
        }
        return false;
    }

    public bool CheckCanPlay()
    {
        return Examine(condition, condition_scale);
    }
    public bool Hold()
    {
        return Examine(hold_effect, hold_effect_scale);
    }
    public bool Examine(List<string> effects,List<int> scale)
    {
        bool res = true;
        for (int i = 0; i < effects.Count; i++)
        {
            res = res && EffectManager.Instance.Execute(effects[i],scale[i]);
        }
        return res;
    }
    public bool Examine(List<string> effects,Character chara)
    {
        bool res = true;
        foreach (var key in effects)
        {
            res = res && EffectManager.Instance.Execute(key,chara);
        }
        return res;
    }
}