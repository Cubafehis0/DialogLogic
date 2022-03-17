using System;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree;
/// <summary>
/// 表示卡牌类型
/// </summary>
public enum CardType
{
    Lgc,
    Spt,
    Mrl,
    Imm,
    Rdb,
    Ags
}
public class Card : MonoBehaviour
{
    public CardInfo info;
    public IEffectNode effectNode = null;
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

    public string meme;


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
        effectNode = GetTestEffect(this.info.title);
    }

    List<string> GetEffects(string effects)
    {
        if (effects == null) return new List<string>();
        var rawEffectList = new List<string>(effects.Split(';', '；')).FindAll(e => !string.IsNullOrEmpty(e));
        List<string> res = new List<string>();
        foreach(var e in rawEffectList)
        {
            string image = EftAndCdtNameImage.GetInstance().GetImageName(e);
            if (image != null)
            {
                res.Add(image);
            }
        }
        return res;
    
    }
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