using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using CardEffects;

namespace CardEffects
{
    public class EffectHolder
    {
        /// <summary>
        /// 无效果
        /// </summary>
        public bool NIL(int a)
        {
            Debug.Log("NOTHING");
            return true;
        }

        /// <summary>
        /// 逻辑增加n
        /// </summary>
        public bool lgcPlus(int a)
        {
            CardGameManager.Instance.player.Ration = CardGameManager.Instance.player.Ration + a;
            return true;
        }

        /// <summary>
        /// 下一次说服判定增加n,可以跨回合保存
        /// </summary>    
        public bool persuadPlus(int a)
        {
            Signal signal = new Signal(true,1,"addCheck",a);
            CardGameManager.Instance.setSignal("PERSUADE",signal);
            return true;
        }

        /// <summary>
        /// 选择一个倾向并增加n
        /// </summary>     
        public bool chooseTendencyPlus(int a)
        {
            int index = 0; // choose();
            CardGameManager.Instance.player.data[index] += a;
            return true;
        }

        /// <summary>
        /// 道德增加n
        /// </summary>     
        public bool mrlPlus(int a)
        {
            CardGameManager.Instance.player.Moral = CardGameManager.Instance.player.Moral + a;
            return true;
        }

        /// <summary>
        /// 强势增加n
        /// </summary>
        public bool agsPlus(int a)
        {
            CardGameManager.Instance.player.Strong = CardGameManager.Instance.player.Strong + a;
            return true;
        }

        /// <summary>
        /// 逻辑增加n，持续三回合,从打出回合开始计
        /// </summary>
        public bool lgcPlusThreeRound(int a)
        {
            lgcPlus(a);
            Signal signal = new Signal(true,2,"lgcPlus",a);
            CardGameManager.Instance.setSignal("AUTO_PLAY",signal);
            return true;
        }

        /// <summary>
        /// 接下来的两次欺骗判定增加n，可以跨回合保存
        /// </summary>
        public bool cheatPlusTwoTimes(int a)
        {
            Signal signal = new Signal(true,2,"addCheck",a);
            CardGameManager.Instance.setSignal("FRAUD",signal);
            return true;
        }

        /// <summary>
        /// 本回合下一张对策被执行两次（词条名：双击）不可以跨回合保存
        /// </summary>
        public bool doubleNextCard(int a)
        {
            Signal signal = new Signal(false,1,"doubleExecute",a);
            CardGameManager.Instance.setSignal("CARD_PLAY",signal);
            return true;
        }

        /// <summary>
        /// 下一次欺骗选项增加n判定,可以跨回合保存
        /// </summary>
        public bool cheatPlus(int a)
        {
            Signal signal = new Signal(true,1,"addCheck",a);
            CardGameManager.Instance.setSignal("FRAUD",signal);
            return true;
        }

        /// <summary>
        /// 从牌库中选择n张牌加入手牌
        /// </summary>
        public bool chooseDraw(int a)
        {
            //getCardId();
            return true;
        }

        /// <summary>
        /// 耗费两点行动点,行动点不足时无法打出
        /// </summary>
        public bool twoEnergyCost(int a)
        {
            CardGameManager.Instance.player.energy -= 2;
            if (CardGameManager.Instance.player.energy > 0)
            {
                return true;
            }

            CardGameManager.Instance.player.energy = 0;
            return false;
        }

        /// <summary>
        /// 接下来的三回合抽牌数增加n
        /// </summary>
        public bool drawCardPlusThreeRound(int a)
        {            
            Signal signal = new Signal(true,3,"drawCard",a);
            CardGameManager.Instance.setSignal("AUTO_PLAY",signal);
            return true;
        }

        /// <summary>
        /// 直接结束本回合
        /// </summary>
        public bool roundEnd(int a)
        {
            CardGameManager.Instance.gameState = CardGameState.TURN_END;
            CardGameManager.Instance.nextGameState();
            return true;
        }

        /// <summary>
        /// 将本回合剩余的行动点增加到下一回合
        /// </summary>
        public bool holdEnergy(int a)
        {
            Signal signal = new Signal(true,1,"addEnergy",CardGameManager.Instance.player.energy);
            CardGameManager.Instance.setSignal("AUTO_PLAY",signal);
            return true;
        }

        /// <summary>
        /// 免疫下n次敌人话术Debuff
        /// </summary>
        public bool debuffInvalid(int a)
        {
            Signal signal = new Signal(true,a,"immune",0);
            CardGameManager.Instance.setSignal("ENEMY_DEBUFF",signal);
            return true;
        }

        /// <summary>
        /// 下一个选择判定加n
        /// </summary>
        public bool anySentencePlus(int a)
        {
            return true;
        }

        /// <summary>
        /// 本回合下一张对策不消耗行动点,不可以跨回合保存
        /// </summary>
        public bool energyFreeNextCard(int a)
        {
            Signal signal = new Signal(false,1,"zeroCost",0);
            CardGameManager.Instance.setSignal("CARD_PLAY",signal);
            return true;
        }

        /// <summary>
        /// 道德变为n
        /// </summary>
        public bool mrlChangeTo(int a)
        {
            CardGameManager.Instance.player.Moral = a;
            return true;
        }

        /// <summary>
        /// 铭记n张牌（这张牌每回合都回到手牌)
        /// </summary>
        public bool lockCard(int a)
        {
            return true;
        }

        /// <summary>
        /// 强势增加n，持续三回合,从打出回合开始计
        /// </summary>
        public bool agsPlusThreeRound(int a)
        {           
            
            Signal signal = new Signal(false,1,"zeroCost",0);
            CardGameManager.Instance.setSignal("CARD_PLAY",signal);
            return true;
        }

        /// <summary>
        /// 本回合上n张执行的对策牌是逻辑牌
        /// </summary>
        public bool lastCardIsLogic(int a)
        {
            return true;
        }

        /// <summary>
        /// 一次性：打出一次就被移出本次战斗
        /// </summary>
        public bool onceOnly(int a)
        {
            return false;
        }

        /// <summary>
        /// 锁定对话：接下来只能选择这个对话选项,需要配合美术表现
        /// </summary>
        public bool lockSentence(int a)
        {
            return true;
        }

        /// <summary>
        /// 移除一个对话选项中的n个判定条件,需要配合美术表现
        /// </summary>
        public bool removeJudgement(int a)
        {
            return true;
        }

        /// <summary>
        /// 对方进入不信任状态（对方固有判定值加成加倍）
        /// </summary>
        public bool lockPunishmentBelief(int a)
        {
            return true;
        }

        /// <summary>
        /// 逻辑大于n
        /// </summary>
        public bool lgcBiggerThan(int a)
        {
            return CardGameManager.Instance.player.Ration > a;
        }

        /// <summary>
        /// 执行过对策超过n次次数
        /// </summary>
        public bool measureFrequencyOver(int a)
        {
            return true;
        }

        /// <summary>
        /// 如果道德大于
        /// </summary>
        public bool mrlBiggerThan(int a)
        {
            return CardGameManager.Instance.player.Moral > a;
        }

        /// <summary>
        /// 执行过对策少于n次次数
        /// </summary>
        public bool measureFrequencyLess(int a)
        {
            return true;
        }

        /// <summary>
        /// 无忌大于n
        /// </summary>
        public bool inmBiggerThan(int a)
        {
            return CardGameManager.Instance.player.Unethic > a;
        }

        /// <summary>
        /// 外感大于n
        /// </summary>
        public bool outBiggerThan(int a)
        {
            return CardGameManager.Instance.player.Outside > a;
        }

        /// <summary>
        /// 迂回大于n
        /// </summary>
        public bool rdbBiggerThan(int a)
        {
            return CardGameManager.Instance.player.Detour > a;
        }

        /// <summary>
        /// 抽牌
        /// </summary>
        public bool drawCard(int a)
        {
            CardGameManager.Instance.draw((uint)a);
            return true;
        }
        /// <summary>
        /// 增加行动点
        /// </summary>
        public bool addEnergy(int a)
        {
            CardGameManager.Instance.player.energy += a;
            return true;
        }

    }
}

public class Effect
{
    public delegate bool Fun(int a);
    public Fun fun;
}
public class Signal
{
    public bool flag;
    public int lastTurns;
    public string effectKey;
    public int arg;
    public List<int> args;

    public Signal(bool flag, int lastTurns, string effectKey, int arg,List<int> args = null)
    {
        this.flag = flag;
        this.lastTurns = lastTurns;
        this.effectKey = effectKey;
        this.arg = arg;
        this.args = null;
    }
}
public class EffectManager
{
    private EffectHolder effectHolder = new EffectHolder();
    private static EffectManager _instance = null;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EffectManager();
            }

            return _instance;
        }
    }

    private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();

    private Dictionary<string, Effect> initDict()
    {
        Dictionary<string, Effect> res = new Dictionary<string, Effect>();
        var t = typeof(EffectHolder);
        foreach (var m in t.GetMethods())
        {
            Effect eff = new Effect();
            eff.fun = (Effect.Fun) Delegate.CreateDelegate(typeof(Effect.Fun), effectHolder,m);
            effects.Add(m.Name,eff);
        }
        return res;
    }
    public bool execute(string key, int scale = 0)
    {
        Effect eff = null;
        if (effects.TryGetValue(key,out eff))
        {
            return eff.fun(scale);
        }
        else
        {
            eff = new Effect();
            var t = typeof(EffectHolder);
            var m = t.GetMethod(key);
            if (m == null)
            {
                Debug.LogError(String.Format("对应的Effect{0}不存在，请检查配置！",key));
                return true;
            }
            eff.fun = (Effect.Fun) Delegate.CreateDelegate(typeof(Effect.Fun), effectHolder, t.GetMethod(key));
            effects.Add(key,eff);
            return eff.fun(scale);
        }
    }
    public bool execute(string key, Character chara)
    {
        return true;
    }
}