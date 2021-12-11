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
    /// ѡ��һ����������n
    /// δ���
    /// </summary>     
    bool chooseTendencyPlus(ICardPlayerState player, int a);
}

public interface IChanceEffects
{
    /// <summary>
    /// ��һ��˵���ж�����n,���Կ�غϱ���
    /// </summary>    
    bool IncreaseChanceOfConvincing(ICardPlayerState player, int a);

    /// <summary>
    /// ��һ����ƭѡ������n�ж�,���Կ�غϱ���
    /// </summary>
    bool IncreaseChanceOfCheating(ICardPlayerState player, int a);

    /// <summary>
    /// ��һ��ѡ���ж���n
    /// δ���
    /// </summary>
    bool IncreaseChanceOfSuccess(ICardPlayerState player, int a);

    /// <summary>
    /// �����Ի���������ֻ��ѡ���������ѡ��
    /// δ���
    /// </summary>
    bool LockOption(ICardPlayerState player, int a);

    /// <summary>
    /// ѡ��һ������ѡ�ѡ������n���ж�����ɾ��
    /// δ���
    /// </summary>
    bool removeJudgement(ICardPlayerState player, int a);

}

public interface IPlayerEffects
{
    bool GainEnergy(ICardPlayerState player, int a);

    /// <summary>
    /// �ķ������ж���,�ж��㲻��ʱ�޷����
    /// </summary>
    bool CostEnergy(ICardPlayerState player, int a);

    bool DrawCard(ICardPlayerState player, int a);

    bool EndTurn(ICardPlayerState player, int a);

    /// <summary>
    /// ���ƿ���ѡ��n���Ƽ�������
    /// δʵ��
    /// </summary>
    bool CopyFromCardSet(ICardPlayerState player, int a);
}

public interface IObsoleteEffects
{
    bool AddStrongThreeRound(ICardPlayerState player, int a);

    /// <summary>
    /// �߼�����n���������غ�,�Ӵ���غϿ�ʼ��
    /// </summary>
    bool AddLogicThreeRound(ICardPlayerState player, int a);

    bool drawCardPlusThreeRound(ICardPlayerState player, int a);



    /// <summary>
    /// ��������������ƭ�ж�����n�����Կ�غϱ���
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
    /// ��Ч��
    /// </summary>
    bool NIL(ICardPlayerState player, int a);
    
    bool debuffInvalid(ICardPlayerState player, int a);
    bool doubleNextCard(ICardPlayerState player, int a);
    bool energyFreeNextCard(ICardPlayerState player, int a);
    bool holdEnergy(ICardPlayerState player, int a);
    bool lockCard(ICardPlayerState player, int a);

    /// <summary>
    /// �Է����벻����״̬���Է������ж�ֵ�ӳɼӱ���
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

    //------------IEffectsLibrary��ʼ-------------------------------
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

    //------------IEffectsLibrary����-------------------------------
    //------------IPlayerEffects��ʼ--------------------------------
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

    //------------IPlayerEffects����--------------------------------
    //------------IChanceEffects��ʼ--------------------------------

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

    //------------IChanceEffects����--------------------------------
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
    /// ���غ���һ�ŶԲ߱�ִ�����Σ���������˫���������Կ�غϱ���
    /// </summary>
    public bool doubleNextCard(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(false, 1, "doubleExecute", a);
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    /// <summary>
    /// �����������غϳ���������n
    /// </summary>
    public bool drawCardPlusThreeRound(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 3, "drawCard", a);
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
    }



    /// <summary>
    /// �����غ�ʣ����ж������ӵ���һ�غ�
    /// </summary>
    public bool holdEnergy(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, 1, "addEnergy", CardGameManager.Instance.MainPlayerState.Energy);
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
    }

    /// <summary>
    /// ������n�ε��˻���Debuff
    /// </summary>
    public bool debuffInvalid(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(true, a, "immune", 0);
        SignalManager.Instance.SetSignal("ENEMY_DEBUFF", signal);
        return true;
    }

    /// <summary>
    /// ���غ���һ�ŶԲ߲������ж���,�����Կ�غϱ���
    /// </summary>
    public bool energyFreeNextCard(ICardPlayerState player, int a)
    {
        Signal signal = new Signal(false, 1, "zeroCost", 0);
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    /// <summary>
    /// ����n���ƣ�������ÿ�غ϶��ص�����)
    /// </summary>
    public bool lockCard(ICardPlayerState player, int a)
    {
        return true;
    }



    /// <summary>
    /// һ���ԣ����һ�ξͱ��Ƴ�����ս��
    /// </summary>
    public bool onceOnly(ICardPlayerState player, int a)
    {
        return false;
    }

    /// <summary>
    /// �Է����벻����״̬���Է������ж�ֵ�ӳɼӱ���
    /// </summary>
    public bool lockPunishmentBelief(ICardPlayerState player, int a)
    {
        return true;
    }




}
