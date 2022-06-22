using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ModdingAPI;
public class PilePacked : MonoBehaviour, IPile<CardBase>
{
    private List<CardBase> cards=new List<CardBase>();
    [SerializeField]
    private PileType type;
    [SerializeField]
    private UnityEvent onShuffle = new UnityEvent();
    [SerializeField]
    private UnityEvent<CardBase> onAdd = new UnityEvent<CardBase>();
    [SerializeField]
    private UnityEvent<CardBase> onRemove = new UnityEvent<CardBase>();

    public int Count => cards.Count;
    public CardBase this[int index] => ((IReadOnlyList<CardBase>)cards)[index];
    public UnityEvent OnShuffle { get => onShuffle; }
    public UnityEvent<CardBase> OnAdd { get => onAdd;}
    public UnityEvent<CardBase> OnRemove { get => onRemove;}

    public virtual void Add(CardBase item)
    {
        cards.Add(item);

        OnAdd.Invoke(item);
    }

    public virtual void Remove(CardBase item)
    {
        
        cards.Remove(item);
        OnRemove.Invoke(item);
    }

    public void Clear()
    {
        List<CardBase> tmp = new List<CardBase>(this);
        foreach (var item in tmp)
        {
            Remove(item);
        }
        cards.Clear();
    }

    public IEnumerator<CardBase> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    public void MigrateAllTo(IPile<CardBase> to)
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
        List<CardBase> tmp = new List<CardBase>(this);
        foreach (var c in tmp)
        {
            MigrateTo(c, to);
        }
    }

    public void MigrateTo(CardBase card, IPile<CardBase> newPile)
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
