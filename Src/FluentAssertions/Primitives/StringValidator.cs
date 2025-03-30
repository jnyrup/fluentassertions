using FluentAssertions.Common;
using FluentAssertions.Execution;

namespace FluentAssertions.Primitives;

internal class StringValidator
{
    private readonly IStringComparisonStrategy comparisonStrategy;
    private AssertionChain assertionChain;

    public StringValidator(AssertionChain assertionChain, IStringComparisonStrategy comparisonStrategy, string because,
        object[] becauseArgs)
    {
        this.comparisonStrategy = comparisonStrategy;
        this.assertionChain = assertionChain.BecauseOf(because, becauseArgs);
    }

    public void Validate(string? subject, string? expected)
    {
        if (expected is null && subject is null)
        {
            return;
        }

        if (!ValidateAgainstNulls(subject, expected))
        {
            return;
        }

        if (expected.IsLongOrMultiline() || subject.IsLongOrMultiline())
        {
            assertionChain = assertionChain.UsingLineBreaks;
        }

        comparisonStrategy.ValidateAgainstMismatch(assertionChain, subject, expected);
    }

    private bool ValidateAgainstNulls(string? subject, string? expected)
    {
        if (expected is null == subject is null)
        {
            return true;
        }

        assertionChain.FailWith($"{comparisonStrategy.ExpectationDescription}{{0}}{{reason}}, but found {{1}}.", expected, subject);
        return false;
    }
}
