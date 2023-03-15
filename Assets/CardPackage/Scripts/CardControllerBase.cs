using ModdingAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICardControllerEventListener<T> where T : CardBase
{
    void OnPlayCard(Card card);
    void OnDraw(Card card);
    void OnDiscard(Card card);
    void OnDiscard2Draw();
    void OnEnergyChange();
}
public class CardControllerBase<T> : Singleton<CardControllerBase<T>>, IProperties where T : CardBase
{
    [SerializeField]
    protected PilePacked drawPile;
    [SerializeField]
    protected PilePacked discardPile;
    [SerializeField]
    public PilePacked hand;
    [SerializeField]
    protected PilePacked playingPile;
    [SerializeField] 
    protected PilePacked exhaustPile;
    [SerializeField]
    private int maxHandCnt;

    protected List<ICardControllerEventListener<T>> eventListeners;
    private bool drawBan = false;

    public bool DrawBan { get => drawBan; set => drawBan = value; }


    public override void Awake()
    {
        base.Awake();
        eventListeners = new List<ICardControllerEventListener<T>>(GetComponents<ICardControllerEventListener<T>>());
    }

    public void AddCard(PileType pileType, CardBase card, PilePositionType pilePosition)
    {
        switch (pileType)
        {
            case PileType.Hand:
                if (!IsHandFull()) hand.Add(card);
                break;
            case PileType.DrawDeck:
                drawPile.Add(card);
                break;
            case PileType.DiscardDeck:
                discardPile.Add(card);
                break;
        }
    }

    public void AddNewCard(PileType pileType, string name, PilePositionType pilePosition)
    {
        Card newCard = DynamicCardLibrary.Instance.AddNewCard(name);

        AddCard(pileType, newCard, pilePosition);
    }

    public void ShuffleDraw()
    {
        drawPile.Shuffle();
    }
    public bool CanDraw()
    {
        if (drawBan) return false;
        if (IsHandFull()) return false;
        if (drawPile.Count == 0 && discardPile.Count == 0) return false;
        return true;
    }

    public void DiscardAll()
    {
        hand.MigrateAllTo(discardPile);
    }

    public void DiscardCard(CardBase cid)
    {
        hand.MigrateTo(cid, discardPile);
    }

    public void Draw(int num = 1)
    {

        while (num-- > 0)
        {
            if (drawBan) return;
            if (IsHandFull())
            {
                //手牌满了
                return;
            }
            if (drawPile.Count == 0)
            {
                //抽牌堆为空
                if (discardPile.Count == 0)
                {
                    //没有牌可抽
                    return;
                }
                //洗牌
                Discard2Draw();
                foreach (var listener in eventListeners)
                {
                    listener.OnDiscard2Draw();
                }
            }
            CardBase card = drawPile[0];
            Context.SetCardAlias("FROM", card.id);
            Context.SetPlayerAlias("FROM", card.id);
            drawPile.MigrateTo(card, hand);
            card.OnDraw();
            foreach (var listener in eventListeners)
            {
                listener.OnDraw((Card)card);
            }
            Context.SetCardAlias("FROM", null);
            Context.SetPlayerAlias("FROM", null);
        }
    }

    public void Draw2Full()
    {
        for (int i = 0; i < 20; i++)
        {
            if (!CanDraw()) break;
            Draw();
        }
    }

    public bool IsHandFull()
    {
        return hand.Count == maxHandCnt;
    }


    /// <summary>
    /// 打出卡牌
    /// </summary>
    /// <param name="card"></param>
    public virtual void PlayCard(CardBase card, GameObject target)
    {

    }

    private void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
    }

    public bool TryGetInt(string key, out int value)
    {
        switch (key)
        {
            case "hand_count":
                value = hand.Count;
                return true;
            case "draw_count":
                value = drawPile.Count;
                return true;
            case "discard_count":
                value = discardPile.Count;
                return true;
            default:
                value = 0;
                return false;
        };
    }
}