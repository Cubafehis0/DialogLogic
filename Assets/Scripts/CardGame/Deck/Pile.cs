using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PileType 
{
    public static int Hand { get => 1; }
    public static int DrawDeck { get => 2; }
    public static int DiscardDeck { get => 4; }
    public static int All { get => 7; }
}

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
    public Pile(IEnumerable<T> i) :base(i){

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
        List<T> tmp=new List<T>(this);
        foreach(var item in tmp)
        {
            Remove(item);
        }
    }

    public void MigrateTo(T card, Pile<T> newPile)
    {
        if (card == null || newPile == null)
        {
            Debug.LogError("Migrate Error: ���벻��Ϊ��");
            return;
        }
        if (Contains(card))
        {
            if (this == newPile)
            {
                Debug.Log("MigrateTo Error: ����ת�Ƶ�����");
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
            Debug.LogError("MigrateAllto Error: ����ת�Ƶ����ƶ�");
            return;
        }
        if (this == to)
        {
            Debug.LogError("MigrateAllto Error: ����ת�Ƶ������ƶ�");
            return;
        }
        List<T> tmp = new List<T>(this);
        foreach (var c in tmp)
        {
            MigrateTo(c, to);
        }
    }
}
