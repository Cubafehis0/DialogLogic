using CardGame.Recorder;
using SemanticTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardManager
{
    IReadonlyPile<Card> DiscardPile { get; }
    IReadonlyPile<Card> DrawPile { get; }
    IReadonlyPile<Card> Hand { get; }
    bool IsHandFull { get; }
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
public class CardManager : MonoBehaviour, ICardManager
{
    private CardPlayerState cardPlayerState;
    private Pile<Card> hand = new Pile<Card>();
    private Pile<Card> drawPile = new Pile<Card>();
    private Pile<Card> discardPile = new Pile<Card>();
    private Pile<Card> exhaustPile = new Pile<Card>();

    public IReadonlyPile<Card> Hand { get => hand; }
    public IReadonlyPile<Card> DrawPile { get => drawPile; }
    public IReadonlyPile<Card> DiscardPile { get => discardPile; }

    public bool IsHandFull => Hand.Count == cardPlayerState.Player.PlayerInfo.MaxCardNum;

    private void Awake()
    {
        cardPlayerState = GetComponent<CardPlayerState>();
        Hand.OnAdd.AddListener(x => cardPlayerState.Modifiers.Add(x.info.handModifier));
        Hand.OnRemove.AddListener(x => cardPlayerState.Modifiers.Remove(x.info.handModifier));
    }

    public bool CanDraw()
    {
        if (cardPlayerState.DrawBan) return false;
        if (IsHandFull) return false;
        if (drawPile.Count == 0 && discardPile.Count == 0) return false;
        return true;
    }

    public void Draw(uint num)
    {
        if (cardPlayerState.DrawBan) return;
        for (int i = 0; i < num; i++)
        {
            if (IsHandFull)
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
            drawPile.MigrateTo(drawPile[0], hand);
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
    /// ����
    /// </summary>
    /// <param name="cardID">����id</param>
    /// <returns>�Ƿ�ɹ�����</returns>
    public void PlayCard(Card card)
    {
        if (!CardGameManager.Instance.WaitGUI)
        {
            StartCoroutine(PlayCardEnumerator(card));
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
            Context.PushCardContext(card);
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
                CardRecorder.Instance.AddRecordEntry(log);
                hand.MigrateTo(card, card.info.Exhaust ? exhaustPile : discardPile);
                cardPlayerState.OnPlayCard.Invoke();
                if (card.info.Effects == null) Debug.Log("��Ч��");
                card.info.Effects.Execute();
                yield return new WaitUntil(() => CardGameManager.Instance.WaitGUI == false);
            }
            else
            {
                Debug.Log("�޷�ʹ�ÿ��ƣ�" + card.info.Title);
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
            Card newCard = CardGameManager.Instance.GetCardCopy(StaticCardLibrary.Instance.GetByName(name));
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
