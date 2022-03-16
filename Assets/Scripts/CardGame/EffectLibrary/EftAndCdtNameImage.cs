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
                Debug.Log(string.Format("����Ϊ{0}Ч����������Ӧӳ����Ϊ��",m.Name));
                continue;
            }
            if(nameImage.ContainsKey(image.ImageName))
            {
                Debug.Log(string.Format("����Ϊ{0}������Ϊ{1}��Ч����������Ӧӳ�����ظ�", nameImage[image.ImageName], m.Name));
                continue;
            }
            nameImage.Add(image.ImageName, m.Name);
        }
    }

    private Dictionary<string, string> eftNameImage;
    private Dictionary<string, string> cdtNameImage;
    /// <summary>
    /// ��ȡЧ������������Ч����Ӧ�ķ�������
    /// </summary>
    /// <param name="rawName"></param>
    /// <returns></returns>
    public string GetEffectName(string rawName)
    {
        if (string.IsNullOrEmpty(rawName)) return null;
        if (eftNameImage.TryGetValue(rawName, out string res))
            return res;
        else
        {
            Debug.LogError(string.Format("û���ҵ�����Ϊ{0}��Ч��", rawName));
            return null;
        }
    }
    public string GetImageName(string rawName)
    {
        string res = GetConditionName(rawName);
        return res == null ? GetEffectName(res) : res;
    }
    public string GetConditionName(string rawName)
    {
        if (string.IsNullOrEmpty(rawName)) return null;
        if (cdtNameImage.TryGetValue(rawName, out string res))
            return res;
        else
        {
            Debug.LogError(string.Format("û���ҵ�����Ϊ{0}������", rawName));
            return null;
        }
    }
}
