using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AbilityModule : MonoBehaviour, ITurnStart, ITurnEnd
{
    private List<AbilityCounter> abilityCounters = new List<AbilityCounter>();
    private Dictionary<AbilityCounter, AbilityObject> activateObject = new Dictionary<AbilityCounter, AbilityObject>();
    public void AddAbility(AbilityBase ability, int cnt)
    {
        var counter = abilityCounters.Find(x => x.Ability == ability);
        if (counter != null)
        {
            counter.Count += cnt;
            activateObject[counter].UpdateVisuals();
            if (counter.NeedDestory())
            {
                abilityCounters.Remove(counter);
                counter.OnRemove();

                activateObject[counter].Destroy();
                activateObject.Remove(counter);
            }
        }
        else
        {
            AbilityCounter newCounter = new AbilityCounter(ability, cnt, "player");
            abilityCounters.Add(newCounter);
            newCounter.OnAdd();

            AbilityObject newObject = Instantiate(AbilityLibrary.Instance.AbilityPrefab, transform);
            newObject.transform.SetAsLastSibling();
            newObject.Set(newCounter);
            activateObject[newCounter] = newObject;
        }
    }

    public void AddAbility(string name, int cnt)
    {
        if (AbilityLibrary.Instance.abilityDictionary.TryGetValue(name, out AbilityBase ability))
        {
            AddAbility(ability, cnt);
        }
    }


    public void OnTurnStart()
    {
        foreach (var counter in abilityCounters)
        {
            counter.OnTurnStart();
        }
    }

    public void OnTurnEnd()
    {
        foreach (var counter in abilityCounters)
        {
            counter.OnTurnEnd();
        }
    }

}
