using CardGame.Recorder;
using SemanticTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICardController
{
    IReadonlyPile<Card> DiscardPile { get; }
    IReadonlyPile<Card> DrawPile { get; }
    IReadonlyPile<Card> Hand { get; }
    bool IsHandFull { get; }
    bool DrawBan { get; set; }

    void AddCard(PileType pileType, Card card);
    void AddCardSet2DrawPile(List<string> cardset);
    bool CanDraw();
    void ClearTemporaryActivateFlags();
    void Discard2Draw();
    void DiscardAll();
    void DiscardCard(Card cid);
    void Draw(uint num);
    void Draw2Full();
    int? GetPileProp(string name);
    void PlayCard(Card card);
}

[RequireComponent(typeof(CardPlayerState))]
public class CardController : MonoBehaviour, ICardController
{
    private CardPlayerState cardPlayerState;
    private Pile<Card> hand = new Pile<Card>();
    private Pile<Card> drawPile = new Pile<Card>();
    private Pile<Card> discardPile = new Pile<Card>();
    private Pile<Card> exhaustPile = new Pile<Card>();
    private bool drawBan = false;
    public UnityEvent OnPlayCard = new UnityEvent();
    public IReadonlyPile<Card> Hand { get => hand; }
    public IReadonlyPile<Card> DrawPile { get => drawPile; }
    public IReadonlyPile<Card> DiscardPile { get => discardPile; }
    public IReadonlyPile<Card> ExhaustPile { get => exhaustPile; }

    public bool DrawBan { get => drawBan; set => drawBan = value; }
    public bool IsHandFull => Hand.Count == cardPlayerState.Player.PlayerInfo.MaxCardNum;

    private void Awake()
    {
        cardPlayerState = GetComponent<CardPlayerState>();
        Hand.OnAdd.AddListener(x =>
        {
            if (x.info.handModifier != null)
                cardPlayerState.AddModifier(x.info.handModifier);
        });
        Hand.OnRemove.AddListener(x =>
        {
            if (x.info.handModifier != null)
                cardPlayerState.RemoveModifier(x.info.handModifier);
        });
    }

    public bool CanDraw()
    {
        if (drawBan) return false;
        if (IsHandFull) return false;
        if (drawPile.Count == 0 && discardPile.Count == 0) return false;
        return true;
    }

    public void Draw(uint num)
    {
        if (drawBan) return;
        for (int i = 0; i < num; i++)
        {
            if (IsHandFull)
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

            Card card = drawPile[0];
            Context.PushPlayerContext(cardPlayerState);
            Context.PushCardContext(card);
            drawPile.MigrateTo(card, hand);
            card.info.DrawEffects?.Execute();
            Context.PopCardContext();
            Context.PopPlayerContext();
        }
    }

    public void Draw2Full()
    {
        for (int i = 0; i < 20; i++)
        {
            if (!CanDraw()) break;
            Draw(1);
        }
    }

    public void DiscardCard(Card cid)
    {
        hand.MigrateTo(cid, discardPile);
    }

    public void DiscardAll()
    {
        hand.MigrateAllTo(discardPile);
    }

    public void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
    }


    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="cardID">出牌id</param>
    /// <returns>是否成功出牌</returns>
    public void PlayCard(Card card)
    {
        if (!ForegoundGUISystem.current)
        {
            StartCoroutine(PlayCardEnumerator(card));
        }
    }

    private IEnumerator PlayCardEnumerator(Card card)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard card为空");
        if (card.info == null) throw new ArgumentNullException("CardPlayerState.PlayCard card未构建");
        if (cardPlayerState.Energy < card.FinalCost)
        {
            //能量不足
            Debug.Log("能量不足");
        }
        else
        {
            Context.PushPlayerContext(cardPlayerState);
            Context.PushCardContext(card);
            if (card.Activated || (card.info.Requirements?.Value ?? true))
            {
                //目标有效且可以使用
                cardPlayerState.Energy -= card.FinalCost;
                Debug.Log("使用卡牌： " + card.info.Title);
                CardLogEntry log = new CardLogEntry
                {
                    Name = card.info.Name,
                    IsActive = card.Activated,
                    LogType = ActionTypeEnum.PlayCard,
                    Turn = CardGameManager.Instance.Turn,
                    CardCategory = card.info.category,
                };
                CardRecorder.Instance.AddRecordEntry(log);
                hand.MigrateTo(card, card.info.Exhaust ? exhaustPile : discardPile);
                OnPlayCard.Invoke();
                if (card.info.Effects == null) Debug.Log("空效果");
                else card.info.Effects.Execute();
                yield return new WaitUntil(() => ForegoundGUISystem.current == false);
            }
            else
            {
                Debug.Log("无法使用卡牌：" + card.info.Title);
            }
            Context.PopCardContext();
            Context.PopPlayerContext();
        }
    }

    public void ClearTemporaryActivateFlags()
    {
        foreach (Card card in Hand)
        {
            card.TemporaryActivate = false;
        }
        foreach (Card card in DiscardPile)
        {
            card.TemporaryActivate = false;
        }
        foreach (Card card in DrawPile)
        {
            card.TemporaryActivate = false;
        }
    }

    public void AddCardSet2DrawPile(List<string> cardset)
    {

        foreach (string name in cardset)
        {
            Card newCard = StaticCardLibrary.Instance.GetByName(name);
            StaticCardLibrary.Instance.GetNewCardObject(newCard);
            drawPile.Add(newCard);
        }
        drawPile.Shuffle();
    }

    public void AddCard(PileType pileType, Card card)
    {
        switch (pileType)
        {
            case PileType.Hand:
                hand.Add(card);
                break;
            case PileType.DrawDeck:
                drawPile.Add(card);
                break;
            case PileType.DiscardDeck:
                discardPile.Add(card);
                break;
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

}
