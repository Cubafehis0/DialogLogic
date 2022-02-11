using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IConditionLibrary
{
    bool inmBiggerThan(ICardPlayerState player, int a);

    bool lgcBiggerThan(ICardPlayerState player, int a);
    bool outBiggerThan(ICardPlayerState player, int a);
    bool mrlBiggerThan(ICardPlayerState player, int a);
    bool rdbBiggerThan(ICardPlayerState player, int a);
    bool measureFrequencyLess(ICardPlayerState player, int a);
    bool measureFrequencyOver(ICardPlayerState player, int a);
    bool lastCardIsLogic(ICardPlayerState player, int a);
}
public class ConditionLibrary : MonoBehaviour,IConditionLibrary
{
    /// <summary>
    /// 逻辑大于n
    /// </summary>
    [EffectNameImage("lgcBiggerThan")]
    public bool lgcBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Logic > a;
    }

    /// <summary>
    /// 执行过对策超过n次次数
    /// </summary>
    [EffectNameImage("measureFrequencyOver")]
    public bool measureFrequencyOver(ICardPlayerState player, int a)
    {
        //return CardGameManager.Instance.statistics.cardUsed > a;
        return false;
    }

    /// <summary>
    /// 如果道德大于
    /// </summary>
    [EffectNameImage("mrlBiggerThan")]
    public bool mrlBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Moral > a;
    }

    /// <summary>
    /// 执行过对策少于n次次数
    /// </summary>
    [EffectNameImage("measureFrequencyLess")]
    public bool measureFrequencyLess(ICardPlayerState player, int a)
    {
        //return CardGameManager.Instance.statistics.cardUsed < a;
        return false;
    }

    /// <summary>
    /// 无忌大于n
    /// </summary>
    [EffectNameImage("innBiggerThan")]
    public bool inmBiggerThan(ICardPlayerState player, int a)
    {

        //return CardGameManager.Instance.MainPlayerState.MainCharacter.Unethic > a;
        return false;
    }

    /// <summary>
    /// 外感大于n
    /// </summary>
    [EffectNameImage("outBiggerThan")]
    public bool outBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Outside > a;
    }

    /// <summary>
    /// 迂回大于n
    /// </summary>
    [EffectNameImage("rdbBiggerThan")]
    public bool rdbBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Detour > a;
    }

    /// <summary>
    /// 本回合上n张执行的对策牌是逻辑牌
    /// </summary>
    [EffectNameImage("lastCardIsLogic")]
    public bool lastCardIsLogic(ICardPlayerState player, int a)
    {
        return true;
    }
}
