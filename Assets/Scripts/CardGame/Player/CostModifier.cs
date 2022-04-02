using ExpressionAnalyser;
using SemanticTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;


public static class StaticCostModifierLibrary
{
    private static Dictionary<string, CostModifier> costModifierDictionary = new Dictionary<string, CostModifier>();
    public static CostModifier GetByName(string name)
    {
        return costModifierDictionary[name];
    }

    public static void Declare(CostModifier costModifier)
    {
        if (costModifierDictionary.ContainsKey(costModifier.Name)) throw new SemanticException("不能重复定义modifier");
        costModifierDictionary.Add(costModifier.Name, costModifier);
    }
}


public class CostModifier
{
    [XmlElement(ElementName = "name")]
    public string Name;

    [XmlElement(ElementName = "condition")]
    public ConditionNode Condition;

    [XmlElement(ElementName = "factor")]
    public string NumExpression;
    [XmlIgnore]
    public IExpression exp;

    public CostModifier() { }
    public CostModifier(CostModifier origin)
    {
        Name = origin.Name;
        Condition = origin.Condition;
        NumExpression = origin.NumExpression;
        exp = origin.exp;
    }

    public void Construct()
    {
        Condition?.Construct();
        exp = ExpressionParser.AnalayseExpression(NumExpression);
    }
}