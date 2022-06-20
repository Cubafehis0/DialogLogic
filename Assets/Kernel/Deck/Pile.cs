using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IReadonlyPile<T> : IReadOnlyList<T>
{
    public UnityEvent<T> OnAdd { get; }
    public UnityEvent<T> OnRemove { get; }
}

public class Pile<T> : List<T>, IReadonlyPile<T>, IPile<T>
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

    public new virtual void Add(T item)
    {
        base.Add(item);
        OnAdd.Invoke(item);
    }

    public new virtual void Remove(T item)
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

    public void MigrateTo(T card, IPile<T> newPile)
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

    public void MigrateAllTo(IPile<T> to)
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
