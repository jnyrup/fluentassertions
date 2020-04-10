namespace FluentAssertions.Equivalency
{
    internal class AssertionContext<TSubject> : AssertionContext<TSubject, TSubject>, IAssertionContext<TSubject>
    {
        public AssertionContext(SelectedMemberInfo subjectProperty, TSubject subject, TSubject expectation, string because, object[] becauseArgs)
            : base(subjectProperty, subject, expectation, because, becauseArgs)
        {
        }

        new internal static IAssertionContext<TSubject> CreateFromEquivalencyValidationContext(IEquivalencyValidationContext context)
        {
            TSubject expectation = (context.Expectation != null) ? (TSubject)context.Expectation : default;

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

        public SelectedMemberInfo SubjectProperty { get; }

        public TSubject Subject { get; }

        public TExpectation Expectation { get; }

        public string Because { get; set; }

        public object[] BecauseArgs { get; set; }

        internal static IAssertionContext<TSubject, TExpectation> CreateFromEquivalencyValidationContext(IEquivalencyValidationContext context)
        {
            TExpectation expectation = (context.Expectation != null) ? (TExpectation)context.Expectation : default;

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
