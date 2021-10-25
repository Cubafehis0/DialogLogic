using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 不需要指定目标的卡牌瞄准器
/// </summary>
public class NoTargetAimer : BaseAimer
{
    public static NoTargetAimer instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }


    private void OnDisable()
    {
        aimer = null;
    }

    protected override bool CheckValidTarget(GameObject target)
    {
        return true;
    }

    protected override void UpdateVisual()
    {
        if (aimer)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimer.transform.position = pos;
        }
    }
}
