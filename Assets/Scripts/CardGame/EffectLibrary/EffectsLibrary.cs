using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterEffects
{
    bool AddInside(ICardPlayerState player, int n);
    bool SetInside(ICardPlayerState player, int n);
    bool AddOutside(ICardPlayerState player, int n);
    bool SetOutside(ICardPlayerState player, int n);
    bool AddLogic(ICardPlayerState player, int n);
    bool SetLogic(ICardPlayerState player, int n);
    bool AddPassion(ICardPlayerState player, int n);
    bool SetPassion(ICardPlayerState player, int n);
    bool AddMoral(ICardPlayerState player, int n);
    bool SetMoral(ICardPlayerState player, int n);
    bool AddUnethic(ICardPlayerState player, int n);
    bool SetUnethic(ICardPlayerState player, int n);
    bool AddDetour(ICardPlayerState player, int n);
    bool SetDetour(ICardPlayerState player, int n);
    bool AddStrong(ICardPlayerState player, int n);
    bool SetStrong(ICardPlayerState player, int n);

    /// <summary>
    /// 选择一个倾向并增加n
    /// 未完成
    /// </summary>     
    bool chooseTendencyPlus(ICardPlayerState player, int a);
}

public interface IChanceEffects
{
    /// <summary>
    /// 下一次说服判定增加n,可以跨回合保存
    /// </summary>    
    bool IncreaseChanceOfConvincing(ICardPlayerState player, int a);

    /// <summary>
    /// 下一次欺骗选项增加n判定,可以跨回合保存
    /// </summary>
    bool IncreaseChanceOfCheating(ICardPlayerState player, int a);

    /// <summary>
    /// 下一个选择判定加n
    /// 未完成
    /// </summary>
    bool IncreaseChanceOfSuccess(ICardPlayerState player, int a);

    /// <summary>
    /// 锁定对话：接下来只能选择这个发言选项
    /// 未完成
    /// </summary>
    bool LockOption(ICardPlayerState player, int a);

    /// <summary>
    /// 选择一个发言选项，选择其中n个判定条件删除
    /// 未完成
    /// </summary>
    bool removeJudgement(ICardPlayerState player, int a);

}

public interface IPlayerEffects
{
    bool GainEnergy(ICardPlayerState player, int a);

    /// <summary>
    /// 耗费两点行动点,行动点不足时无法打出
    /// </summary>
    bool CostEnergy(ICardPlayerState player, int a);

    bool DrawCard(ICardPlayerState player, int a);

    bool EndTurn(ICardPlayerState player, int a);

    /// <summary>
    /// 从牌库中选择n张牌加入手牌
    /// 未实现
    /// </summary>
    bool CopyFromCardSet(ICardPlayerState player, int a);
}

public interface IObsoleteEffects
{
    bool AddStrongThreeRound(ICardPlayerState player, int a);

    /// <summary>
    /// 逻辑增加n，持续三回合,从打出回合开始计
    /// </summary>
    bool AddLogicThreeRound(ICardPlayerState player, int a);

    bool drawCardPlusThreeRound(ICardPlayerState player, int a);



    /// <summary>
    /// 接下来的两次欺骗判定增加n，可以跨回合保存
    /// </summary>
    bool cheatPlusTwoTimes(ICardPlayerState player, int a);

    bool onceOnly(ICardPlayerState player, int a);
}

public interface IAddAbilityEffects
{

}




public interface IEffectsLibrary:ICharacterEffects,IChanceEffects,IPlayerEffects,IObsoleteEffects
{
    /// <summary>
    /// 无效果
    /// </summary>
    bool NIL(ICardPlayerState player, int a);
    
    bool debuffInvalid(ICardPlayerState player, int a);
    bool doubleNextCard(ICardPlayerState player, int a);
    bool energyFreeNextCard(ICardPlayerState player, int a);
    bool holdEnergy(ICardPlayerState player, int a);
    bool lockCard(ICardPlayerState player, int a);

    /// <summary>
    /// 对方进入不信任状态（对方固有判定值加成加倍）
    /// </summary>
    bool lockPunishmentBelief(ICardPlayerState player, int a);

    
}

public class EffectsLibrary : MonoBehaviour, IEffectsLibrary
{
    private static EffectsLibrary instance = null;
    public static EffectsLibrary Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    public bool NIL(ICardPlayerState player, int a)
    {
        return true;
    }

    //------------IEffectsLibrary开始-------------------------------
    public bool AddInside(ICardPlayerState player, int n)
    {
        player.Character.Inside += n;
        return true;
    }

    public bool SetInside(ICardPlayerState player, int n)
    {
        player.Character.Inside = n;
        return true;
    }

    public bool AddOutside(ICardPlayerState player, int n)
    {
        player.Character.Outside += n;
        return true;
    }

    public bool SetOutside(ICardPlayerState player, int n)
    {
        player.Character.Outside = n;
        return true;
    }

    public bool AddLogic(ICardPlayerState player, int n)
    {
        player.Character.Logic += n;
        return true;
    }

    public bool SetLogic(ICardPlayerState player, int n)
    {
        player.Character.Logic = n;
        return true;
    }

    public bool AddPassion(ICardPlayerState player, int n)
    {
        player.Character.Passion += n;
        return true;
    }

    public bool SetPassion(ICardPlayerState player, int n)
    {
        player.Character.Moral = n;
        return true;
    }

    public bool AddMoral(ICardPlayerState player, int n)
    {
        player.Character.Moral += n;
        return true;
    }

    public bool SetMoral(ICardPlayerState player, int n)
    {
        player.Character.Moral = n;
        return true;
    }

    public bool AddUnethic(ICardPlayerState player, int n)
    {
        player.Character.Unethic += n;
        return true;
    }

    public bool SetUnethic(ICardPlayerState player, int n)
    {
        player.Character.Unethic = n;
        return true;
    }

    public bool AddDetour(ICardPlayerState player, int n)
    {
        player.Character.Detour += n;
        return true;
    }

    public bool SetDetour(ICardPlayerState player, int n)
    {
        player.Character.Detour = n;
        return true;
    }

    public bool AddStrong(ICardPlayerState player, int n)
    {
        player.Character.Logic += n;
        return true;
    }

    public bool SetStrong(ICardPlayerState player, int n)
    {
        player.Character.Strong = n;
        return true;
    }

    public bool chooseTendencyPlus(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    //------------IEffectsLibrary结束-------------------------------
    //------------IPlayerEffects开始--------------------------------
    public bool GainEnergy(ICardPlayerState player, int a)
    {
        player.Energy += a;
        return true;
    }

    public bool CostEnergy(ICardPlayerState player, int n)
    {
        if (CardGameManager.Instance.MainPlayerState.Energy >= n)
        {
            CardGameManager.Instance.MainPlayerState.Energy -= n;
            return true;
        }
        return false;
    }

    public bool DrawCard(ICardPlayerState player, int a)
    {
        player.Draw((uint)a);
        return true;
    }

    public bool EndTurn(ICardPlayerState player, int a)
    {
        CardGameManager.Instance.EndTurn();
        return true;
    }

    public bool CopyFromCardSet(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    //------------IPlayerEffects结束--------------------------------
    //------------IChanceEffects开始--------------------------------

    public bool IncreaseChanceOfConvincing(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 1, "addCheck", a);
        SignalManager.Instance.SetSignal("PERSUADE", signal);
        return true;
    }

    public bool IncreaseChanceOfCheating(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 1, "addCheck", a);
        SignalManager.Instance.SetSignal("FRAUD", signal);
        return true;
    }

    public bool IncreaseChanceOfSuccess(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool LockOption(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool removeJudgement(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool cheatPlusTwoTimes(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 2, "addCheck", a);
        SignalManager.Instance.SetSignal("FRAUD", signal);
        return true;
    }

    //------------IChanceEffects结束--------------------------------
    public bool AddStrongThreeRound(ICardPlayerState player, int a)
    {

        Signal signal = new Signal(false, 1, "zeroCost", 0);
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    public bool AddLogicThreeRound(ICardPlayerState player, int a)
    {
        AddLogic(player, a);
        Signal signal = new Signal(true, 2, "lgcPlus", a);
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
    }

    /// <summary>
    /// 本回合下一张对策被执行两次（词条名：双击）不可以跨回合保存
    /// </summary>
    public bool doubleNextCard(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(false, 1, "doubleExecute", a);
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    /// <summary>
    /// 接下来的三回合抽牌数增加n
    /// </summary>
    public bool drawCardPlusThreeRound(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 3, "drawCard", a);
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
    }



    /// <summary>
    /// 将本回合剩余的行动点增加到下一回合
    /// </summary>
    public bool holdEnergy(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 1, "addEnergy", CardGameManager.Instance.MainPlayerState.Energy);
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
    }

    /// <summary>
    /// 免疫下n次敌人话术Debuff
    /// </summary>
    public bool debuffInvalid(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, a, "immune", 0);
        SignalManager.Instance.SetSignal("ENEMY_DEBUFF", signal);
        return true;
    }

    /// <summary>
    /// 本回合下一张对策不消耗行动点,不可以跨回合保存
    /// </summary>
    public bool energyFreeNextCard(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(false, 1, "zeroCost", 0);
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    /// <summary>
    /// 铭记n张牌（这张牌每回合都回到手牌)
    /// </summary>
    public bool lockCard(ICardPlayerState player, int a)
    {
        return true;
    }



    /// <summary>
    /// 一次性：打出一次就被移出本次战斗
    /// </summary>
    public bool onceOnly(ICardPlayerState player, int a)
    {
        return false;
    }

    /// <summary>
    /// 对方进入不信任状态（对方固有判定值加成加倍）
    /// </summary>
    public bool lockPunishmentBelief(ICardPlayerState player, int a)
    {
        return true;
    }




}
