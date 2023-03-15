using CardGame.Recorder;
using ModdingAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : CardControllerBase<Card>, ITurnEnd, ITurnStart
{
    [SerializeField]
    private int baseEnergy;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int baseDraw;

    List<CostModifier> costModifiers = new List<CostModifier>();
    public int Energy
    {
        get => energy;
        set
        {
            if(energy != value)
            {
                energy = value;
                foreach (var listener in eventListeners)
                {
                    listener.OnEnergyChange();
                }
            }
        }
    }
    public void OnTurnStart()
    {
        Debug.Log("�ҵĻغϣ��鿨������");
        Energy = baseEnergy;
        Draw(baseDraw);
    }
    public int GetFinalCost(Card card)
    {
        int res = card.BaseCost;
        if (card.Activated)
        {
            return 0;
        }
        foreach (var modifer in costModifiers)//��ȱ��
        {
            if (modifer != null && (modifer.Condition?.Invoke() ?? true))
            {
                res = modifer.num.Invoke();
            }
        }
        return res;
    }

    public override void PlayCard(CardBase card, GameObject target)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardΪ��");

        if (card.CheckCanPlay(gameObject, out _))
        {
            //Ŀ����Ч�ҿ���ʹ��
            Energy -= (GetFinalCost((Card)card));
            hand.MigrateTo(card, playingPile);
            foreach (var listener in eventListeners)
            {
                listener.OnPlayCard((Card)card);
            }
            card.Excute(target);
            //yield return new WaitUntil(() => !AnimationManager.Instance.IsGUI());
            playingPile.MigrateTo(card, ((Card)card).Exhaust ? exhaustPile : discardPile);
        }

    }
    public void OnTurnEnd()
    {
        //ClearTemporaryActivateFlags();
    }
}
