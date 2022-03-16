using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//响应鼠标事件接口

public interface ICardObject : IVisuals
{
    void UpdateTitle();
    void UpdateCdtDesc();
    void UpdateEftDesc();
    void UpdateMeme();
}

[RequireComponent(typeof(Card))]
public class CardObject : MonoBehaviour, ICardObject,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    public UnityEvent<Card> OnPointerEnter = new UnityEvent<Card>();
    public UnityEvent<Card> OnPointerExit = new UnityEvent<Card>();
    public UnityEvent<Card> OnPointerDown = new UnityEvent<Card>();

    private Renderer spriteRenderer = null;

    private Canvas cardUICanvas = null;

    [SerializeField]
    private Text titleText = null;
    [SerializeField]
    private Text cdtDescText = null;
    [SerializeField]
    private Text eftDescText = null;
    [SerializeField]
    private Text memeText = null;

    private int orderInLayer = 0;
    public int OrderInLayer
    {
        get => orderInLayer;
        set
        {
            orderInLayer = value;
            if (spriteRenderer) spriteRenderer.sortingOrder = value;
            if (cardUICanvas) cardUICanvas.sortingOrder = value;

        }
    }

    /// <summary>
    /// 卡牌描述，后续会改成Card信息类
    /// </summary>
    private Card card = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        card = GetComponent<Card>();
        cardUICanvas = GetComponentInChildren<Canvas>();
    }

    public void UpdateTitle()
    {
        if (titleText) titleText.text = card.info.title;
    }

    public void UpdateCdtDesc()
    {
        if (cdtDescText) cdtDescText.text = card.info.LocalizedConditionDesc;
    }

    public void UpdateEftDesc()
    {
        if (eftDescText) eftDescText.text = card.info.LocalizedEffectDesc;
    }
    public void UpdateMeme()
    {
        if (memeText) memeText.text = card.info.LocalizedMeme;
    }
    public void UpdateVisuals()
    {
        UpdateTitle();
        UpdateCdtDesc();
        UpdateEftDesc();
        UpdateMeme();
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }

    public void GetCardComponent()
    {
        card = GetComponent<Card>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnter.Invoke(card);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OnPointerExit.Invoke(card);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown.Invoke(card);
    }
}
