using System;

namespace FluentAssertions.Common
{
    internal static class Guard
    {
        public static void ThrowIfArgumentIsAmbiguous<T>(T[] obj, string paramName)
        {
            if (obj is null)
            {
                throw new ArgumentException("It is ambiguous whether it is a null collection or a collection of a null item", paramName);
            }
        }

        public static void ThrowIfArgumentIsNull<T>(T obj, string paramName)
            where T : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIfArgumentIsNull<T>(T obj, string paramName, string message)
            where T : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void ThrowIfArgumentIsNullOrEmpty(string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIfArgumentIsNullOrEmpty(string str, string paramName, string message)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
