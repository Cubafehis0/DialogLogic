using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CardObject : MonoBehaviour, ICardObject
{
    private ICardEventHandler eventHandler = null;
    public ICardEventHandler EventHander
    {
        get => eventHandler;
        set => eventHandler = value;
    }

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

    public string Title
    {
        get => card.title;
        set
        {
            card.title = value;
            UpdateTitle();
        }
    }

    public string CdtDesc
    {
        get => card.CdtDesc;
        set
        {
            //card.desc = value;
            UpdateCdtDesc();
        }
    }
    public string EftDesc
    {
        get => card.EftDesc;
        set
        {
            UpdateEftDesc();
        }
    }

    public string Meme
    {
        get => card.meme;
        set
        {
            card.meme = value;
            UpdateCdtDesc();
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

    private void OnMouseEnter()
    {
        if (eventHandler != null) eventHandler.OnEventMouseEnter(card);
    }

    private void OnMouseExit()
    {
        if (eventHandler != null) eventHandler.OnEventMouseExit(card);
    }

    private void OnMouseDown()
    {
        if (eventHandler != null) eventHandler.OnEventMousePress(card, true);
    }

    public void UpdateTitle()
    {
        if (titleText) titleText.text = card.title;
    }

    public void UpdateCdtDesc()
    {
        if (cdtDescText) cdtDescText.text = card.CdtDesc;
    }

    public void UpdateEftDesc()
    {
        if (eftDescText) eftDescText.text = card.EftDesc;
    }
    public void UpdateMeme()
    {
        if (memeText) memeText.text = card.meme;
    }
    public void UpdateVisuals()
    {
        UpdateTitle();
        UpdateCdtDesc();
        UpdateEftDesc();
        UpdateMeme();
    }
    public void GetCardComponent()
    {
        card = GetComponent<Card>();
    }
}
