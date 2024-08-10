using System;
using FluentAssertions.Execution;

namespace FluentAssertions.Equivalency.Steps;

public class StringEqualityEquivalencyStep : IEquivalencyStep
{
    public EquivalencyResult Handle(Comparands comparands, IEquivalencyValidationContext context,
        IEquivalencyValidator nestedValidator)
    {
        Type expectationType = comparands.GetExpectedType(context.Options);

        if (expectationType is null || expectationType != typeof(string))
        {
            return EquivalencyResult.ContinueWithNext;
        }

        if (!ValidateAgainstNulls(comparands, context.CurrentNode))
        {
            return EquivalencyResult.AssertionCompleted;
        }

        bool subjectIsString = ValidateSubjectIsString(comparands, context.CurrentNode);

        if (subjectIsString)
        {
            string subject = (string)comparands.Subject;
            string expectation = (string)comparands.Expectation;

            subject.Should()
                .Be(expectation, CreateOptions(context.Options),
                    context.Reason.FormattedMessage, context.Reason.Arguments);
        }

        return EquivalencyResult.AssertionCompleted;
    }

    private static Func<EquivalencyOptions<string>, EquivalencyOptions<string>>
        CreateOptions(IEquivalencyOptions existingOptions) =>
        o =>
        {
            if (existingOptions is EquivalencyOptions<string> equivalencyOptions)
            {
                return equivalencyOptions;
            }

            if (existingOptions.IgnoreLeadingWhitespace)
            {
                o.IgnoringLeadingWhitespace();
            }

            if (existingOptions.IgnoreTrailingWhitespace)
            {
                o.IgnoringTrailingWhitespace();
            }

            if (existingOptions.IgnoreCase)
            {
                o.IgnoringCase();
            }

            if (existingOptions.IgnoreNewlineStyle)
            {
                o.IgnoringNewlineStyle();
            }

            return o;
        };

    private static bool ValidateAgainstNulls(Comparands comparands, INode currentNode)
    {
        object expected = comparands.Expectation;
        object subject = comparands.Subject;

        bool onlyOneNull = expected is null != subject is null;

        if (onlyOneNull)
        {
            AssertionScope.Current.AddNonReportable("ValidateAgainstNulls", currentNode.Description);
            AssertionScope.Current.FailWith(
                "Expected {ValidateAgainstNulls} to be {0}{reason}, but found {1}.", expected, subject);

            return false;
        }

        return true;
    }

    private static bool ValidateSubjectIsString(Comparands comparands, INode currentNode)
    {
        if (comparands.Subject is string)
        {
            return true;
        }

        AssertionScope.Current.AddNonReportable("ValidateSubjectIsString", currentNode);

        return
            AssertionScope.Current
                .FailWith("Expected {ValidateSubjectIsString} to be {0}{reason}, but found {1}.",
                    comparands.RuntimeType, comparands.Subject.GetType());
    }
}
