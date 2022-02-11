using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectDesc
{
    static Dictionary<string, string> eftName2Desc;

    public static void InitalDic(EffectTable effectTable)
    {
        eftName2Desc = new Dictionary<string, string>();
        AddItem(effectTable.effect);
        AddItem(effectTable.condition);
    }
    public static string GetDesc(string name,int scale)
    {
        string ret = "";
        if(eftName2Desc.TryGetValue(name,out string desc))
        {
            ret += desc.Replace("n", scale.ToString());
        }
        return ret;
    }
    private static void AddItem(List<EffectEntity> effectList)
    {
        foreach (var i in effectList)
        {
            if (eftName2Desc.ContainsKey(i.name))
            {
                Debug.Log(string.Format("名称为{0}的条件或效果重复", i.name));
                continue;
            }
            eftName2Desc.Add(i.name, i.description);
        }
    }
}
