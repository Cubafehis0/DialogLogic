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
            name = "����";
            description = "��<b>����</b>�ܵ����˺�����<b>50%</b>���� X �غϡ�";
            icon = "Icon_Vulnerable";
        }

        public override void OnAdd(string owner)
        {
            base.OnAdd(owner);
            Debug.Log($"{owner}��������ϵ��150%");
        }
        public override void OnRemove(string owner)
        {
            base.OnRemove(owner);
            Debug.Log($"{owner}��������ϵ��100%");
        }
    }

    [Ability("Weak")]
    class Weak : AbilityBase
    {
        Weak()
        {
            name = "����";
            description = "���� ��ɵ��˺����� 25% ������ʱ�� X �غϡ�";
            icon = "Icon_Weak";
        }

        public override void OnAdd(string owner)
        {
            base.OnAdd(owner);
            Debug.Log($"{owner}��������ϵ��75%");
        }

        public override void OnRemove(string owner)
        {
            base.OnRemove(owner);
            Debug.Log($"{owner}��������ϵ��100%");
        }

    }

}
