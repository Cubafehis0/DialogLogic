using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicStaticLib : IEnumerable
{
    public List<Relic> relics = new List<Relic>();
    public List<Relic>[] normalRelics = new List<Relic>[6];
    public List<Relic>[] rareRelics = new List<Relic>[6];

    public RelicStaticLib()
    {
        for (int i = 0; i < 6; i++)
        {
            normalRelics[i] = new List<Relic>();
            rareRelics[i] = new List<Relic>();
        }
    }

    public RelicStaticLib(RelicLibInfo relicLibInfo) : this()
    {
        relics = relicLibInfo.relics.ConvertAll(x => new Relic(x));
        foreach (Relic relic in relics)
        {
            RegisterRelic(relic);
        }
    }

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
        foreach (List<Relic> t in normalRelics)
        {
            foreach (Relic r in t)
            {
                yield return r;
            }
        }
        foreach (List<Relic> t in rareRelics)
        {
            foreach (Relic r in t)
            {
                yield return r;
            }
        }
    }

    //grade代表任务完成等级
    public List<Relic> RandomChooseRelics(int grade, Personality personality, List<Relic> exclusive)
    {
        foreach (var relic in exclusive)
        {
            int index = PersonalityTypeToIndex(relic.relicInfo.Category);
            if (relic.relicInfo.Rarity == 0)
            {
                if (!normalRelics[index].Contains(relic))
                {
                    Debug.LogError("卡牌池没有该碎片");
                    return null;
                }
                else
                {
                    normalRelics[index].Remove(relic);
                }
            }
            else if (relic.relicInfo.Rarity == 1)
            {
                if (!rareRelics[index].Contains(relic))
                {
                    Debug.LogError("卡牌池没有该碎片");
                    return null;
                }
                else
                {
                    rareRelics[index].Remove(relic);
                }
            }
        }


        int normalWeight = grade switch
        {
            0 => 20,
            1 => 70,
            2 => 95,
            _ => 100
        };

        int totalWeight = Mathf.Abs(personality[PersonalityType.Moral]) +
            Mathf.Abs(personality[PersonalityType.Logic]) +
            Mathf.Abs(personality[PersonalityType.Detour]);
        List<PersonalityType> aimingTypes = new List<PersonalityType>
        {
            personality[PersonalityType.Moral] > 0 ? PersonalityType.Moral : PersonalityType.Unethic,
            personality[PersonalityType.Logic] > 0 ? PersonalityType.Logic : PersonalityType.Passion,
            personality[PersonalityType.Detour] > 0 ? PersonalityType.Detour : PersonalityType.Strong,
        };

        List<Relic> outRelics = new List<Relic>();

        int usableCount = 0;
        foreach (var aimingType in aimingTypes)
        {
            if (personality[aimingType] == 0) continue;
            usableCount += normalRelics[PersonalityTypeToIndex(aimingType)].Count;
            usableCount += rareRelics[PersonalityTypeToIndex(aimingType)].Count;
        }
        if (usableCount <= 3)
        {
            foreach (var aimingType in aimingTypes)
            {
                if (personality[aimingType] == 0) continue;
                outRelics.AddRange(normalRelics[PersonalityTypeToIndex(aimingType)]);
                outRelics.AddRange(rareRelics[PersonalityTypeToIndex(aimingType)]);
            }
        }
        else
        {
            while (outRelics.Count < 3)
            {
                Relic relic = null;
                int random = Random.Range(0, totalWeight);
                int counter = 0;
                foreach (var aimingType in aimingTypes)
                {
                    counter += personality[aimingType];
                    if (random < counter)
                    {
                        int index = PersonalityTypeToIndex(aimingType);
                        if (Random.Range(0, 100) < normalWeight)
                        {
                            if (normalRelics[index].Count > 0)
                            {
                                relic = normalRelics[index][Random.Range(0, normalRelics[index].Count)];
                            }
                        }
                        else if (rareRelics[index].Count > 0)
                        {
                            relic = rareRelics[index][Random.Range(0, rareRelics[index].Count)];
                        }
                        break;
                    }
                }
                if (relic != null && !outRelics.Contains(relic))
                {
                    outRelics.Add(relic);
                }
            }
            Debug.Log("Names:");
            foreach (var item in outRelics)
            {
                Debug.Log(item.relicInfo.Name);
            }
        }

        foreach (var relic in exclusive)
        {
            int index = PersonalityTypeToIndex(relic.relicInfo.Category);
            if (relic.relicInfo.Rarity == 0)
            {
                normalRelics[index].Add(relic);

            }
            else if (relic.relicInfo.Rarity == 1)
            {
                rareRelics[index].Add(relic);

            }
        }
        return outRelics;

    }

    private void RegisterRelic(Relic relic)
    {
        int index = PersonalityTypeToIndex(relic.relicInfo.Category);
        if (index == -1)
        {
            Debug.LogError("碎片倾向有误");
        }
        else if (relic.relicInfo.Rarity == 0)
        {
            normalRelics[index].Add(relic);
        }
        else if (relic.relicInfo.Rarity == 1)
        {
            rareRelics[index].Add(relic);
        }
    }
}
