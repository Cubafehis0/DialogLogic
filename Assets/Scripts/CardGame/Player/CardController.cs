using CardGame.Recorder;
using ModdingAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CardPlayerState))]
public class CardController : MonoBehaviour, ICardController, ITurnEnd, ITurnStart
{
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
    private int baseEnergy;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int baseDraw;

    private CardPlayerState cardPlayerState;

    private bool drawBan = false;

    public UnityEvent OnEnergyChange = new UnityEvent();
    public UnityEvent OnPlayCard = new UnityEvent();
    public IReadonlyPile<Card> Hand { get => hand; }
    public IReadonlyPile<Card> DrawPile { get => drawPile; }
    public IReadonlyPile<Card> DiscardPile { get => discardPile; }
    public IReadonlyPile<Card> ExhaustPile { get => exhaustPile; }
    public IReadonlyPile<Card> PlayingPile { get => playingPile; }

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
    public bool IsHandFull()
    {
        return Hand.Count == cardPlayerState.Player.PlayerInfo.MaxCardNum;
    }

    private void Awake()
    {
        cardPlayerState = GetComponent<CardPlayerState>();
    }

    public bool CanDraw()
    {
        if (drawBan) return false;
        if (IsHandFull()) return false;
        if (drawPile.Count == 0 && discardPile.Count == 0) return false;
        return true;
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
            Card card = drawPile[0];
            Context.SetCardAlias("FROM", card.id);
            Context.SetPlayerAlias("FROM", card.id);
            drawPile.MigrateTo(card, hand);
            card.info.DrawEffects?.Invoke();
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

    public void DiscardCard(Card cid)
    {
        hand.MigrateTo(cid, discardPile);
    }

    public void DiscardAll()
    {
        hand.MigrateAllTo(discardPile);
    }

    private void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
    }


    /// <summary>
    /// 打出卡牌
    /// </summary>
    /// <param name="card"></param>
    public void PlayCard(Card card)
    {
        if (!ForegoundGUISystem.current && CardGameManager.Instance.TurnManager.IsPlayerTurn)
        {
            AnimationManager.Instance.AddAnimation(PlayCardEnumerator(card));
        }
    }


    private IEnumerator PlayCardEnumerator(Card card)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard card为空");
        if (card.info == null) throw new ArgumentNullException("CardPlayerState.PlayCard card未构建");
        if (cardPlayerState.Energy < card.GetFinalCost())
        {
            //能量不足
            Debug.Log("能量不足");
        }
        else
        {
            if (card.Activated || (card.info.Requirements?.Invoke() ?? true))
            {
                //目标有效且可以使用
                cardPlayerState.Energy -= card.GetFinalCost();
                Debug.Log("使用卡牌： " + card.info.Title);
                CardLogEntry log = new CardLogEntry
                {
                    Name = card.info.Name,
                    IsActive = card.Activated,
                    LogType = ActionTypeEnum.PlayCard,
                    Turn = CardGameManager.Instance.TurnManager.Turn,
                    CardCategory = card.info.category,
                };
                CardGameManager.Instance.CardRecorder.AddRecordEntry(log);
                hand.MigrateTo(card, playingPile);
                OnPlayCard.Invoke();
                if (card.info.Effects == null) Debug.Log("空效果");
                else card.info.Effects.Invoke();
                yield return new WaitUntil(() => ForegoundGUISystem.current == false);
                playingPile.MigrateTo(card, card.info.Exhaust ? exhaustPile : discardPile);
            }
            else
            {
                Debug.Log("无法使用卡牌：" + card.info.Title);
            }
        }
    }


    public void AddCard(PileType pileType, Card card)
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

    public void AddCard(PileType pileType, IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            AddCard(pileType, card);
        }
    }

    public void AddCard(PileType pileType, string name)
    {
        Card newCard = GameManager.Instance.CardLibrary.GetCopyByName(name);
        GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        AddCard(pileType, newCard);
    }

    public void AddCard(PileType pileType, IEnumerable<string> names)
    {
        foreach (string name in names)
        {
            Card newCard = GameManager.Instance.CardLibrary.GetCopyByName(name);
            GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
            AddCard(pileType, newCard);
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

    public void OnTurnStart()
    {
        Debug.Log("我的回合，抽卡！！！");
        Energy = baseEnergy;
        Draw(baseDraw);
    }

    public void OnTurnEnd()
    {
        ClearTemporaryActivateFlags();
    }
    private void ClearTemporaryActivateFlags()
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
}
