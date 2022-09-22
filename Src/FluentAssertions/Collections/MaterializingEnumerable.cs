using System.Collections;
using System.Collections.Generic;

namespace FluentAssertions.Collections;

internal sealed class MaterializingEnumerable<T> : IEnumerable<T>
{
    private readonly List<T> materialized = new();

    private readonly IEnumerator<T> enumerator;

    private bool fullyEnumerated;

    public MaterializingEnumerable(IEnumerable<T> enumerable)
    {
        enumerator = enumerable.GetEnumerator();
    }

    private IEnumerable<T> GetElements()
    {
        foreach (var item in materialized)
        {
            yield return item;
        }

        while (enumerator.MoveNext())
        {
            T item = enumerator.Current;
            materialized.Add(item);
            yield return item;
        }

        fullyEnumerated = true;
        enumerator.Dispose();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() =>
        (fullyEnumerated ? materialized : GetElements()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
}
