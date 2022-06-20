using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PilePacked : MonoBehaviour, IPile<Card>
{
    [SerializeField]
    public HandPile cardPile = new HandPile();

    public Card this[int index] => ((IReadOnlyList<Card>)cardPile)[index];

    public UnityEvent OnShuffle => cardPile.OnShuffle;

    public UnityEvent<Card> OnAdd => cardPile.OnAdd;

    public UnityEvent<Card> OnRemove => cardPile.OnRemove;

    public int Count => cardPile.Count;

    public void Add(Card item)
    {
        cardPile.Add(item);
    }

    public void Clear()
    {
        cardPile.Clear();
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return cardPile.GetEnumerator();
    }

    public void MigrateAllTo(IPile<Card> to)
    {
        cardPile.MigrateAllTo(to);
    }

    public void MigrateTo(Card card, IPile<Card> newPile)
    {
        this.cardPile.MigrateTo(card, newPile);
    }

    public void Remove(Card item)
    {
        cardPile.Remove(item);
    }

    public void Shuffle()
    {
        cardPile.Shuffle();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)cardPile).GetEnumerator();
    }
}
