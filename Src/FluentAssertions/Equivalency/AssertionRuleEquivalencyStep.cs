using System;
using System.Linq.Expressions;

using FluentAssertions.Common;
using FluentAssertions.Execution;

namespace FluentAssertions.Equivalency
{
    public class AssertionRuleEquivalencyStep<TSubject>
        : AssertionRuleEquivalencyStep<TSubject, TSubject, IAssertionContext<TSubject>>
    {
        private readonly Expression<Func<IEquivalencyValidationContext, bool>> predicate;

        public AssertionRuleEquivalencyStep(Expression<Func<IEquivalencyValidationContext, bool>> predicate,
            Action<IAssertionContext<TSubject>> assertion)
            : base(assertion)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        protected internal override bool AppliesTo(IEquivalencyValidationContext context) => predicate.Compile()(context);

        protected internal override IAssertionContext<TSubject> GetAssertionContext(IEquivalencyValidationContext context) =>
            AssertionContext<TSubject>.CreateFromEquivalencyValidationContext(context);

        protected internal override Expression GetPredicateBody() => predicate.Body;
    }

    public class AssertionRuleEquivalencyStep<TSubject, TExpectation>
        : AssertionRuleEquivalencyStep<TSubject, TExpectation, IAssertionContext<TSubject, TExpectation>>
    {
        private readonly Expression<Func<IEquivalencyValidationContext, bool>> predicate;

        public AssertionRuleEquivalencyStep(Expression<Func<IEquivalencyValidationContext, bool>> predicate,
            Action<IAssertionContext<TSubject, TExpectation>> assertion)
            : base(assertion)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        protected internal override bool AppliesTo(IEquivalencyValidationContext context) => predicate.Compile()(context);

        protected internal override IAssertionContext<TSubject, TExpectation> GetAssertionContext(IEquivalencyValidationContext context) =>
            AssertionContext<TSubject, TExpectation>.CreateFromEquivalencyValidationContext(context);

        protected internal override Expression GetPredicateBody() => predicate.Body;
    }

    public abstract class AssertionRuleEquivalencyStep<TSubject, TExpectation, TAssertionContext> : IEquivalencyStep
        where TAssertionContext : IAssertionContext<TSubject, TExpectation>
    {
        private readonly Action<TAssertionContext> assertion;
        private readonly AutoConversionStep converter = new AutoConversionStep();

        protected AssertionRuleEquivalencyStep(Action<TAssertionContext> assertion)
        {
            this.assertion = assertion;
        }

        public bool CanHandle(IEquivalencyValidationContext context, IEquivalencyAssertionOptions config) => true;

        public bool Handle(IEquivalencyValidationContext context, IEquivalencyValidator parent,
            IEquivalencyAssertionOptions config)
        {
            bool success = false;
            using (var scope = new AssertionScope())
            {
                // Try without conversion
                if (AppliesTo(context))
                {
                    success = ExecuteAssertion(context);
                }

                bool converted = false;
                if (!success && converter.CanHandle(context, config))
                {
                    // Convert into a child context
                    context = context.Clone();
                    converter.Handle(context, parent, config);
                    converted = true;
                }

                if (converted && AppliesTo(context))
                {
                    // Try again after conversion
                    success = ExecuteAssertion(context);
                    if (success)
                    {
                        // If the assertion succeeded after conversion, discard the failures from
                        // the previous attempt. If it didn't, let the scope throw with those failures.
                        scope.Discard();
                    }
                }
            }

            return success;
        }

        protected internal abstract bool AppliesTo(IEquivalencyValidationContext context);

        private bool ExecuteAssertion(IEquivalencyValidationContext context)
        {
            bool subjectIsNull = context.Subject is null;

            bool subjectIsValidType =
                AssertionScope.Current
                    .ForCondition(subjectIsNull || context.Subject.GetType().IsSameOrInherits(typeof(TSubject)))
                    .FailWith("Expected " + context.SelectedMemberDescription + " from subject to be a {0}{reason}, but found a {1}.",
                        typeof(TSubject), context.Subject?.GetType());

            bool expectationIsNull = context.Expectation is null;

            bool expectationIsValidType =
                AssertionScope.Current
                    .ForCondition(expectationIsNull || context.Expectation.GetType().IsSameOrInherits(typeof(TExpectation)))
                    .FailWith("Expected " + context.SelectedMemberDescription + " from expectation to be a {0}{reason}, but found a {1}.",
                        typeof(TSubject), context.Expectation?.GetType());

            if (subjectIsValidType && expectationIsValidType)
            {
                assertion(GetAssertionContext(context));
                return true;
            }

            return false;
        }

        protected internal abstract TAssertionContext GetAssertionContext(IEquivalencyValidationContext context);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return "Invoke Action<" + typeof(TSubject).Name + "> when " + GetPredicateBody();
        }

        protected internal abstract Expression GetPredicateBody();
    }
}
