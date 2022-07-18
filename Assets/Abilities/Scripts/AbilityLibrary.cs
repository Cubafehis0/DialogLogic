using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class AbilityLibrary : Singleton<AbilityLibrary>
{
    public AbilityObject AbilityPrefab;
    [HideInInspector]
    public Dictionary<string, AbilityBase> abilityDictionary = new Dictionary<string, AbilityBase>();
    public StringSpriteDictionary spriteDictionary = new StringSpriteDictionary();
    private void Start()
    {
        var subTypeList = new List<Type>();
        var assembly = typeof(AbilityBase).Assembly;//获取当前父类所在的程序集``
        var assemblyAllTypes = assembly.GetTypes();//获取该程序集中的所有类型
        foreach (var itemType in assemblyAllTypes)//遍历所有类型进行查找
        {
            var baseType = itemType.BaseType;//获取元素类型的基类
            if (baseType != null)//如果有基类
            {
                if (baseType.Name == typeof(AbilityBase).Name)//如果基类就是给定的父类
                {
                    subTypeList.Add(itemType);//加入子类表中
                }
            }
        }
        foreach (Type type in subTypeList)
        {
            foreach (object attributes in type.GetCustomAttributes(false))
            {
                AbilityAttribute attr = (AbilityAttribute)attributes;
                if (null != attr)
                {
                    Type[] types = new Type[0];
                    ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, CallingConventions.HasThis, types, null);
                    abilityDictionary[attr.PositionalString] = (AbilityBase)constructor.Invoke(null);
                }
            }
        }
    }

    public AbilityBase GetAbility(string name)
    {
        if (abilityDictionary.TryGetValue(name, out var ability))
            return ability;
        return null;
    }
}
