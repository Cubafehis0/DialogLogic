using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pile<T> : List<T>
{
    private UnityEvent<T> onAdd = new UnityEvent<T>();
    private UnityEvent<T> onRemove = new UnityEvent<T>();
    private UnityEvent onShuffle = new UnityEvent();

    public UnityEvent<T> OnAdd { get => onAdd; }
    public UnityEvent<T> OnRemove { get => onRemove; }
    public UnityEvent OnShuffle { get => onShuffle; }

    public Pile() : base()
    {

    }
    public Pile(IEnumerable<T> i) : base(i)
    {

    }

    public void Shuffle()
    {
        MyMath.Shuffle(this);
        OnShuffle.Invoke();
    }

    public new void Add(T item)
    {
        base.Add(item);
        OnAdd.Invoke(item);
    }

    public new void Remove(T item)
    {
        base.Remove(item);
        OnRemove.Invoke(item);
    }

    public new void Clear()
    {
        List<T> tmp = new List<T>(this);
        foreach (var item in tmp)
        {
            Remove(item);
        }
    }

    public void MigrateTo(T card, Pile<T> newPile)
    {
        if (card == null || newPile == null)
        {
            Debug.LogError("Migrate Error: 输入不能为空");
            return;
        }
        if (Contains(card))
        {
            if (this == newPile)
            {
                Debug.Log("MigrateTo Error: 不能转移到自身");
                return;
            }
            Remove(card);
            newPile.Add(card);
        }

    }

    public void MigrateAllTo(Pile<T> to)
    {
        if (to == null)
        {
            Debug.LogError("MigrateAllto Error: 不能转移到空牌堆");
            return;
        }
        if (this == to)
        {
            Debug.LogError("MigrateAllto Error: 不能转移到自身牌堆");
            return;
        }
        List<T> tmp = new List<T>(this);
        foreach (var c in tmp)
        {
            MigrateTo(c, to);
        }
    }
}
