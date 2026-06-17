using System;
using Tasks.DoNotChange;

namespace Tasks;

public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
{
    private readonly DoublyLinkedList<T> _storage = [];

    public T Dequeue()
    {
        EnsureNotEmpty();
        return _storage.RemoveAt(0);
    }
    public void Enqueue(T item) => _storage.Add(item);

    public T Pop()
    {
        EnsureNotEmpty();
        return _storage.RemoveAt(_storage.Length - 1);
    }
    public void Push(T item) => _storage.Add(item);

    private void EnsureNotEmpty()
    {
        if (_storage.Length == 0)
            throw new InvalidOperationException();
    }
}
