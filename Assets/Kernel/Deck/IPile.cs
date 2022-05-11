using UnityEngine.Events;

public interface IPile<T>:IReadonlyPile<T>
{
    UnityEvent OnShuffle { get; }
    void Add(T item);
    void Clear();
    void MigrateAllTo(IPile<T> to);
    void MigrateTo(T card, IPile<T> newPile);
    void Remove(T item);
    void Shuffle();
}