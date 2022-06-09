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
    bool DrawBan { get; set; }
    bool IsHandFull();
    bool CanDraw();

    void AddCard(PileType pileType, string name);
    void AddCard(PileType pileType, Card card);
    void AddCard(PileType pileType, IEnumerable<string> names);
    void AddCard(PileType pileType, IEnumerable<Card> cards);

    void DiscardAll();
    void DiscardCard(Card cid);
    void Draw(int num = 1);
    void Draw2Full();
    int? GetPileProp(string name);
    void PlayCard(Card card);

}

public class PlayingPile : Pile<Card>
{
    public PlayingPile() : base()
    {
        OnAdd.AddListener(x => Context.PushCardContext(x));
        OnRemove.AddListener(x => Context.PopCardContext());
    }
}

[RequireComponent(typeof(CardPlayerState))]
public class CardController : MonoBehaviour, ICardController, ITurnEnd
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


    private CardPlayerState cardPlayerState;
    private HandPile hand = new HandPile();
    private IPile<Card> drawPile => drawPilePacked;
    private IPile<Card> discardPile => discardPilePacked;
    private IPile<Card> exhaustPile => exhaustPilePacked;
    private IPile<Card> playingPile => playingPilePacked;

    private bool drawBan = false;
    public UnityEvent OnPlayCard = new UnityEvent();
    public IReadonlyPile<Card> Hand { get => hand; }
    public IReadonlyPile<Card> DrawPile { get => drawPile; }
    public IReadonlyPile<Card> DiscardPile { get => discardPile; }
    public IReadonlyPile<Card> ExhaustPile { get => exhaustPile; }
    public IReadonlyPile<Card> PlayingPile { get => playingPile; }

    public bool DrawBan { get => drawBan; set => drawBan = value; }

    public bool IsHandFull()
    {
        return Hand.Count == cardPlayerState.Player.PlayerInfo.MaxCardNum;
    }

    private void Awake()
    {
        cardPlayerState = GetComponent<CardPlayerState>();
    }

    public IReadonlyModifierGroup Modifiers => hand.Modifiers;

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
        if (!ForegoundGUISystem.current && CardGameManager.Instance.isPlayerTurn)
        {
            AnimationManager.Instance.AddAnimation(PlayCardEnumerator(card));
        }
    }


    private IEnumerator PlayCardEnumerator(Card card)
    {
        if (card == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardΪ��");
        if (card.info == null) throw new ArgumentNullException("CardPlayerState.PlayCard cardδ����");
        if (cardPlayerState.Energy < card.FinalCost)
        {
            //��������
            Debug.Log("��������");
        }
        else
        {
            Context.PushPlayerContext(cardPlayerState);
            if (card.Activated || (card.info.Requirements?.Value ?? true))
            {
                //Ŀ����Ч�ҿ���ʹ��
                cardPlayerState.Energy -= card.FinalCost;
                Debug.Log("ʹ�ÿ��ƣ� " + card.info.Title);
                CardLogEntry log = new CardLogEntry
                {
                    Name = card.info.Name,
                    IsActive = card.Activated,
                    LogType = ActionTypeEnum.PlayCard,
                    Turn = CardGameManager.Instance.Turn,
                    CardCategory = card.info.category,
                };
                CardGameManager.Instance.CardRecorder.AddRecordEntry(log);
                hand.MigrateTo(card, playingPile);
                OnPlayCard.Invoke();
                if (card.info.Effects == null) Debug.Log("��Ч��");
                else card.info.Effects.Execute();
                yield return new WaitUntil(() => ForegoundGUISystem.current == false);
                playingPile.MigrateTo(card, card.info.Exhaust ? exhaustPile : discardPile);
            }
            else
            {
                Debug.Log("�޷�ʹ�ÿ��ƣ�" + card.info.Title);
            }
            Context.PopPlayerContext();
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
