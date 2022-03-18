using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
using System.Xml;
using System.Text.RegularExpressions;

public class Card : MonoBehaviour
{
    //卡牌基本信息
    public string title;
    private string baseConfitionDesc;
    private string baseEffectDesc;
    public string meme;
    public int cost;
    public int category;

    private Dictionary<string, IExpressionNode> cardVars;
    
    //private Dictionary<string, object> cardConsts;

    public CardInfo info;

    public List<IConditionNode> conditionNode=null;
    public List<IEffectNode> pullEffectNode = null;
    public List<IEffectNode> holdEffectNode = null;
    public List<IEffectNode> effectNode = null;
    private bool temporaryActivate = false;
    private bool permanentActivate = false;

    public int FinalCost
    {
        get
        {
            int ret = info.cost;
            foreach (var modifer in CardPlayerState.Instance.costModifers)//有缺陷
            {
                CostModifier m = modifer.value;
                ret = m.condition.Value ? m.exp.Value : ret;
            }
            return ret;
        }
    }


    public bool Activated
    {
        get
        {
            return TemporaryActivate || PermanentActivate;
        }
    }
    public bool TemporaryActivate { get => temporaryActivate; set => temporaryActivate = value; }
    public bool PermanentActivate { get => permanentActivate; set => permanentActivate = value; }

    public void Construct(CardInfo? info)
    {
        if (info == null) return;
        this.info = info.Value;
        //effectNode = GetTestEffect(this.info.title);
    }
    public int GetCardVarValue(string varName)
    {
        if (cardVars.TryGetValue(varName, out IExpressionNode node)) return node.Value;
        else
        {
            Debug.LogError(string.Format("卡牌中不存在名称为{0}的变量", varName));
            return 0;
        }
    }

    public void AddCardVarValue(string varName, IExpressionNode node)
    {
        if (!cardVars.ContainsKey(varName))
        {
            cardVars.Add(varName, node);
        }
        else cardVars[varName] = node;
    }


    private readonly static string CardVarParttern=@"(#)([a-zA-Z0-9_]+)(#)";
    public string ConditionDesc{get=> GetDesc(baseConfitionDesc);}
    public string EffectDesc{get=> GetDesc(baseEffectDesc);}
    private string GetDesc(string desc)
    {
        if (desc == null) return "";
        var matches = Regex.Matches(desc, CardVarParttern);
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
    public void Construct(XmlElement cardDefine)
    {
        title = cardDefine["title"].InnerText;
        baseConfitionDesc = cardDefine["condition_desc"]?.InnerText;
        baseEffectDesc = cardDefine["effect_desc"].InnerText;
        meme = cardDefine["meme"].InnerText;
        XmlNode effectNode = cardDefine.GetElementsByTagName("effect")[0];
        if (effectNode["condition"] != null) conditionNode =XmlDocumentHelper.ParseCardConditions(effectNode["condition"]);
        if (effectNode["pull_effect"] != null) pullEffectNode = XmlDocumentHelper.ParseCardEffects(effectNode["pull_effect"]);
        if (effectNode["hold_effect"] != null) pullEffectNode = XmlDocumentHelper.ParseCardEffects(effectNode["hold_effect"]);
        if (effectNode["play_effect"] != null) pullEffectNode = XmlDocumentHelper.ParseCardEffects(effectNode["play_effect"]);
        XmlNodeList cardVars = cardDefine.GetElementsByTagName("define_card_var");
        if (cardVars != null) this.cardVars = XmlDocumentHelper.ParseCardVar(cardVars);
        
        //temporaryActivate = false;
        //permanentActivate = false;
        //cost=1;
        //category=1;
}


    public void Construct(Card card)
    {
        this.title = card.title;
        this.baseConfitionDesc = card.baseConfitionDesc;
        this.baseEffectDesc = card.baseEffectDesc;
        this.meme = card.meme;
        this.cost = card.cost;
        this.category = card.category;
        this.cardVars = card.cardVars;
        //this.cardConsts = card.cardConsts;
        this.conditionNode = card.conditionNode;
        this.pullEffectNode = card.pullEffectNode;
        this.holdEffectNode = card.holdEffectNode;
        this.effectNode = card.effectNode;
        this.temporaryActivate = card.temporaryActivate;
        this.permanentActivate = card.permanentActivate;
    }

    //List<string> GetEffects(string effects)
    //{
    //    if (effects == null) return new List<string>();
    //    var rawEffectList = new List<string>(effects.Split(';', '；')).FindAll(e => !string.IsNullOrEmpty(e));
    //    List<string> res = new List<string>();
    //    foreach(var e in rawEffectList)
    //    {
    //        string image = EftAndCdtNameImage.GetInstance().GetImageName(e);
    //        if (image != null)
    //        {
    //            res.Add(image);
    //        }
    //    }
    //    return res;
    
    //}
    //List<int> GetEffectsScale(string scales,int eftCnt)
    private static IEffectNode GetTestEffect(string title)
    {
        return title switch
        {
            "测试无效果" => null,
            "测试立刻加逻辑" => SemanticTreeClass.TestAddLogicEffectNode,
            "选项测试逻辑+2" => SemanticTreeClass.TestAddLogic2Node,
            "选项测试道德+2" => SemanticTreeClass.TestAddMoral2Node,
            "选项测试迂回+2" => SemanticTreeClass.TestAddDetour2Node,
            "测试选择+2" => SemanticTreeClass.TestSelect,
            "测试持续逻辑+1" => SemanticTreeClass.TestLgcPlusThreeRound,
            "测试抽牌1" => SemanticTreeClass.TestDraw,
            "测试向手牌增加1张“说教“卡牌" => SemanticTreeClass.TestAddPreach,
            "测试【丢弃所有】向手牌中增加弃牌数量的【说教】" => SemanticTreeClass.TestReconstruction,
            "测试随机【激活】1张手牌（无视条件且不消费行动点）" => SemanticTreeClass.TestRandomActive,
            "测试【激活】全部手牌" => SemanticTreeClass.TestAllHandActive,
            "测试【本回合】【下一次】【对策】不消耗【行动点】" => SemanticTreeClass.TestFreeCardNode,
            "测试【丢弃：1】" => SemanticTreeClass.TestDropCard,
            "测试从【卡组】中【选择：1】复制加入【手牌】" => SemanticTreeClass.TestChooseDraw,
            "测试从【手牌】中【选择：1】【激活】" => SemanticTreeClass.TestChooseActive,
            "测试从【抽牌堆】中【选择：1】【永久激活】" => SemanticTreeClass.TestChooseDeckActiveForever,
            "测试【本回合】，【说服判定】增加3" => SemanticTreeClass.TestPersuadPlus,
            "测试【本回合】不能再抽取手牌" => SemanticTreeClass.TestSetDrawBan,
            "测试每打出一张【说教】牌，增加一点外感" => SemanticTreeClass.TestOutPlusByPreachNode,
            "测试【持续】回合开始时，获得一张【说教】牌" => SemanticTreeClass.TestAddPreachEveryRound,
            "测试【持续】回合开始时，每持有一点外感便随机揭示x个判定" => SemanticTreeClass.TestRevealByOut,
            _ => null,
        };
    }

    public void AddModifier()
    {

    }

}