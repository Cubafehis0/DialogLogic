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
    private PilePacked drawPilePacked;
    [SerializeField]
    private PilePacked discardPilePacked;
    [SerializeField]
    private PilePacked handPilePacked;
    [SerializeField]
    private PilePacked playingPilePacked;
    [SerializeField]
    private PilePacked exhaustPilePacked;
    [SerializeField]
    private int baseEnergy;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int baseDraw;

    private CardPlayerState cardPlayerState;
    private IPile<Card> hand => handPilePacked;//handPilePacked;
    private IPile<Card> drawPile => drawPilePacked;
    private IPile<Card> discardPile => discardPilePacked;
    private IPile<Card> exhaustPile => exhaustPilePacked;
    private IPile<Card> playingPile => playingPilePacked;

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

    public ModifierGroup Modifiers => handPilePacked.cardPile.Modifiers;

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
                //��������
                return;
            }
            if (drawPile.Count == 0)
            {
                //���ƶ�Ϊ��
                if (discardPile.Count == 0)
                {
                    //û���ƿɳ�
                    return;
                }
                //ϴ��
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
    /// �������
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
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardΪ��");
        if (card.info == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardδ����");
        if (cardPlayerState.Energy < card.GetFinalCost())
        {
            //��������
            Debug.Log("��������");
        }
        else
        {
            if (card.Activated || (card.info.Requirements?.Invoke() ?? true))
            {
                //Ŀ����Ч�ҿ���ʹ��
                cardPlayerState.Energy -= card.GetFinalCost();
                Debug.Log("ʹ�ÿ��ƣ� " + card.info.Title);
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
                if (card.info.Effects == null) Debug.Log("��Ч��");
                else card.info.Effects.Invoke();
                yield return new WaitUntil(() => ForegoundGUISystem.current == false);
                playingPile.MigrateTo(card, card.info.Exhaust ? exhaustPile : discardPile);
            }
            else
            {
                Debug.Log("�޷�ʹ�ÿ��ƣ�" + card.info.Title);
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
        Debug.Log("�ҵĻغϣ��鿨������");
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
