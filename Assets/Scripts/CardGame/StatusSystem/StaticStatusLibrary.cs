using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public static class StaticStatusLibrary
{
    private static Dictionary<string, Status> statusDictionary = new Dictionary<string, Status>();

    public static Status GetByName(string name)
    {
        return statusDictionary[name];
    }

    public static void DeclareStatus(string name)
    {
        if (statusDictionary.ContainsKey(name)) throw new SemanticException("不能重复定义状态");
        statusDictionary.Add(name, new Status());
    }
}