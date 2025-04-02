using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace FluentAssertions.Xml;

/// <summary>
/// Contains a number of methods to assert that an <see cref="XAttribute"/> is in the expected state.
/// </summary>
[DebuggerNonUserCode]
public class XAttributeAssertions : ReferenceTypeAssertions<XAttribute, XAttributeAssertions>
{
    private readonly AssertionChain assertionChain;

    /// <summary>
    /// Initializes a new instance of the <see cref="XAttributeAssertions" /> class.
    /// </summary>
    public XAttributeAssertions(XAttribute? attribute, AssertionChain assertionChain)
        : base(attribute, assertionChain)
    {
        this.assertionChain = assertionChain;
    }

    /// <summary>
    /// Asserts that the current <see cref="XAttribute"/> equals the <paramref name="expected"/> attribute.
    /// </summary>
    /// <param name="expected">The expected attribute</param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    public AndConstraint<XAttributeAssertions> Be(XAttribute? expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(Subject?.Name == expected?.Name && Subject?.Value == expected?.Value)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context} to be {0}{reason}, but found {1}.", expected, Subject);

        return new AndConstraint<XAttributeAssertions>(this);
    }

    /// <summary>
    /// Asserts that the current <see cref="XAttribute"/> does not equal the <paramref name="unexpected"/> attribute,
    /// using its <see cref="object.Equals(object)" /> implementation.
    /// </summary>
    /// <param name="unexpected">The unexpected attribute</param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    public AndConstraint<XAttributeAssertions> NotBe(XAttribute? unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(!(Subject?.Name == unexpected?.Name && Subject?.Value == unexpected?.Value))
            .BecauseOf(because, becauseArgs)
            .FailWith("Did not expect {context} to be {0}{reason}.", unexpected);

        return new AndConstraint<XAttributeAssertions>(this);
    }

    /// <summary>
    /// Asserts that the current <see cref="XAttribute"/> has the specified <paramref name="expected"/> value.
    /// </summary>
    /// <param name="expected">The expected value</param>
    /// <param name="because">
    /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
    /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
    /// </param>
    /// <param name="becauseArgs">
    /// Zero or more objects to format using the placeholders in <paramref name="because" />.
    /// </param>
    public AndConstraint<XAttributeAssertions> HaveValue(string expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject is not null)
            .FailWith("Expected the attribute to have value {0}{reason}, but {context:member} is <null>.", expected);

        if (assertionChain.Succeeded)
        {
            assertionChain
                .ForCondition(Subject!.Value == expected)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} \"{0}\" to have value {1}{reason}, but found {2}.",
                    Subject.Name, expected, Subject.Value);
        }

        return new AndConstraint<XAttributeAssertions>(this);
    }

    /// <summary>
    /// Returns the type of the subject the assertion applies on.
    /// </summary>
    protected override string Identifier => "XML attribute";
}
