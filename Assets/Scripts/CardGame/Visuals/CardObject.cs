using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Card))]//Cardδ�����ܸĳɷ�Mono
[RequireComponent(typeof(Canvas))]
public class CardObject : MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text cdtDescText;
    [SerializeField]
    private Text eftDescText;
    [SerializeField]
    private Text memeText;

    private Card card = null;
    private Canvas cardUICanvas = null;
    private int orderInLayer = 0;

    private void Awake()
    {
        card = GetComponent<Card>();
        cardUICanvas = GetComponent<Canvas>();
    }

    public int OrderInLayer
    {
        get => orderInLayer;
        set
        {
            if (cardUICanvas == null) cardUICanvas = GetComponent<Canvas>();
            cardUICanvas.sortingOrder = value + 10;
            orderInLayer = value;
        }
    }

    public Card Card { get => card; }

    public void UpdateVisuals()
    {
        if (titleText) titleText.text = card.Title;
        if (cdtDescText) cdtDescText.text = card.ConditionDesc;
        if (eftDescText) eftDescText.text = card.EffectDesc;
        if (memeText) memeText.text = card.Meme;
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }

    public void GetCardComponent()
    {
        card = GetComponent<Card>();
    }
}
