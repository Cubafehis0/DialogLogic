using SemanticTree;
using SemanticTree.Expression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CostModifierInfo
{
    public string Name { get; set; }
    public ICondition ConditionInfo { get; set; }
    public string NumExpression { get; set; }
    public CostModifierInfo(ICondition conditionInfo, string numExpression)
    {
        ConditionInfo = conditionInfo;
        NumExpression = numExpression ?? throw new ArgumentNullException(nameof(numExpression));
    }

    public CostModifierInfo()
    {
        ConditionInfo = null;
        NumExpression = "";
    }
}

public static class StaticCostModifierLibrary
{
    private static Dictionary<string, CostModifier> costModifierDictionary = new Dictionary<string, CostModifier>();
    public static CostModifier GetByName(string name)
    {
        return costModifierDictionary[name];
    }
    public static void Declare(XmlNode xmlNode)
    {
        string name = xmlNode["name"].InnerText;
        Declare(name);
    }

    public static void Declare(string name)
    {
        if (costModifierDictionary.ContainsKey(name)) throw new SemanticException("不能重复定义modifier");
        costModifierDictionary.Add(name, new CostModifier());
    }

    public static void Define(XmlNode xmlNode)
    {
        string name = xmlNode["name"].InnerText;
        if (!costModifierDictionary.ContainsKey(name)) Declare(xmlNode);
        costModifierDictionary[name].Construct(xmlNode);
    }
}


public class CostModifier
{
    private CostModifierInfo info;
    public ICondition condition;
    public IExpression exp;

    public void Construct(XmlNode xmlNode)
    {
        info= new CostModifierInfo();
        XmlNode it = xmlNode.FirstChild;
        while (it != null)
        {
            switch (it.Name)
            {
                case "name":
                    info.Name = xmlNode["name"].InnerText;
                    break;
                case "condition":
                    condition = SemanticAnalyser.AnalyseConditionList(it);
                    break;
                case "set_value":
                    exp = MyExpressionParse.ExpressionParser.AnalayseExpression(it.InnerText);
                    break;
                default:
                    throw new SemanticException("未识别的符号");
            }
            it = it.NextSibling;
        }

    }
}