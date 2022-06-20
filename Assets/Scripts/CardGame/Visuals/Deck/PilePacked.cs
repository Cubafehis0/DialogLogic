using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PilePacked : MonoBehaviour, IPile<Card>
{
    private List<Card> cards=new List<Card>();
    [SerializeField]
    protected CardPlayerState playerState;
    [SerializeField]
    private PileType type;
    [SerializeField]
    private UnityEvent onShuffle = new UnityEvent();
    [SerializeField]
    private UnityEvent<Card> onAdd = new UnityEvent<Card>();
    [SerializeField]
    private UnityEvent<Card> onRemove = new UnityEvent<Card>();

    public int Count => cards.Count;
    public Card this[int index] => ((IReadOnlyList<Card>)cards)[index];
    public UnityEvent OnShuffle { get => onShuffle; }
    public UnityEvent<Card> OnAdd { get => onAdd;}
    public UnityEvent<Card> OnRemove { get => onRemove;}

    public virtual void Add(Card item)
    {
        cards.Add(item);

        OnAdd.Invoke(item);
    }

    public virtual void Remove(Card item)
    {
        cards.Remove(item);
        OnRemove.Invoke(item);
    }

    public void Clear()
    {
        List<Card> tmp = new List<Card>(this);
        foreach (var item in tmp)
        {
            Remove(item);
        }
        cards.Clear();
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    public void MigrateAllTo(IPile<Card> to)
    {
        if (to == null)
        {
            Debug.LogError("MigrateAllto Error: 不能转移到空牌堆");
            return;
        }
        //有问题
        if (ReferenceEquals(this, to))
        {
            Debug.LogError("MigrateAllto Error: 不能转移到自身牌堆");
            return;
        }
        List<Card> tmp = new List<Card>(this);
        foreach (var c in tmp)
        {
            MigrateTo(c, to);
        }
    }

    public void MigrateTo(Card card, IPile<Card> newPile)
    {
        if (card == null || newPile == null)
        {
            Debug.LogError("Migrate Error: 输入不能为空");
            return;
        }
        if (ReferenceEquals(this, newPile))
        {
            Debug.Log("MigrateTo Error: 不能转移到自身");
            return;
        }
        if (cards.Contains(card))
        {
            Remove(card);
            newPile.Add(card);
        }
    }



    public void Shuffle()
    {
        MyMath.Shuffle(cards);
        OnShuffle.Invoke();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)cards).GetEnumerator();
    }
}
