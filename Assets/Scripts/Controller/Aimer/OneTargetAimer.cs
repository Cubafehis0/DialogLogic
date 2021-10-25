using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 需要指定目标的卡牌瞄准器
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class OneTargetAimer : BaseAimer
{
    private LineRenderer lineRenderer = null;

    public static OneTargetAimer instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }

    protected override void UpdateVisual()
    {
        if (aimer)
        {
            aimer.transform.localPosition = Vector2.MoveTowards(aimer.transform.localPosition, Vector2.zero, 20f * Time.deltaTime);
            Vector2 pos0 = aimer.transform.position;
            Vector2 pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, pos0);
            lineRenderer.SetPosition(1, pos1);
        }
    }

    private void OnEnable()
    {
        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
    }
}
