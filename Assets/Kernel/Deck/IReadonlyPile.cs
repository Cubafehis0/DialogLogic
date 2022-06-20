using System.Collections.Generic;
using UnityEngine.Events;

public interface IReadonlyPile<T> : IReadOnlyList<T>
{
    public UnityEvent<T> OnAdd { get; }
    public UnityEvent<T> OnRemove { get; }
}
