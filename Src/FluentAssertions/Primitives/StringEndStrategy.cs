using System.Collections.Generic;
using FluentAssertions.Common;
using FluentAssertions.Execution;

namespace FluentAssertions.Primitives;

internal class StringEndStrategy : IStringComparisonStrategy
{
    private readonly IEqualityComparer<string> comparer;
    private readonly string predicateDescription;

    public StringEndStrategy(IEqualityComparer<string> comparer, string predicateDescription)
    {
        this.comparer = comparer;
        this.predicateDescription = predicateDescription;
    }

    public string ExpectationDescription => $"Expected {{context:string}} to {predicateDescription} ";

    public void ValidateAgainstMismatch(AssertionChain assertionChain, string subject, string expected)
    {
        assertionChain
            .ForCondition(subject.Length >= expected.Length)
            .FailWith($"{ExpectationDescription}{{0}}{{reason}}, but {{1}} is too short.", expected, subject);

        if (!assertionChain.Succeeded)
        {
            return;
        }

        int indexOfMismatch = subject.Substring(subject.Length - expected.Length).IndexOfFirstMismatch(expected, comparer);

        if (indexOfMismatch < 0)
        {
            return;
        }

        assertionChain.FailWith(
            $"{ExpectationDescription}{{0}}{{reason}}, but {{1}} differs near {subject.IndexedSegmentAt(indexOfMismatch)}.",
            expected, subject);
    }
}
