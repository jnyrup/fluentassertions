using System;
using System.Reflection;

namespace FluentAssertions.Common
{
    public static class ObjectExtensions
    {
        public static bool IsSameOrEqualTo(this object actual, object expected)
        {
            if (actual is null && expected is null)
            {
                return true;
            }

            if (actual is null)
            {
                return false;
            }

            if (expected is null)
            {
                return false;
            }

            if (actual.Equals(expected))
            {
                return true;
            }

            Type expectedType = expected.GetType();
            Type actualType = actual.GetType();

            return expectedType != typeof(string) && actualType != typeof(string)
                && CanConvert(actual, expected, actualType, expectedType)
                && CanConvert(expected, actual, expectedType, actualType);
        }

        private static bool CanConvert(object source, object target, Type sourceType, Type targetType)
        {
            try
            {
                var converted = Convert.ChangeType(source, targetType);

                return (source.Equals(Convert.ChangeType(converted, sourceType)))
                    && converted.Equals(target);
            }
            catch
            {
                // ignored
                return false;
            }
        }
    }
}
