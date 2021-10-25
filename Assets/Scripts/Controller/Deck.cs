using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeckEventHandler
{
    public void OnDeckUpdate();
    public void OnDeckAdd(CardObject card);
}

public class Deck : MonoBehaviour
{
    /// <summary>
    /// ���Է�װ
    /// ��ǰ���еĿ������壬�������Ӽ��٣���Ҫ����UpdateReferenceˢ��
    /// ��set��������get���������ṩ����
    /// </summary>
    private CardObject[] cardsList;
    public CardObject[] CardsList { get { return (CardObject[])cardsList.Clone(); } private set => cardsList = value; }

    /// <summary>
    /// Ŀǰû�ж�̬���Deck��������������ڳ�ʼ������Ҫ����
    /// </summary>
    private IDeckEventHandler[] handlers;
    private void Awake()
    {
        handlers = GetComponents<IDeckEventHandler>();
    }

    public void UpdateReference()
    {
        cardsList = transform.GetComponentsInChildren<CardObject>();
        if (handlers!=null)
            foreach (IDeckEventHandler handler in handlers)
                handler.OnDeckUpdate();
    }

    public CardObject GetCard(int index)
    {
        if (0 <= index && index < cardsList.Length)
            return cardsList[index];
        return null;
    }

    public CardObject[] GetAllCards()
    {
        return CardsList;
    }

    public void Add(CardObject card)
    {
        card.deck = this;
        card.transform.SetParent(transform,true);
        card.transform.SetAsLastSibling();
        UpdateReference();
        if (handlers != null)
            foreach (IDeckEventHandler handler in handlers)
                handler.OnDeckAdd(card);

    }


    public static void MigrateTo(CardObject card,Deck newDeck)
    {
        if (card == null || newDeck == null) return;
        if (card.deck!=newDeck)
        {
            Deck oldDeck = card.deck;
            newDeck.Add(card);
            if (oldDeck)
            {
                oldDeck.UpdateReference();
            }
        }
    }
}
