using ModdingAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardControllerBase : Singleton<CardControllerBase>
{

    public UnityEvent OnEnergyChange = new UnityEvent();
    public UnityEvent OnPlayCard = new UnityEvent();


    [SerializeField]
    private PilePacked drawPile;
    [SerializeField]
    private PilePacked discardPile;
    [SerializeField]
    private PilePacked hand;
    [SerializeField]
    private PilePacked playingPile;
    [SerializeField]
    private PilePacked exhaustPile;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int maxHandCnt;

    private bool drawBan = false;
    public IReadonlyPile<CardBase> Hand { get => hand; }
    public IReadonlyPile<CardBase> DrawPile { get => drawPile; }
    public IReadonlyPile<CardBase> DiscardPile { get => discardPile; }
    public IReadonlyPile<CardBase> ExhaustPile { get => exhaustPile; }
    public IReadonlyPile<CardBase> PlayingPile { get => playingPile; }

    public bool DrawBan { get => drawBan; set => drawBan = value; }

    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            OnEnergyChange.Invoke();
        }
    }


    public void AddCard(PileType pileType, CardBase card)
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

    public void AddCard(PileType pileType, IEnumerable<CardBase> cards)
    {
        foreach (CardBase card in cards)
        {
            AddCard(pileType, card);
        }
    }

    public void AddCard<T>(PileType pileType, string name) where T : CardBase, new()
    {
        T newCard = StaticLibraryBase<T>.Instance.GetCopyByName(name);
        Singleton<DynamicLibrary>.Instance.GetCardObject(newCard);
        AddCard(pileType, newCard);
    }

    public void AddCard<T>(PileType pileType, IEnumerable<string> names) where T : CardBase, new()
    {
        foreach (string name in names)
        {
            T newCard = StaticLibraryBase<T>.Instance.GetCopyByName(name);
            Singleton<DynamicLibrary>.Instance.GetCardObject(newCard);
            AddCard(pileType, newCard);
        }
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
            }
            CardBase card = drawPile[0];
            Context.SetCardAlias("FROM", card.id);
            Context.SetPlayerAlias("FROM", card.id);
            drawPile.MigrateTo(card, hand);
            card.OnDraw();
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

    public int? GetPileProp(string name)
    {
        return name switch
        {
            "hand_count" => Hand.Count,
            "draw_count" => DrawPile.Count,
            "discard_count" => DiscardPile.Count,
            _ => null
        };
    }
    public bool IsHandFull()
    {
        return Hand.Count == maxHandCnt;
    }


    /// <summary>
    /// 打出卡牌
    /// </summary>
    /// <param name="card"></param>
    public void PlayCard(CardBase card, GameObject target)
    {
        AnimationManager.Instance.AddAnimation(PlayCardEnumerator(card,target));
    }

    private void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
    }


    private IEnumerator PlayCardEnumerator(CardBase card,GameObject target)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard card为空");
        card.PreCalculateCost();
        if (Energy < card.cost)
        {
            //能量不足
            Debug.Log("能量不足");
        }
        else
        {
            if (card.CheckCanPlay(out _))
            {
                //目标有效且可以使用
                Energy -= card.cost;
                hand.MigrateTo(card, playingPile);
                OnPlayCard.Invoke();
                card.Excute(target);
                yield return new WaitUntil(AnimationManager.Instance.IsFree);
                playingPile.MigrateTo(card, card.exhaust ? exhaustPile : discardPile);
            }
        }
    }
}