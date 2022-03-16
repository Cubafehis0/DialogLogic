using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 表示卡牌类型
/// </summary>
public enum CardType
{
    Lgc,
    Spt,
    Mrl,
    Imm,
    Rdb,
    Ags
}

public class Card : MonoBehaviour
{
    public uint staticID;
    public IPile parentPile;
    public CardType CardType{ get;set;}

    public List<string> hold_effect;
    public List<string> condition;
    public List<string> effect;
    public List<string> post_effect;
    public List<string> pull_effect;

    public List<int> hold_effect_scale;
    public List<int> condition_scale;
    public List<int> effect_scale;
    public List<int> post_effect_scale;
    public List<int> pull_effect_scale;

    public string title;
    public string CdtDesc {
        get => GetDesc(condition, condition_scale);
    }
    public string EftDesc { 
        get
        {
            string ret = "";
            string tmp = GetDesc(hold_effect,hold_effect_scale);
            if (!string.IsNullOrEmpty(tmp))
                ret += "持有时:" + tmp;

            tmp = GetDesc(effect,effect_scale) + GetDesc(post_effect,post_effect_scale);
            if (!string.IsNullOrEmpty(tmp))
                ret += "打出时:" + tmp;

            tmp = GetDesc(pull_effect,pull_effect_scale);
            if (!string.IsNullOrEmpty(tmp))
                ret += "抽取时:" + tmp;
            return ret;
        }
    }

    public string meme;
    public Card()
    {

    }
    public Card(CardEntity cardEntity)
    {

    }
    public Card(uint staticID, string hold_effect, string hold_effect_scale, string condition, string condition_scale, string effect, string effect_scale, string post_effect, string post_effect_scale)
    {
        this.staticID = staticID;
        this.hold_effect = new List<string>(hold_effect.Split(';'));
        this.condition = new List<string>(condition.Split(';'));
        this.effect = new List<string>(effect.Split(';'));
        this.post_effect = new List<string>(post_effect.Split(';'));

        this.hold_effect_scale = new List<int>(hold_effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.condition_scale = new List<int>(condition_scale.Split(';').Select(x => Int32.Parse(x)));
        this.effect_scale = new List<int>(effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.post_effect_scale = new List<int>(post_effect_scale.Split(';').Select(x => Int32.Parse(x)));
    }

    public Card(uint staticID, List<string> hold_effect, List<int> hold_effect_scale, List<string> condition, List<int> condition_scale, List<string> effect, List<int> effect_scale, List<string> post_effect, List<int> post_effect_scale)
    {
        this.staticID = staticID;
        this.hold_effect = new List<string>(hold_effect);
        this.condition = new List<string>(condition);
        this.effect = new List<string>(effect);
        this.post_effect = new List<string>(post_effect);

        this.hold_effect_scale = new List<int>(hold_effect_scale);
        this.condition_scale = new List<int>(condition_scale);
        this.effect_scale = new List<int>(effect_scale);
        this.post_effect_scale = new List<int>(post_effect_scale);
    }

    public void Refresh(CardEntity entity)
    {
        uint.TryParse(entity.id,out staticID);
        this.title = entity.name;
        this.meme = entity.meme;
        EftAndCdtNameImage nameImage = EftAndCdtNameImage.GetInstance();
        this.hold_effect = GetEffects(entity.hold_effect);
        this.condition = GetEffects(entity.condition);
        this.effect = GetEffects(entity.effect);
        this.post_effect = GetEffects(entity.post_effect);
        this.pull_effect = GetEffects(entity.pull_effect);

        this.hold_effect_scale = GetEffectsScale(entity.hold_effect_scale, hold_effect.Count);
        this.condition_scale = GetEffectsScale(entity.condition_scale, condition.Count);
        this.effect_scale = GetEffectsScale(entity.effect_scale, effect.Count);
        this.post_effect_scale = GetEffectsScale(entity.post_effect_scale, post_effect.Count);
        this.pull_effect_scale = GetEffectsScale(entity.pull_effect_scale, pull_effect.Count);
    }

    List<string> GetEffects(string effects)
    {
        if (effects == null) return new List<string>();
        var rawEffectList = new List<string>(effects.Split(';', '；')).FindAll(e => !string.IsNullOrEmpty(e)).ToList();
        List<string> res = new List<string>();
        foreach(var e in rawEffectList)
        {
            string image = EftAndCdtNameImage.GetInstance().GetImageName(e);
            if (image != null)
            {
                res.Add(image);
            }
        }
        return res;
    
    }
    List<int> GetEffectsScale(string scales,int eftCnt)
    {
        List<int> retval = new List<int>();
        for (int i = 0; i < eftCnt; i++)
            retval.Add(0);
        int cnt = 0;
        if (!string.IsNullOrEmpty(scales))
        {
            scales = scales.Trim();
            string[] nums = scales.Split(new char[] { ';', '；' });
            foreach (string num in nums)
            {
                if (cnt == eftCnt)
                    break;
                int.TryParse(num,out int t);
                retval[cnt++]=t;
            }
        }
        return retval;
    }

    string GetDesc(List<string> effects,List<int> scales)
    {
        string ret = "";
        for(int i=0;i<effects.Count;i++)
        {
            ret += EffectDesc.GetDesc(effects[i], scales[i]);
        }
        return ret;
    }
}