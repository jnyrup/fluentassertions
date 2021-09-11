using System;
using FluentAssertions.Formatting;

namespace FluentAssertions.Collections.MaximumMatching
{
    /// <summary>
    /// Stores a predicate's expression and index in the maximum matching problem.
    /// </summary>
    /// <typeparam name="TValue">The type of the element values in the maximum matching problems.</typeparam>
    internal class ActionPredicate<TValue> : IPredicate<TValue>
    {
        public ActionPredicate(Func<TValue, bool> expression, int index)
        {
            Index = index;
            Expression = expression;
        }

        /// <summary>
        /// The index of the predicate in the maximum matching problem.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The expression of the predicate.
        /// </summary>
        public Func<TValue, bool> Expression { get; }

        /// <summary>
        /// Determines whether the predicate matches the specified element.
        /// </summary>
        public bool Matches(TValue element) => Expression(element);

        public override string ToString() => $"Index: {Index}, Expression: {Formatter.ToString(Expression)}";
    }
}
