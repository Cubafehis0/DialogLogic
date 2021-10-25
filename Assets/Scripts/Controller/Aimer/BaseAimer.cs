using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Aimer用于选择目标
/// </summary>
public class BaseAimer : MonoBehaviour
{
    protected UnityEvent<CardObject,GameObject> callback = new UnityEvent<CardObject,GameObject>();

    protected CardObject aimer;

    /// <summary>
    /// 供外部调用，强制瞄准一个目标
    /// </summary>
    /// <param name="target">目标</param>
    public void AimTarget(GameObject target)
    {
        if (callback != null)
        {
            callback?.Invoke(aimer,target);
            enabled = false;
        }
    }

    /// <summary>
    /// 添加选中目标后的回调函数
    /// </summary>
    /// <param name="callback">返回目标选择的发起者Card和选择的GameObject</param>
    public void AddCallback(UnityAction<CardObject, GameObject> callback)
    {
        if (callback == null) return;
        this.callback.AddListener(callback);
    }

    /// <summary>
    /// 启用瞄准器
    /// </summary>
    /// <param name="aimer">发起者Card</param>
    /// <param name="callback">回调函数</param>
    public void StartAiming(CardObject aimer, UnityAction<CardObject, GameObject> callback = null)
    {
        this.aimer = aimer;
        if (callback != null) AddCallback(callback);
        DisableHUD();
        enabled = true;
    }

    /// <summary>
    /// 强制取消瞄准器
    /// </summary>
    public void CancelAiming()
    {
        enabled = false;
        AimTarget(null);
    }


    protected virtual void UpdateVisual() { }

    protected virtual bool CheckValidTarget(GameObject target) {return target.CompareTag("Character"); }


    private void DisableHUD()
    {
        IHUD hud = GetComponent<IHUD>();
        if (hud != null)
        {
            MonoBehaviour script = hud as MonoBehaviour;
            if (script) script.enabled = false;
        }
    }

    private void Update()
    {
        UpdateVisual();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {

            GameObject target = My2DRaycaster.GetCurrentObject(LayerMask.GetMask("Character"));
            if (target && CheckValidTarget(target))
            {
                AimTarget(target);
                enabled = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
            CancelAiming();
    }



}
