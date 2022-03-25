using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ExpressionAnalyser;

public class Card : MonoBehaviour
{
    //卡牌基本信息
    public CardInfo info;

    [SerializeField]
    private bool temporaryActivate = false;
    [SerializeField]
    private bool permanentActivate = false;

    private Dictionary<string, IExpression> cardVars;

    public EffectList Effects;
    public ICondition conditionNode = null;

    public int FinalCost
    {
        get
        {
            if (Activated) return 0;
            int ret = info.BaseCost;
            foreach (var modifer in CardPlayerState.Instance.costModifers)//有缺陷
            {
                CostModifier m = modifer.value;
                if(m.condition?.Value ?? true)
                {
                    ret = m.exp.Value;
                }
            }
            return ret;
        }
    }
    public bool Activated { get => TemporaryActivate || PermanentActivate; }
    public bool TemporaryActivate { get => temporaryActivate; set => temporaryActivate = value; }
    public bool PermanentActivate { get => permanentActivate; set => permanentActivate = value; }

    private readonly static string CardVarParttern = @"(#)([a-zA-Z0-9_]+)(#)";

    public void Construct(Card prefab)
    {
        //浅拷贝
        info = prefab.info;
        conditionNode = prefab.conditionNode;
    }
    public void Construct(XmlNode xml)
    {
        //if (!xml.Name.Equals("define_card")) throw new SemanticException();
        //temporaryActivate = false;
        //permanentActivate = false;
        //cost = 1;
        //Category = 1;
        //XmlNode it = xml.FirstChild;
        //while (it != null)
        //{
        //    switch (it.Name)
        //    {
        //        case "title":
        //            Title = it.InnerText;
        //            break;
        //        case "effect_desc":
        //            baseEffectDesc = it.InnerText;
        //            break;
        //        case "condition_desc":
        //            baseConditionDesc = it.InnerText;
        //            break;
        //        case "meme":
        //            Meme = it.InnerText;
        //            break;
        //        case "condition":
        //            if (conditionNode != null) throw new SemanticException();
        //            conditionNode = SemanticAnalyser.AnalyseConditionList(it);
        //            break;
        //        case "pull_effect":
        //            if (pullEffectNode != null) throw new SemanticException();
        //            pullEffectNode = SemanticAnalyser.AnalayseEffectList(it);
        //            break;
        //        case "hold_effect":
        //            if (holdEffectNode != null) throw new SemanticException();
        //            holdEffectNode = SemanticAnalyser.AnalayseEffectList(it);
        //            break;
        //        case "play_effect":
        //            if (effectNode != null) throw new SemanticException();
        //            effectNode = SemanticAnalyser.AnalayseEffectList(it);
        //            break;
        //        case "define_card_var":
        //            throw new NotImplementedException();
        //        default:
        //            throw new SemanticException();
        //    }
        //    it = it.NextSibling;
        //}
    }

    public int GetCardVarValue(string varName)
    {
        if (cardVars.TryGetValue(varName, out IExpression node)) return node.Value;
        else
        {
            throw new SemanticException("卡牌中不存在名称为{0}的变量");
        }
    }

    public void DefineCardVar(string varName, IExpression node)
    {
        if (!cardVars.ContainsKey(varName))
        {
            cardVars.Add(varName, node);
        }
        else
        {
            throw new SemanticException();
        }
    }



    private string GetDesc(string desc)
    {
        if (desc == null) return "";
        MatchCollection matches = Regex.Matches(desc, CardVarParttern);
        if (matches.Count > 0)
        {
            foreach (Match m in matches)
            {
                string varName = m.Groups[2].Value;
                string varHolder = m.Groups[0].Value;
                //还有全局变量表的查询和替换，待补充
                desc = desc.Replace(varHolder, GetCardVarValue(varName).ToString());
            }
        }
        return desc;
    }
}