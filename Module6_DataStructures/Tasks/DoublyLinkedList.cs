using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks;

public class DoublyLinkedList<T> : IDoublyLinkedList<T>
{
    private sealed class Node(T value)
    {
        public T Value { get; } = value;
        public Node? Previous { get; set; }
        public Node? Next { get; set; }
    }

    private Node? _head;
    private Node? _tail;

    public int Length { get; private set; }
    public void Add(T e) => AddAt(Length, e);
    public void AddAt(int index, T e)
    {
        ValidateInsertIndex(index);
        Node newNode = new(e);
        if (Length == 0)
        {
            _head = _tail = newNode;
        }
        else if (index == 0)
        {
            newNode.Next = _head;
            _head!.Previous = newNode;
            _head = newNode;
        }
        else if (index == Length)
        {
            newNode.Previous = _tail;
            _tail!.Next = newNode;
            _tail = newNode;
        }
        else
        {
            Node current = GetNodeAt(index);
            Node previous = current.Previous!;

            newNode.Previous = previous;
            newNode.Next = current;
            previous.Next = newNode;
            current.Previous = newNode;
        }
        Length++;
    }

    public T ElementAt(int index) => GetNodeAt(index).Value;
    public IEnumerator<T> GetEnumerator() => new Enumerator(_head);

    public void Remove(T item)
    {
        Node? current = _head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                RemoveNode(current);
                return;
            }
            current = current.Next;
        }
    }
    public T RemoveAt(int index)
    {
        Node node = GetNodeAt(index);
        T value = node.Value;

        RemoveNode(node);

        return value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static void ValidateInsertIndex(int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException();
    }

    private Node GetNodeAt(int index)
    {
        if (index < 0 || index >= Length)
            throw new IndexOutOfRangeException();

        if (index <= Length / 2)
        {
            Node current = _head!;
            for (int i = 0; i < index; i++)
                current = current.Next!;

            return current;
        }

        Node tail = _tail!;
        for (int i = Length - 1; i > index; i--)
            tail = tail.Previous!;

        return tail;
    }

    private void RemoveNode(Node node)
    {
        Node? previous = node.Previous;
        Node? next = node.Next;

        if (previous is null) _head = next;
        else previous.Next = next;

        if (next is null) _tail = previous;
        else next.Previous = previous;

        Length--;
    }

    private sealed class Enumerator(Node? head) : IEnumerator<T>
    {
        private Node? _current;
        private bool _started;

        public T Current => _current!.Value;
        object IEnumerator.Current => _current!.Value!;

        public bool MoveNext()
        {
            if (!_started)
            {
                _current = head;
                _started = true;
            }
            else
            {
                _current = _current?.Next;
            }
            return _current is not null;
        }

        public void Reset()
        {
            _started = false;
            _current = null;
        }
        public void Dispose() { }
    }
}
