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
    /// �߼�����n
    /// </summary>
    [EffectNameImage("lgcBiggerThan")]
    public bool lgcBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Logic > a;
    }

    /// <summary>
    /// ִ�й��Բ߳���n�δ���
    /// </summary>
    [EffectNameImage("measureFrequencyOver")]
    public bool measureFrequencyOver(ICardPlayerState player, int a)
    {
        //return CardGameManager.Instance.statistics.cardUsed > a;
        return false;
    }

    /// <summary>
    /// ������´���
    /// </summary>
    [EffectNameImage("mrlBiggerThan")]
    public bool mrlBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Moral > a;
    }

    /// <summary>
    /// ִ�й��Բ�����n�δ���
    /// </summary>
    [EffectNameImage("measureFrequencyLess")]
    public bool measureFrequencyLess(ICardPlayerState player, int a)
    {
        //return CardGameManager.Instance.statistics.cardUsed < a;
        return false;
    }

    /// <summary>
    /// �޼ɴ���n
    /// </summary>
    [EffectNameImage("innBiggerThan")]
    public bool inmBiggerThan(ICardPlayerState player, int a)
    {

        //return CardGameManager.Instance.MainPlayerState.MainCharacter.Unethic > a;
        return false;
    }

    /// <summary>
    /// ��д���n
    /// </summary>
    [EffectNameImage("outBiggerThan")]
    public bool outBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Outside > a;
    }

    /// <summary>
    /// �ػش���n
    /// </summary>
    [EffectNameImage("rdbBiggerThan")]
    public bool rdbBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Detour > a;
    }

    /// <summary>
    /// ���غ���n��ִ�еĶԲ������߼���
    /// </summary>
    [EffectNameImage("lastCardIsLogic")]
    public bool lastCardIsLogic(ICardPlayerState player, int a)
    {
        return true;
    }
}
