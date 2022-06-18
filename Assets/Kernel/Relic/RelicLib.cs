
using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;

public class RelicLib
{
    public List<Relic> ownedRelics = new List<Relic>();
    public Dictionary<Relic, Relic> conflictTable = new Dictionary<Relic, Relic>();

    public int PersonalityTypeToIndex(PersonalityType personalityType)
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

    public void AddRelic(Relic relic)
    {
        ownedRelics.Add(relic);
        conflictTable.Add(relic, null);
        if (MatchOppositeRelic(relic)) RelicGameManager.Instance.Sanity -= 2;
    }

    public void Reset()
    {
        RelicGameManager.Instance.Sanity = 0;
        ownedRelics = new List<Relic>();
    }

    public void RemoveRelic(Relic relic)
    {
        if (!ownedRelics.Contains(relic)) Debug.LogError("Œ¥≥÷”–∏√ÀÈ∆¨");
        else
        {
            Relic opRelic = conflictTable[relic];
            if (opRelic != null)
            {
                conflictTable[opRelic] = null;
                RelicGameManager.Instance.Sanity += 2;
            }
            ownedRelics.Remove(relic);
            conflictTable.Remove(relic);
            if (MatchOppositeRelic(opRelic))
            {
                RelicGameManager.Instance.Sanity -= 2;
            }
        }
    }


    private bool MatchOppositeRelic(Relic relic)
    {
        foreach (Relic ownedRelic in ownedRelics)
        {
            if (ownedRelic == relic) continue;
            if (conflictTable[ownedRelic] == null && ownedRelic.relicInfo.Category == Relic.GetOppositeCategory(relic.relicInfo.Category))
            {
                conflictTable[ownedRelic] = relic;
                conflictTable[relic] = ownedRelic;
                return true;
            }
        }
        return false;
    }
}
