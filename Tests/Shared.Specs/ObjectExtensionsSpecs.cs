using System.Collections.Generic;
using FluentAssertions.Common;
using Xunit;

namespace FluentAssertions.Specs
{
    public class ObjectExtensionsSpecs
    {
        [Theory]
        [MemberData(nameof(GetNumericObjects))]
        public void IsSameOrEqualTo(object actual, object expected)
        {
            actual.IsSameOrEqualTo(expected).Should().BeTrue();
        }

        public static IEnumerable<object[]> GetNumericObjects()
        {
            object[] types = new object[]
            {
                (byte)1,
                (sbyte)1,
                (short)1,
                (ushort)1,
                (int)1,
                (uint)1,
                (long)1,
                (ulong)1,
                (float)1,
                (double)1,
                (decimal)1
            };

            foreach (var actual in types)
            {
                foreach (var expected in types)
                {
                    yield return new[] { actual, expected };
                }
            }
        }

        [Theory]
        [InlineData(0.02d, 0)]
        [InlineData(0, 0.02d)]
        [InlineData(0.02f, 0)]
        [InlineData(0, 0.02f)]
        [InlineData(long.MaxValue, 9.22337204E+18)]
        [InlineData(9.22337204E+18, long.MaxValue)]
        [InlineData(9223372030000000000L, 9.22337204E+18)]
        [InlineData(9.22337204E+18, 9223372030000000000L)]
        public void IsSameOrEqualTo_negatives(object actual, object expected)
        {
            actual.IsSameOrEqualTo(expected).Should().BeFalse();
        }
    }
}
