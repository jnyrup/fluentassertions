using System;

namespace FluentAssertions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class DebuggerNonUserCodeAttribute : Attribute
    {
    }
}
