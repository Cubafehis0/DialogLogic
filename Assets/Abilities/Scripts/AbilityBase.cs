using System.Collections;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class AbilityAttribute : Attribute
{
    readonly string positionalString;

    public AbilityAttribute(string positionalString)
    {
        this.positionalString = positionalString;
    }

    public string PositionalString
    {
        get { return positionalString; }
    }
}

public class AbilityBase
{
    public string name;
    public string description;
    public string icon;
    public virtual void OnUpdate(string owner,int cnt) { }
    public virtual void OnTurnStart(string owner) { }
    public virtual void OnTurnEnd(string owner) { }
    public virtual void OnAdd(string owner) { }
    public virtual void OnRemove(string owner) { }

    [Ability("Vulnerable")]
    class Vulnerable : AbilityBase
    {
        Vulnerable()
        {
            name = "易伤";
            description = "从<b>攻击</b>受到的伤害增加<b>50%</b>持续 X 回合。";
            icon = "Icon_Vulnerable";
        }

        public override void OnAdd(string owner)
        {
            base.OnAdd(owner);
            Debug.Log($"{owner}设置易伤系数150%");
        }
        public override void OnRemove(string owner)
        {
            base.OnRemove(owner);
            Debug.Log($"{owner}设置易伤系数100%");
        }
    }

    [Ability("Weak")]
    class Weak : AbilityBase
    {
        Weak()
        {
            name = "虚弱";
            description = "攻击 造成的伤害减少 25% 。持续时间 X 回合。";
            icon = "Icon_Weak";
        }

        public override void OnAdd(string owner)
        {
            base.OnAdd(owner);
            Debug.Log($"{owner}设置虚弱系数75%");
        }

        public override void OnRemove(string owner)
        {
            base.OnRemove(owner);
            Debug.Log($"{owner}设置虚弱系数100%");
        }

    }

}
