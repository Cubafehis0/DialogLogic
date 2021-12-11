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
    public bool lgcBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Logic > a;
    }

    /// <summary>
    /// ִ�й��Բ߳���n�δ���
    /// </summary>
    public bool measureFrequencyOver(ICardPlayerState player, int a)
    {
        //return CardGameManager.Instance.statistics.cardUsed > a;
        return false;
    }

    /// <summary>
    /// ������´���
    /// </summary>
    public bool mrlBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Moral > a;
    }

    /// <summary>
    /// ִ�й��Բ�����n�δ���
    /// </summary>
    public bool measureFrequencyLess(ICardPlayerState player, int a)
    {
        //return CardGameManager.Instance.statistics.cardUsed < a;
        return false;
    }

    /// <summary>
    /// �޼ɴ���n
    /// </summary>
    public bool inmBiggerThan(ICardPlayerState player, int a)
    {

        //return CardGameManager.Instance.MainPlayerState.MainCharacter.Unethic > a;
        return false;
    }

    /// <summary>
    /// ��д���n
    /// </summary>
    public bool outBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Outside > a;
    }

    /// <summary>
    /// �ػش���n
    /// </summary>
    public bool rdbBiggerThan(ICardPlayerState player, int a)
    {
        return CardGameManager.Instance.MainPlayerState.Character.Detour > a;
    }

    /// <summary>
    /// ���غ���n��ִ�еĶԲ������߼���
    /// </summary>
    public bool lastCardIsLogic(ICardPlayerState player, int a)
    {
        return true;
    }
}
