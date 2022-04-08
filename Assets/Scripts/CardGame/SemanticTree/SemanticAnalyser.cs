using System.Xml;
using SemanticTree;
using SemanticTree.Condition;

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
            //"modify_health" => new ModifyHealth(xmlNode),
            //"modify_personality" => new ModifyPersonality(xmlNode),
            //"modify_speech" => new ModifySpeech(xmlNode),
            //"set_focus" => new ModifyFocus(xmlNode),
            //"add_status" => new AddStatus(xmlNode),
            //"draw" => new Draw(xmlNode),
            //"set_draw_ban" => new SetDrawBan(xmlNode),
            //"add_card_to_hand" => new AddCard2Hand(xmlNode),
            //"hand" => new GetPileNode(xmlNode),
            //"card_set" => new GetPileNode(xmlNode),
            //"draw_pile" => new GetPileNode(xmlNode),
            //"random_card" => new RandomCard(xmlNode),
            //"foreach_card" => new ForEachInPile(xmlNode),
            //"activate" => new ActivateCard(),
            //"add_cost_modifier" => new AddCostModifier(xmlNode),
            //"remove_cost_modifier" => new RemoveCostModifier(xmlNode),
            //"discard_current" => new DiscardCard(),
            //"discard_all" => new DiscardAllHand(xmlNode),
            //"discard_some" => new DiscardSomeHand(xmlNode),
            //"random_reveal" => new RandomRevealNode(xmlNode),
            //"set_variable" => null,
            _ => throw new SemanticException("未识别的效果"),
        };
    }


}