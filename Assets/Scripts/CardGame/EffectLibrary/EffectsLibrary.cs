using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsLibrary :MonoBehaviour
{
    private static EffectsLibrary instance = null;
    public static EffectsLibrary Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    [EffectNameImage("NIL")]
    public bool NIL(ICardPlayerState player, int a)
    {
        return true;
    }

    //------------IEffectsLibrary��ʼ-------------------------------

    [EffectNameImage("insidePlus")]
    public bool AddInside(ICardPlayerState player, int n)
    {
        player.Character.Inside += n;
        return true;
    }

    [EffectNameImage("insideSet")]
    public bool SetInside(ICardPlayerState player, int n)
    {
        player.Character.Inside = n;
        return true;
    }

    [EffectNameImage("outsidePlus")]
    public bool AddOutside(ICardPlayerState player, int n)
    {
        player.Character.Outside += n;
        return true;
    }

    [EffectNameImage("outsideSet")]
    public bool SetOutside(ICardPlayerState player, int n)
    {
        player.Character.Outside = n;
        return true;
    }

    [EffectNameImage("lgcPlus")]
    public bool AddLogic(ICardPlayerState player, int n)
    {
        player.Character.Logic += n;
        return true;
    }

    [EffectNameImage("lgcSet")]
    public bool SetLogic(ICardPlayerState player, int n)
    {
        player.Character.Logic = n;
        return true;
    }

    [EffectNameImage("sptPlus")]
    public bool AddPassion(ICardPlayerState player, int n)
    {
        player.Character.Passion += n;
        return true;
    }

    [EffectNameImage("sptSet")]
    public bool SetPassion(ICardPlayerState player, int n)
    {
        player.Character.Moral = n;
        return true;
    }

    [EffectNameImage("mrlPlus")]
    public bool AddMoral(ICardPlayerState player, int n)
    {
        player.Character.Moral += n;
        return true;
    }

    [EffectNameImage("mrlSet")]
    public bool SetMoral(ICardPlayerState player, int n)
    {
        player.Character.Moral = n;
        return true;
    }

    [EffectNameImage("AddUnethic")]
    public bool AddUnethic(ICardPlayerState player, int n)
    {
        player.Character.Unethic += n;
        return true;
    }

    [EffectNameImage("SetUnethic")]
    public bool SetUnethic(ICardPlayerState player, int n)
    {
        player.Character.Unethic = n;
        return true;
    }

    [EffectNameImage("AddDetour")]
    public bool AddDetour(ICardPlayerState player, int n)
    {
        player.Character.Detour += n;
        return true;
    }

    [EffectNameImage("SetDetour")]
    public bool SetDetour(ICardPlayerState player, int n)
    {
        player.Character.Detour = n;
        return true;
    }

    [EffectNameImage("agsPlus")]
    public bool AddStrong(ICardPlayerState player, int n)
    {
        player.Character.Logic += n;
        return true;
    }

    [EffectNameImage("agsSet")]
    public bool SetStrong(ICardPlayerState player, int n)
    {
        player.Character.Strong = n;
        return true;
    }

    [EffectNameImage("ChooseAddPrsn")]
    public bool ChooseAddPrsn(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("ChooseReversePrsn")]
    public bool ChooseReversePrsn(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("ChooseAddBasicPrsn")]
    public bool ChooseAddBasicPrsn(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("DoubleStrong")]
    public bool DoubleStrong(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("AddStrongThreeRound")]
    public bool AddStrongThreeRound(ICardPlayerState player, int a)
    {
        AddStrong(player, a);
        Signal signal = new Signal { effectKey = "AddStrong", decreaseOnTurnEnd = true, value = 3, arg = a };
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    [EffectNameImage("AddLogicThreeRound")]
    public bool AddLogicThreeRound(ICardPlayerState player, int a)
    {
        AddLogic(player, a);
        Signal signal = new Signal { effectKey = "AddLogic", decreaseOnTurnEnd = true, value = 3, arg = a };
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
    }
    //------------IEffectsLibrary����-------------------------------
    //------------IPlayerEffects��ʼ--------------------------------
    [EffectNameImage("GainEnergy")]
    public bool GainEnergy(ICardPlayerState player, int a)
    {
        player.Energy += a;
        return true;
    }

    [EffectNameImage("CostEnergy")]
    public bool CostEnergy(ICardPlayerState player, int n)
    {
        if (CardGameManager.Instance.MainPlayerState.Energy >= n)
        {
            CardGameManager.Instance.MainPlayerState.Energy -= n;
            return true;
        }
        return false;
    }

    [EffectNameImage("draw")]
    public bool DrawCard(ICardPlayerState player, int a)
    {
        player.Draw((uint)a);
        return true;
    }

    [EffectNameImage("drawCardPlusThreeRound")]
    public bool DrawMoreNextThreeTurn(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { effectKey = "Draw", decreaseOnUse = true, value = 3, arg = a };
        SignalManager.Instance.SetSignal("TURN_START", signal);
        return true;
    }

    [EffectNameImage("drawUntilLimit")]
    public bool Draw2Full(ICardPlayerState player, int a)
    {
        player.Draw2Full();
        return true;
    }

    [EffectNameImage("ChooseDiscard")]
    public bool ChooseDiscard(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }


    [EffectNameImage("EndTurn")]
    public bool EndTurn(ICardPlayerState player, int a)
    {
        CardGameManager.Instance.EndTurn();
        return true;
    }

    [EffectNameImage("CopyFromCardSet")]
    public bool CopyFromCardSet(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    //------------IPlayerEffects����--------------------------------
    //------------IChanceEffects��ʼ--------------------------------

    [EffectNameImage("AddConvincing")]
    public bool AddConvincing(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { decreaseOnUse = true, value = 1, effectKey = "addCheck", arg = a };
        SignalManager.Instance.SetSignal("PERSUADE", signal);
        return true;
    }

    [EffectNameImage("AddCheating")]
    public bool AddCheating(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { decreaseOnUse = true, value = 1, effectKey = "addCheck", arg = a };
        SignalManager.Instance.SetSignal("FRAUD", signal);
        return true;
    }

    [EffectNameImage("AddAllSpeak")]
    public bool AddAllSpeak(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("LockOption")]
    public bool LockOption(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("removeJudgement")]
    public bool removeJudgement(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    [EffectNameImage("cheatPlusTwoTimes")]
    public bool cheatPlusTwoTimes(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { decreaseOnUse = true, value = 2, effectKey = "addCheck", arg = a };
        SignalManager.Instance.SetSignal("FRAUD", signal);
        return true;
    }

    //------------IChanceEffects����--------------------------------


    ///// <summary>
    ///// ���غ���һ�ŶԲ߱�ִ�����Σ���������˫���������Կ�غϱ���
    ///// </summary>
    //public bool doubleNextCard(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(false, 1, "doubleExecute", a);
    //    SignalManager.Instance.SetSignal("CARD_PLAY", signal);
    //    return true;
    //}

    ///// <summary>
    ///// �����غ�ʣ����ж������ӵ���һ�غ�
    ///// </summary>
    //public bool holdEnergy(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(true, 1, "addEnergy", CardGameManager.Instance.MainPlayerState.Energy);
    //    SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
    //    return true;
    //}

    ///// <summary>
    ///// ������n�ε��˻���Debuff
    ///// </summary>
    //public bool debuffInvalid(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(true, a, "immune", 0);
    //    SignalManager.Instance.SetSignal("ENEMY_DEBUFF", signal);
    //    return true;
    //}

    ///// <summary>
    ///// ���غ���һ�ŶԲ߲������ж���,�����Կ�غϱ���
    ///// </summary>
    //public bool energyFreeNextCard(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(false, 1, "zeroCost", 0);
    //    SignalManager.Instance.SetSignal("CARD_PLAY", signal);
    //    return true;
    //}

    ///// <summary>
    ///// ����n���ƣ�������ÿ�غ϶��ص�����)
    ///// </summary>
    //public bool lockCard(ICardPlayerState player, int a)
    //{
    //    return true;
    //}

    /// <summary>
    /// �Է����벻����״̬���Է������ж�ֵ�ӳɼӱ���
    /// </summary>
    /// 
    [EffectNameImage("lockPunishmentBelief")]
    public bool lockPunishmentBelief(ICardPlayerState player, int a)
    {
        return true;
    }
}
