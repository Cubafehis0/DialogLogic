using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class PilePacked : MonoBehaviour, IPile<Card>
{
    private Pile<Card> card = new Pile<Card>();

    public Card this[int index] => ((IReadOnlyList<Card>)card)[index];

    public UnityEvent OnShuffle => card.OnShuffle;

    public UnityEvent<Card> OnAdd => card.OnAdd;

    public UnityEvent<Card> OnRemove => card.OnRemove;

    public int Count => card.Count;

    public void Add(Card item)
    {
        card.Add(item);
    }

    public void Clear()
    {
        card.Clear();
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return card.GetEnumerator();
    }

    public void MigrateAllTo(IPile<Card> to)
    {
        card.MigrateAllTo(to);
    }

    public void MigrateTo(Card card, IPile<Card> newPile)
    {
        this.card.MigrateTo(card, newPile);
    }

    public void Remove(Card item)
    {
        card.Remove(item);
    }

    public void Shuffle()
    {
        card.Shuffle();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)card).GetEnumerator();
    }
}
