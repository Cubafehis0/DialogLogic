using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    private CardSlot slot;
    [SerializeField]
    private HealthModule healthModule;
    [SerializeField]
    private AbilityModule abilityModule;

    public T GetModule<T>() where T : MonoBehaviour
    {
        if (healthModule is T t1) return t1;
        if (abilityModule is T t2) return t2;
        return null;
    }
}
