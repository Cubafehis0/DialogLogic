using ModdingAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

public class StaticRelicLibrary : Singleton<StaticRelicLibrary>, IEnumerable
{

    public Dictionary<string, Relic> relics = new Dictionary<string, Relic>();
    public static int PersonalityTypeToIndex(PersonalityType personalityType)
    {
        return personalityType switch
        {
            PersonalityType.Moral => 0,
            PersonalityType.Unethic => 1,
            PersonalityType.Logic => 2,
            PersonalityType.Passion => 3,
            PersonalityType.Detour => 4,
            PersonalityType.Strong => 5,
            _ => -1
        };
    }

    public IEnumerator GetEnumerator()
    {
        yield return relics.GetEnumerator();
    }

    //grade代表任务完成等级
    public List<Relic> RandomChooseRelics(int grade, Personality personality, IEnumerable<Relic> inclusive)
    {
        List<Relic> relics = new List<Relic>(inclusive);


        int normalWeight = grade switch
        {
            0 => 20,
            1 => 70,
            2 => 95,
            _ => 100
        };


        List<Relic> availableRelics = new List<Relic>(relics.Where(x => personality[x.Category] > 0));
        RandomList<Relic> randomList = new RandomList<Relic>(availableRelics.ConvertAll(x => new Tuple<int, Relic>(personality[x.Category], x)));

        List<Relic> outRelics = new List<Relic>();
        for (int i = 0; i < 3; i++)
        {
            var relic = randomList.GetRandom();
            outRelics.Add(relic.Item2);
            randomList.Remove(relic);
        }
        return outRelics;

    }

    public Relic GetRelicByName(string name)
    {
        return (Relic)relics[name].Clone();
    }

    public void RegisterRelic(Relic relic)
    {
        int index = PersonalityTypeToIndex(relic.Category);
        if (index == -1)
        {
            Debug.LogError("碎片倾向有误");
        }
        relics.Add(relic.Name,relic);
    }

    public override void Awake()
    {
        base.Awake();
        var subTypeList = new List<Type>();
        var assembly = typeof(Relic).Assembly;//获取当前父类所在的程序集``
        var assemblyAllTypes = assembly.GetTypes();//获取该程序集中的所有类型
        foreach (var itemType in assemblyAllTypes)//遍历所有类型进行查找
        {
            var baseType = itemType.BaseType;//获取元素类型的基类
            if (baseType != null)//如果有基类
            {
                if (baseType == typeof(Relic))//如果基类就是给定的父类
                {
                    subTypeList.Add(itemType);//加入子类表中
                }
            }
        };
        foreach (Type type in subTypeList)
        {
            Type[] types = new Type[0];
            ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.HasThis, types, null);
            if (constructor == null)
            {
                Debug.LogError($"{type.Name}没有private无参构造方法");
            }
            else
            {
                var newRelic = (Relic)constructor.Invoke(null);
                RegisterRelic(newRelic);
            }
        }
    }
}
