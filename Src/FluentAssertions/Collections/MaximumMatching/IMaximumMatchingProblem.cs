using System.Collections.Generic;

namespace FluentAssertions.Collections.MaximumMatching
{
    internal interface IMaximumMatchingProblem<TValue>
    {
        List<IPredicate<TValue>> Predicates { get; }

        List<Element<TValue>> Elements { get; }
    }
}
