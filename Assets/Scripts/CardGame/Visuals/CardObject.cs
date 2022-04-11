using SemanticTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class CardObject : MonoBehaviour
{
    [SerializeField]
    private Card card = null;
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text cdtDescText;
    [SerializeField]
    private Text eftDescText;
    [SerializeField]
    private Text memeText;
    private Canvas cardUICanvas = null;
    private int orderInLayer = 0;

    private void Awake()
    {
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

    public Card Card { get => card; set => card = value; }

    public void UpdateVisuals()
    {
        if (card == null) return;
        if (titleText) titleText.text = card.info.Title;
        if (cdtDescText) cdtDescText.text = card.info.ConditionDesc;
        if (eftDescText) eftDescText.text = card.info.EffectDesc;
        if (memeText) memeText.text = card.info.Meme;
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }
}
