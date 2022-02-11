using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class EffectNameImageAttribute : Attribute
{
    public string ImageName { get; set;}
    public EffectNameImageAttribute(string imageName)
    {
        ImageName = imageName;
    }
}

public class EftAndCdtNameImage
{
    private static EftAndCdtNameImage instance=null;
    public static EftAndCdtNameImage GetInstance()
    {
        if (instance == null)
            instance = new EftAndCdtNameImage();
        return instance;
    }
    private EftAndCdtNameImage()
    {
        GetNameImageDic(typeof(ConditionLibrary), out cdtNameImage);
        GetNameImageDic(typeof(EffectsLibrary), out eftNameImage);    
    }

    private void GetNameImageDic(Type type, out Dictionary<string, string> nameImage)
    {
        nameImage = new Dictionary<string, string>();
        var methodInfos = type.GetMethods();
        foreach(var m in methodInfos)
        {
            var attributes = m.GetCustomAttributes(typeof(EffectNameImageAttribute),false);
            if (attributes.Length == 0)
                continue;
            var image =attributes[0] as EffectNameImageAttribute;
            if(string.IsNullOrEmpty(image.ImageName))
            {
                Debug.Log(string.Format("名称为{0}效果或条件对应映像名为空",m.Name));
                continue;
            }
            if(nameImage.ContainsKey(image.ImageName))
            {
                Debug.Log(string.Format("名称为{0}和名称为{1}的效果或条件对应映像名重复", nameImage[image.ImageName], m.Name));
                continue;
            }
            nameImage.Add(image.ImageName, m.Name);
        }
    }

    private Dictionary<string, string> eftNameImage;
    private Dictionary<string, string> cdtNameImage;
    /// <summary>
    /// 获取效果或条件表中效果对应的方法名称
    /// </summary>
    /// <param name="rawName"></param>
    /// <returns></returns>
    public string GetEffectName(string rawName)
    {
        if (eftNameImage.TryGetValue(rawName, out string res))
            return res;
        else
        {
            Debug.LogError(string.Format("没有找到名称为{0}的效果", rawName));
            return null;
        }
    }
    public string GetConditionName(string rawName)
    {
        if (cdtNameImage.TryGetValue(rawName, out string res))
            return res;
        else
        {
            Debug.LogError(string.Format("没有找到名称为{0}的条件", rawName));
            return null;
        }
    }
}
