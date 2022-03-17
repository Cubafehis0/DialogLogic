using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct CardInfo
{
    public string title;
    public string desc;
    public string meme;
    public int cost;
    public int category;

    public List<string> hold_effect;
    public List<string> condition;
    public List<string> effect;
    public List<string> pull_effect;

    public string effectDesc;
    public string condtionDesc;



    
    public List<int> hold_effect_scale;
    public List<int> condition_scale;
    public List<int> effect_scale;
    public List<int> post_effect_scale;
    public List<int> pull_effect_scale;


    public string LocalizedConditionDesc
    {
        get
        {
            return "条件" + LocalizeNameList(condition, condition_scale);
        }
    }

    public string LocalizedEffectDesc
    {
        get
        {
            string ret = "";
            string tmp = LocalizeNameList(hold_effect, hold_effect_scale);
            if (!string.IsNullOrEmpty(tmp))
                ret += "倾向:" + tmp;

            //tmp = LocalizeNameList(effect, effect_scale) + LocalizeNameList(post_effect, post_effect_scale);
            if (!string.IsNullOrEmpty(tmp))
                ret += "对策:" + tmp;

            tmp = LocalizeNameList(pull_effect, pull_effect_scale);
            if (!string.IsNullOrEmpty(tmp))
                ret += "抽取时:" + tmp;
            return ret;
        }
    }
    public string LocalizedMeme
    {
        get => string.IsNullOrEmpty(meme) ? "" : "——" + meme;
    }

    public static List<string> SplitString(string str)
    {
        if (str == null)
        {
            return new List<string>();
        }
        return new List<string>(str.Split(';', '；')).FindAll(e => !string.IsNullOrEmpty(e)).ToList();
    }

    public static List<int> SplitIntString(string str, int intCnt)
    {
        List<int> retval = new List<int>();
        for (int i = 0; i < intCnt; i++)
            retval.Add(0);
        int cnt = 0;
        if (!string.IsNullOrEmpty(str))
        {
            str = str.Trim();
            string[] nums = str.Split(new char[] { ';', '；' });
            foreach (string num in nums)
            {
                if (cnt == intCnt)
                    break;
                int.TryParse(num, out int t);
                retval[cnt++] = t;
            }
        }
        return retval;
    }

    public static string LocalizeNameList(List<string> nameList, List<int> scales)
    {
        string ret = "";
        return "";
        for (int i = 0; i < nameList.Count; i++)
        {
            ret += EffectDesc.GetDesc(nameList[i], scales[i]);
        }
        if (ret != "")
        {
            ret += "\n";
        }
        return ret;
    }


    //public CardInfo(CardEntity entity)
    //{
    //    condtionDesc = null;
    //    ef
    //    //uint.TryParse(entity.id, out staticID);
    //    title = entity.name;
    //    desc = "";
    //    meme = entity.meme;
    //    category = 0;
    //    cost = 1;
    //    //EftAndCdtNameImage nameImage = EftAndCdtNameImage.GetInstance();
    //    hold_effect = SplitString(entity.hold_effect);
    //    condition = SplitString(entity.condition);
    //    effect = SplitString(entity.effect);
    //    //post_effect = SplitString(entity.post_effect);
    //    pull_effect = SplitString(entity.pull_effect);

    //    hold_effect_scale = SplitIntString(entity.hold_effect_scale, hold_effect.Count);
    //    condition_scale = SplitIntString(entity.condition_scale, condition.Count);
    //    effect_scale = SplitIntString(entity.effect_scale, effect.Count);
    //    //post_effect_scale = SplitIntString(entity.post_effect_scale, post_effect.Count);
    //    pull_effect_scale = SplitIntString(entity.pull_effect_scale, pull_effect.Count);
    //}
}