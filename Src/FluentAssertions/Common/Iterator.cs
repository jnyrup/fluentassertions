using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FluentAssertions.Common;

/// <summary>
/// A smarter enumerator that can provide information about the relative location (current, first, last)
/// of the current item within the collection without unnecessarily iterating the collection.
/// </summary>
internal sealed class Iterator<T> : IEnumerator<T>
{
    private const int InitialIndex = -1;
    private readonly IEnumerable<T> enumerable;
    private readonly int? maxItems;
    private IEnumerator<T> enumerator;
    private T? current;
    private T? next;

    private bool hasCompleted;

    public Iterator(IEnumerable<T> enumerable, int maxItems = int.MaxValue)
    {
        this.enumerable = enumerable;
        this.maxItems = maxItems;

        Reset();
    }

    [MemberNotNull(nameof(enumerator))]
    public void Reset()
    {
        Index = InitialIndex;

        enumerator = enumerable.GetEnumerator();
        HasCurrent = false;
        HasNext = false;
        hasCompleted = false;
        current = default;
        next = default;
    }

    [MemberNotNullWhen(true, nameof(current))]
    private bool HasCurrent { get; set; }

    [MemberNotNullWhen(true, nameof(next))]
    private bool HasNext { get; set; }

    public int Index { get; private set; }

    public bool IsFirst => Index == 0;

    public bool IsLast => (HasCurrent && !HasNext) || HasReachedMaxItems;

    object? IEnumerator.Current => Current;

    public T Current
    {
        get
        {
            if (!HasCurrent)
            {
                throw new InvalidOperationException($"Please call {nameof(MoveNext)} first");
            }

            return current;
        }

        private set
        {
            current = value;
            HasCurrent = true;
        }
    }

    public bool MoveNext()
    {
        if (!hasCompleted && FetchCurrent())
        {
            PrefetchNext();
            return true;
        }

        hasCompleted = true;
        return false;
    }

    private bool FetchCurrent()
    {
        if (HasNext && !HasReachedMaxItems)
        {
            Current = next;
            Index++;

            return true;
        }

        if (enumerator.MoveNext() && !HasReachedMaxItems)
        {
            Current = enumerator.Current;
            Index++;

            return true;
        }

        hasCompleted = true;

        return false;
    }

    public bool HasReachedMaxItems => Index == maxItems;

    private void PrefetchNext()
    {
        if (enumerator.MoveNext())
        {
            next = enumerator.Current;
            HasNext = true;
        }
        else
        {
            next = default;
            HasNext = false;
        }
    }

    public bool IsEmpty
    {
        get
        {
            if (!HasCurrent && !hasCompleted)
            {
                throw new InvalidOperationException($"Please call {nameof(MoveNext)} first");
            }

            return Index == InitialIndex;
        }
    }

    public void Dispose()
    {
        enumerator.Dispose();
    }
}
