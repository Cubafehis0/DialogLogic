using System.Collections;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using SemanticTree;
using System;
using System.Text;
using MyExpressionParse;
using SemanticTree.PlayerEffect;
using SemanticTree.ChoiceEffect;
using SemanticTree.CardEffect;
using XmlParser;
using SemanticTree.Adapter;

public class SemanticAnalyser
{
    public static ICondition AnalyseConditionList(XmlNode xmlNode)
    {
        if (!xmlNode.HasChildNodes) return AnalyseCondition(xmlNode.InnerText);
        else throw new System.NotImplementedException();
    }

    public static ICondition AnalyseCondition(string s)
    {
        throw new System.NotImplementedException();
    }

    public static EffectList AnalayseEffectList(XmlNode xmlNode)
    {
        EffectList ret = new EffectList();
        foreach (XmlNode node in xmlNode.ChildNodes)
        {
            ret.Add(AnalyseEffect(node));
        }
        return ret;
    }

    private static Effect AnalyseEffect(XmlNode xmlNode)
    {
        return xmlNode.Name switch
        {
            "modify_health" => new ModifyHealthNode(xmlNode),
            "modify_personality" => new ModifyPersonalityNode(xmlNode),
            "modify_speech" => new ModifySpeech(xmlNode),
            "set_focus" => new ModifyFocusNode(xmlNode),
            "add_status" => new AddStatus(xmlNode),
            "draw" => new Draw(xmlNode),
            "set_draw_ban" => new SetDrawBan(xmlNode),
            "add_card_to_hand" => new AddCard2Hand(xmlNode),
            "hand" => new GetPileNode(xmlNode),
            "card_set" => new GetPileNode(xmlNode),
            "draw_pile" => new GetPileNode(xmlNode),
            "random_card" => new RandomCard(xmlNode),
            "foreach_card" => new ForEachInPile(xmlNode),
            "activate" => new ActivateCard(),
            "add_cost_modifier" => new AddCostModifier(xmlNode),
            "remove_cost_modifier" => new RemoveCostModifier(xmlNode),
            "discard_current" => new DiscardCard(),
            "discard_all" => new DiscardAllHand(xmlNode),
            "discard_some" => new DiscardSomeHandNode(xmlNode),
            "random_reveal" => new RandomRevealNode(xmlNode),
            "set_variable" => null,
            _ => throw new SemanticException("未识别的效果"),
        };
    }


}