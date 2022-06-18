using ExpressionAnalyser;
using ModdingAPI;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


public static class StaticCostModifierLibrary
{
    private static Dictionary<string, CostModifier> costModifierDictionary = new Dictionary<string, CostModifier>();
    public static CostModifier GetByName(string name)
    {
        throw new System.NotImplementedException();
        //return costModifierDictionary[name];
    }

    public static void Declare(CostModifier costModifier)
    {
        throw new System.NotImplementedException();
        //if (!costModifierDictionary.ContainsKey(costModifier.Name))
        //{
        //    costModifierDictionary.Add(costModifier.Name, costModifier);
        //}

    }
}