using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPile
{
    List<Card> CardsList { get; }
    Card GetCardByOrderID(int index);
    void UpdateBindingObject();
    void Add(Card card);
    void Remove(Card card);
    void MigrateAllTo(IPile newPile);
    void MigrateTo(Card card, IPile newPile);
}

public class Pile : MonoBehaviour, IPile
{
    /// <summary>
    /// ��ǰ���еĿ�������
    /// </summary>
    private List<Card> cardsList = new List<Card>();
    public List<Card> CardsList { get => cardsList; }

    private List<IPileListener> listeners = new List<IPileListener>();

    private void Start()
    {
        UpdateBindingObject();
    }

    public Card GetCardByOrderID(int index)
    {
        if (0 <= index && index < cardsList.Count)
        {
            return cardsList[index];
        }
        return null;
    }
    public void Shuffle()
    {
        Debug.Log("ϴ�� δʵ��");
    }

    public void Add(Card card)
    {
        if (cardsList.Contains(card))
        {
            Debug.LogError("������ӿ���ʧ�ܣ������Ѿ�����");
            return;
        }
        cardsList.Add(card);
        card.parentPile = this;
        BroadcastOnAdd(card);
    }

    private void BroadcastOnAdd(Card newCard)
    {
        //UpdateBindingObject();
        foreach (IPileListener listener in listeners)
            listener.OnAdd(newCard);
    }

    public void Remove(Card card)
    {
        if (!cardsList.Contains(card))
        {
            Debug.LogError("Remove Error");
            return;
        }
        cardsList.Remove(card);
        card.parentPile = null;
        BroadcastRemove(card);
    }

    private void BroadcastRemove(Card oldCard)
    {
        //UpdateBindingObject();
        foreach (IPileListener listener in listeners)
            listener.OnRemove(oldCard);
    }

    public void MigrateTo(Card card, IPile newPile)
    {
        if (card.parentPile != (IPile)this)
        {
            Debug.Log("MigrateTo Error: " + name + " û�� " + card.name);
            return;
        }
        PileMigrateUtils.MigrateTo(card, newPile);
    }

    public void MigrateAllTo(IPile to)
    {
        if (to == null)
        {
            Debug.LogError("MigrateAllto Error: ����ת�Ƶ����ƶ�");
            return;
        }
        if ((IPile)this == to)
        {
            Debug.LogError("MigrateAllto Error: ����ת�Ƶ������ƶ�");
            return;
        }
        List<Card> tmp = new List<Card>(cardsList);
        foreach (Card c in tmp)
        {
            MigrateTo(c, to);
        }
    }

    public void UpdateBindingObject()
    {
        listeners = new List<IPileListener>(GetComponents<IPileListener>());
    }
}
