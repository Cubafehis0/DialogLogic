using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPileObject : ArrangePileObject
{

    /// <summary>
    /// ��ǰ����
    /// </summary>
    public static HandPileObject instance;
    //��������---------------------------------------------------------




    // �м������,������Inspector���޸�-----------------------------------------------





    // �м��������-----------------------------------------------


    

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("��������HandsArrangement����");
        }
        OnSelectCard.AddListener(OnEventMousePress);
    }

    private void OnEnable()
    {
        Pile = CardPlayerState.Instance.Hand;
    }

    private void OnDisable()
    {
        Pile = null;
    }

    private void Start()
    {
        NoTargetAimer.instance.AddCallback(delegate
        {
            lockFocus = false;
            FocusedCard = null;
        });
    }



    public virtual void SelectCard(Card card)
    {
        FocusedCard = card;
        lockFocus = true;
        NoTargetAimer.instance.StartAiming(card);
    }
    protected override void OnAdd(Card card)
    {
        base.OnAdd(card);
        int index = pile.IndexOf(card);
        card.gameObject.SetActive(true);
        card.transform.SetSiblingIndex(index);
        //��ȱ��
        CardObject cardObject = card.GetComponent<CardObject>();
        if (cardObject)
        {
            cardObject.OnPointerEnter.AddListener(OnEventMouseEnter);
            cardObject.OnPointerExit.AddListener(OnEventMouseExit);
        }
        for(int i=0;i<pile.Count;i++)
        {
            pile[i].GetComponent<CardObject>().OrderInLayer = i;
        }
        RecalculatePosition();
    }

    protected override void OnRemove(Card oldCard)
    {
        if (oldCard == FocusedCard)
        {
            lockFocus = false;
            FocusedCard = null;
        }
        oldCard.transform.SetParent(null, true);
        CardObject cardObject = oldCard.GetComponent<CardObject>();
        if (cardObject)
        {
            cardObject.OnPointerEnter.RemoveListener(OnEventMouseEnter);
            cardObject.OnPointerExit.RemoveListener(OnEventMouseExit);
        }
        RecalculatePosition();
    }

    private void OnEventMouseEnter(Card sender)
    {
        if (sender && !lockFocus) FocusedCard = sender;
    }
    private void OnEventMouseExit(Card card)
    {
        if (!lockFocus) FocusedCard = null;
    }
    private void OnEventMousePress(Card sender)
    {
        if (sender) SelectCard(sender);
    }
}
