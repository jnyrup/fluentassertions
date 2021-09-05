using System;
using System.Diagnostics.Contracts;
using FluentAssertions.Numeric;

namespace FluentAssertions
{
    public static class ComparableAssertionsExtensions
    {
        /// <summary>
        /// Returns an <see cref="ComparableTypeAssertions{T}"/> object that can be used to assert the
        /// current <see cref="IComparable{T}"/>.
        /// </summary>
        [Pure]
        public static ComparableTypeAssertions<T> Should<T>(this T comparableValue)
            where T : IComparable<T>
        {
            return new ComparableTypeAssertions<T>(comparableValue);
        }
    }
}
