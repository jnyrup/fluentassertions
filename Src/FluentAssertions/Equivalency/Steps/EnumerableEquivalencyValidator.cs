using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions.Collections.MaximumMatching;
using FluentAssertions.Equivalency.Execution;
using FluentAssertions.Equivalency.Tracing;
using FluentAssertions.Execution;
using FluentAssertions.Formatting;
using static System.FormattableString;

namespace FluentAssertions.Equivalency.Steps
{
    /// <summary>
    /// Executes a single equivalency assertion on two collections, optionally recursive and with or without strict ordering.
    /// </summary>
    internal class EnumerableEquivalencyValidator
    {
        private const int FailedItemsFastFailThreshold = 10;

        #region Private Definitions

        private readonly IEquivalencyValidator parent;
        private readonly IEquivalencyValidationContext context;

        #endregion

        public EnumerableEquivalencyValidator(IEquivalencyValidator parent, IEquivalencyValidationContext context)
        {
            this.parent = parent;
            this.context = context;
            Recursive = false;
        }

        public bool Recursive { get; set; }

        public OrderingRuleCollection OrderingRules { get; set; }

        public void Execute<T>(object[] subject, T[] expectation)
        {
            if (AssertIsNotNull(expectation, subject) && AssertCollectionsHaveSameCount(subject, expectation))
            {
                if (Recursive)
                {
                    using var _ = context.Tracer.WriteBlock(member =>
                        Invariant($"Structurally comparing {subject} and expectation {expectation} at {member.Description}"));
                    AssertElementGraphEquivalency(subject, expectation, context.CurrentNode);
                }
                else
                {
                    using var _ = context.Tracer.WriteBlock(member =>
                        Invariant($"Comparing subject {subject} and expectation {expectation} at {member.Description} using simple value equality"));
                    subject.Should().BeEquivalentTo(expectation);
                }
            }
        }

        private static bool AssertIsNotNull(object expectation, object[] subject)
        {
            return AssertionScope.Current
                .ForCondition(expectation is not null)
                .FailWith("Expected {context:subject} to be <null>, but found {0}.", new object[] { subject });
        }

        private static Continuation AssertCollectionsHaveSameCount<T>(ICollection<object> subject, ICollection<T> expectation)
        {
            return AssertionScope.Current
                .WithExpectation("Expected {context:subject} to be a collection with {0} item(s){reason}", expectation.Count)
                .AssertEitherCollectionIsNotEmpty(subject, expectation)
                .Then
                .AssertCollectionHasEnoughItems(subject, expectation)
                .Then
                .AssertCollectionHasNotTooManyItems(subject, expectation)
                .Then
                .ClearExpectation();
        }

        private void AssertElementGraphEquivalency<T>(object[] subjects, T[] expectations, INode currentNode)
        {
            unmatchedSubjectIndexes = new List<int>(subjects.Length);
            unmatchedSubjectIndexes.AddRange(Enumerable.Range(0, subjects.Length));

            if (OrderingRules.IsOrderingStrictFor(new ObjectInfo(new Comparands(subjects, expectations, typeof(T[])), currentNode)))
            {
                AssertElementGraphEquivalencyWithStrictOrdering(subjects, expectations);
            }
            else
            {
                AssertElementGraphEquivalencyWithLooseOrdering(subjects, expectations);
            }
        }

        private void AssertElementGraphEquivalencyWithStrictOrdering<T>(object[] subjects, T[] expectations)
        {
            int failedCount = 0;
            foreach (int index in Enumerable.Range(0, expectations.Length))
            {
                T expectation = expectations[index];

                using var _ = context.Tracer.WriteBlock(member =>
                    Invariant($"Strictly comparing expectation {expectation} at {member.Description} to item with index {index} in {subjects}"));
                bool succeeded = StrictlyMatchAgainst(subjects, expectation, index);
                if (!succeeded)
                {
                    failedCount++;
                    if (failedCount >= FailedItemsFastFailThreshold)
                    {
                        context.Tracer.WriteLine(member =>
                            $"Aborting strict order comparison of collections after {FailedItemsFastFailThreshold} items failed at {member.Description}");
                        break;
                    }
                }
            }
        }

        private void AssertElementGraphEquivalencyWithLooseOrdering<T>(object[] subjects, T[] expectations)
        {
            var predicatesList = expectations.Select<T, Expression<Func<object, bool>>>((e, i) => (object s) => TryToMatch(s, e, i));
            var maximumMatchingProblem = new MaximumMatchingProblem<object>(predicatesList, subjects);
            var maximumMatchingSolution = maximumMatchingProblem.Solve();

            if (maximumMatchingSolution.UnmatchedPredicatesExist || maximumMatchingSolution.UnmatchedElementsExist)
            {
                string message = string.Empty;
                var doubleNewLine = Environment.NewLine + Environment.NewLine;

                List<Collections.MaximumMatching.Predicate<object>> unmatchedPredicates = maximumMatchingSolution.GetUnmatchedPredicates()
                    .Cast<Collections.MaximumMatching.Predicate<object>>().ToList();
                if (unmatchedPredicates.Any())
                {
                    message += doubleNewLine + "The following predicates did not have matching elements:";
                    message += doubleNewLine +
                        string.Join(Environment.NewLine,
                            unmatchedPredicates.Select(predicate => Formatter.ToString(predicate.Expression)));
                }

                List<Element<object>> unmatchedElements = maximumMatchingSolution.GetUnmatchedElements();
                if (unmatchedElements.Any())
                {
                    message += doubleNewLine + "The following elements did not match any predicate:";

                    IEnumerable<string> elementDescriptions = unmatchedElements
                        .Select(element => $"Index: {element.Index}, Element: {Formatter.ToString(element.Value)}");
                    message += doubleNewLine + string.Join(doubleNewLine, elementDescriptions);
                }

                AssertionScope.Current
                    .BecauseOf(context.Reason.FormattedMessage, context.Reason.Arguments)
                    .WithExpectation("Expected {context:collection} to satisfy all predicates{reason}, but:")
                    .FailWithPreFormatted(message);
            }
        }

        private List<int> unmatchedSubjectIndexes;

        private bool TryToMatch<T>(object subject, T expectation, int expectationIndex)
        {
            using var scope = new AssertionScope();
            parent.RecursivelyAssertEquality(new Comparands(subject, expectation, typeof(T)), context.AsCollectionItem<T>(expectationIndex));

            var failures = scope.Discard();
            bool failed = failures.Any();
            return !failed;
        }

        private bool StrictlyMatchAgainst<T>(object[] subjects, T expectation, int expectationIndex)
        {
            using var scope = new AssertionScope();
            object subject = subjects[expectationIndex];
            IEquivalencyValidationContext equivalencyValidationContext = context.AsCollectionItem<T>(expectationIndex);

            parent.RecursivelyAssertEquality(new Comparands(subject, expectation, typeof(T)), equivalencyValidationContext);

            bool failed = scope.HasFailures();
            return !failed;
        }
    }
}
