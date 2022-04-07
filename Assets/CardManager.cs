using CardGame.Recorder;
using SemanticTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardPlayerState))]
public class CardManager : MonoBehaviour
{
    private CardPlayerState cardPlayerState;
    private Pile<Card> hand = new Pile<Card>();
    private Pile<Card> drawPile = new Pile<Card>();
    private Pile<Card> discardPile = new Pile<Card>();
    private Pile<Card> exhaustPile = new Pile<Card>();

    public Pile<Card> Hand { get => hand; }
    public Pile<Card> DrawPile { get => drawPile; }
    public Pile<Card> DiscardPile { get => discardPile; }

    public bool IsHandFull => Hand.Count == cardPlayerState.Player.PlayerInfo.MaxCardNum;

    private void Awake()
    {
        cardPlayerState = GetComponent<CardPlayerState>();
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
            drawPile.MigrateTo(drawPile[0], Hand);
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
        Hand.MigrateTo(cid, discardPile);
    }

    public void DiscardAll()
    {
        Hand.MigrateAllTo(discardPile);
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

    public IEnumerator PlayCardEnumerator(Card card)
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
                Hand.MigrateTo(card, card.info.Exhaust ? exhaustPile : discardPile);
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

}
