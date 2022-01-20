using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsLibrary : MonoBehaviour
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

    public bool ChooseAddPrsn(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool ChooseReversePrsn(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool ChooseAddBasicPrsn(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool DoubleStrong(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
    }

    public bool AddStrongThreeRound(ICardPlayerState player, int a)
    {
        AddStrong(player, a);
        Signal signal = new Signal { effectKey = "AddStrong", decreaseOnTurnEnd = true, value = 3, arg = a };
        SignalManager.Instance.SetSignal("CARD_PLAY", signal);
        return true;
    }

    public bool AddLogicThreeRound(ICardPlayerState player, int a)
    {
        AddLogic(player, a);
        Signal signal = new Signal { effectKey = "AddLogic", decreaseOnTurnEnd = true, value = 3, arg = a };
        SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
        return true;
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

    public bool DrawMoreNextThreeTurn(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { effectKey = "Draw", decreaseOnUse = true, value = 3, arg = a };
        SignalManager.Instance.SetSignal("TURN_START", signal);
        return true;
    }

    public bool Draw2Full(ICardPlayerState player, int a)
    {
        player.Draw2Full();
        return true;
    }

    public bool ChooseDiscard(ICardPlayerState player, int a)
    {
        throw new System.NotImplementedException();
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

    public bool AddConvincing(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { decreaseOnUse = true, value = 1, effectKey = "addCheck", arg = a };
        SignalManager.Instance.SetSignal("PERSUADE", signal);
        return true;
    }

    public bool AddCheating(ICardPlayerState player, int a)
    {
        Signal signal = new Signal { decreaseOnUse = true, value = 1, effectKey = "addCheck", arg = a };
        SignalManager.Instance.SetSignal("FRAUD", signal);
        return true;
    }

    public bool AddAllSpeak(ICardPlayerState player, int a)
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
        Signal signal = new Signal { decreaseOnUse = true, value = 2, effectKey = "addCheck", arg = a };
        SignalManager.Instance.SetSignal("FRAUD", signal);
        return true;
    }

    //------------IChanceEffects结束--------------------------------


    ///// <summary>
    ///// 本回合下一张对策被执行两次（词条名：双击）不可以跨回合保存
    ///// </summary>
    //public bool doubleNextCard(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(false, 1, "doubleExecute", a);
    //    SignalManager.Instance.SetSignal("CARD_PLAY", signal);
    //    return true;
    //}

    ///// <summary>
    ///// 将本回合剩余的行动点增加到下一回合
    ///// </summary>
    //public bool holdEnergy(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(true, 1, "addEnergy", CardGameManager.Instance.MainPlayerState.Energy);
    //    SignalManager.Instance.SetSignal("AUTO_PLAY", signal);
    //    return true;
    //}

    ///// <summary>
    ///// 免疫下n次敌人话术Debuff
    ///// </summary>
    //public bool debuffInvalid(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(true, a, "immune", 0);
    //    SignalManager.Instance.SetSignal("ENEMY_DEBUFF", signal);
    //    return true;
    //}

    ///// <summary>
    ///// 本回合下一张对策不消耗行动点,不可以跨回合保存
    ///// </summary>
    //public bool energyFreeNextCard(ICardPlayerState player, int a)
    //{
    //    Signal signal = new Signal(false, 1, "zeroCost", 0);
    //    SignalManager.Instance.SetSignal("CARD_PLAY", signal);
    //    return true;
    //}

    ///// <summary>
    ///// 铭记n张牌（这张牌每回合都回到手牌)
    ///// </summary>
    //public bool lockCard(ICardPlayerState player, int a)
    //{
    //    return true;
    //}

    /// <summary>
    /// 对方进入不信任状态（对方固有判定值加成加倍）
    /// </summary>
    public bool lockPunishmentBelief(ICardPlayerState player, int a)
    {
        return true;
    }
}
