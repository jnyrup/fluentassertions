namespace FluentAssertions.Collections.MaximumMatching
{
    internal interface IPredicate<TValue>
    {
        bool Matches(TValue value);
    }
}
