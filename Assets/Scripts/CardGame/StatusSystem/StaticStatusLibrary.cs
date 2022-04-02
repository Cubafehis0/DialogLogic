using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public static class StaticStatusLibrary
{
    private static Dictionary<string, Status> statusDictionary = new Dictionary<string, Status>();

    private static int anonymousCnt;
    public static Status GetByName(string name)
    {
        if (!statusDictionary.ContainsKey(name))
        {
            Debug.LogError(string.Format("未找到{0}状态",name));
        }
            
        return statusDictionary[name];
    }

    public static void DeclareStatus(string name, Status status)
    {
        if (statusDictionary.ContainsKey(name)) throw new SemanticException("不能重复定义状态"+name);
        statusDictionary.Add(name, status);
    }

    public static string DeclareStatus()
    {
        string name = "anonymous" + anonymousCnt++.ToString();
        statusDictionary.Add(name, new Status());
        return name;
    }

}