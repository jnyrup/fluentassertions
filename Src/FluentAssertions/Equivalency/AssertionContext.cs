using System.Reflection;

namespace FluentAssertions.Equivalency
{
    internal class AssertionContext<TSubject> : IAssertionContext<TSubject>
    {
        public AssertionContext(SelectedMemberInfo subjectProperty, TSubject subject, TSubject expectation, string because,
                                object[] becauseArgs)
        {
            SubjectProperty = subjectProperty;
            Subject = subject;
            Expectation = expectation;
            Because = because;
            BecauseArgs = becauseArgs;
        }

        public SelectedMemberInfo SubjectProperty { get; private set; }
        public TSubject Subject { get; private set; }
        public TSubject Expectation { get; private set; }
        public string Because { get; set; }
        public object[] BecauseArgs { get; set; }

        internal static AssertionContext<TSubject> CreateFromEquivalencyValidationContext(IEquivalencyValidationContext context)
        {
            var expectation = (context.Expectation != null) ? (TSubject)context.Expectation : default(TSubject);

            var assertionContext = new AssertionContext<TSubject>(
                context.SelectedMemberInfo,
                (TSubject)context.Subject,
                expectation,
                context.Because,
                context.BecauseArgs);
            return assertionContext;
        }
    }

    internal class AssertionContext<TSubject, TExpectation> : IAssertionContext<TSubject, TExpectation>
    {
        public AssertionContext(SelectedMemberInfo subjectProperty, TSubject subject, TExpectation expectation, string because,
                                object[] becauseArgs)
        {
            SubjectProperty = subjectProperty;
            Subject = subject;
            Expectation = expectation;
            Because = because;
            BecauseArgs = becauseArgs;
        }

        public SelectedMemberInfo SubjectProperty { get; private set; }
        public TSubject Subject { get; private set; }
        public TExpectation Expectation { get; private set; }
        public string Because { get; set; }
        public object[] BecauseArgs { get; set; }

        internal static AssertionContext<TSubject, TExpectation> CreateFromEquivalencyValidationContext(IEquivalencyValidationContext context)
        {
            var expectation = (context.Expectation != null) ? (TExpectation)context.Expectation : default(TExpectation);

            var assertionContext = new AssertionContext<TSubject, TExpectation>(
                context.SelectedMemberInfo,
                (TSubject)context.Subject,
                expectation,
                context.Because,
                context.BecauseArgs);
            return assertionContext;
        }
    }
}
