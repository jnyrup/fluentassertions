using System.Collections.Generic;
using System.Linq;

namespace FluentAssertions.Collections.MaximumMatching
{
    /// <summary>
    /// The <see cref="MaximumMatchingSolution{TElement}"/> class defines the solution (output) for the maximum matching problem.
    /// See documentation of <see cref="MaximumMatchingProblem{TElement}"/> for more details.
    /// </summary>
    /// <typeparam name="TValue">The type of elements which must be matched with predicates.</typeparam>
    internal class MaximumMatchingSolution<TValue>
    {
        private readonly Dictionary<IPredicate<TValue>, Element<TValue>> elementsByMatchedPredicate;
        private readonly IMaximumMatchingProblem<TValue> problem;

        public MaximumMatchingSolution(
            IMaximumMatchingProblem<TValue> problem,
            Dictionary<IPredicate<TValue>, Element<TValue>> elementsByMatchedPredicate)
        {
            this.problem = problem;
            this.elementsByMatchedPredicate = elementsByMatchedPredicate;
        }

        public bool UnmatchedPredicatesExist => problem.Predicates.Count != elementsByMatchedPredicate.Count;

        public bool UnmatchedElementsExist => problem.Elements.Count != elementsByMatchedPredicate.Count;

        public List<IPredicate<TValue>> GetUnmatchedPredicates()
        {
            return problem.Predicates.Except(elementsByMatchedPredicate.Keys).ToList();
        }

        public List<Element<TValue>> GetUnmatchedElements()
        {
            return problem.Elements.Except(elementsByMatchedPredicate.Values).ToList();
        }
    }
}
