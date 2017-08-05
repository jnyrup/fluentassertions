# Refactorings
This is a collection of refactorings to improve the usage of Fluent Assertions.

If your favourite refactoring is missing, please consider to submit it.

# MSTest

## Assert
<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeTrue();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeFalse();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsNull(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeNull();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsNotNull(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeNull();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreEqual(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().Be(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreEqual(expected, actual, delta);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeApproximately(expected, delta);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreNotEqual(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBe(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreSame(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeSameAs(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreNotSame(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeSameAs(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsInstanceOfType(actual, typeof(T));
</pre></td>
<td><pre lang="csharp">
actual.Should().BeOfType&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsNotInstanceOfType(actual, typeof(T));
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeOfType&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual == expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().Be(expected); // functionally equal
</pre></td>
<td><pre lang="csharp">
actual.Should().BeSameAs(expected); // refer to the exact same object in memory
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual == expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBe(expected); // functionally unequal
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeSameAs(expected); // refer to the different object in memory
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual != expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBe(expected); // functionally unequal
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeSameAs(expected); // refer to the different object in memory
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual != expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().Be(expected); // functionally equal
</pre></td>
<td><pre lang="csharp">
actual.Should().BeSameAs(expected) // refer to the exact same object in memory
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual > expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeGreaterThan(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual > expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeLessOrEqualTo(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual >= expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeGreaterOrEqualTo(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual >= expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeLessThan(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual < expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeLessThan(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual < expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeGreaterOrEqualTo(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(actual <= expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeLessOrEqualTo(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(actual <= expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeGreaterThan(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## StringAssert
<table><tr>
<td><pre lang="csharp">
StringAssert.Contains(actual, expectedSubstring);
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(expectedSubstring);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.StartWith(actual, expectedPrefix);
</pre></td>
<td><pre lang="csharp">
actual.Should().StartWith(expectedPrefix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.EndsWith(actual, expectedSuffix);
</pre></td>
<td><pre lang="csharp">
actual.Should().EndWith(expectedSuffix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.Matches(actual, expectedPattern);
</pre></td>
<td><pre lang="csharp">
actual.Should().MatchRegex(expectedPattern);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.DoesNotMatch(actual, expectedPattern);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotMatchRegex(expectedPattern);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## CollectionAssert
<table><tr>
<td><pre lang="csharp">
CollectionAssert.AllItemsAreUnique(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().OnlyHaveUniqueItems();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreEqual(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().Equal(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreNotEqual(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotEqual(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreEquivalent(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeEquivalentTo(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreNotEquivalent(expected, actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeEquivalentTo(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.Contains(actual, expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.DoesNotContain(actual, expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.IsSubsetOf(actual, expectedSuperset);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeSubsetOf(expectedSuperset);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.IsNotSubsetOf(actual, expectedSuperset);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeSubsetOf(expectedSuperset);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AllItemsAreNotNull(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContainNulls();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(T));
</pre></td>
<td><pre lang="csharp">
actual.Should().ContainItemsAssignableTo&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## [ExpectedException]
<table><tr>
<td><pre lang="csharp">
[ExpectedException(typeof(InvalidOperationException))]
public void MyTest()
{
    func();
}
</pre></td>
<td><pre lang="csharp">
public void MyTest()
{
    Action act = () => func();

    act.ShouldThrowExactly<InvalidOperationException>();
}
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
[ExpectedException(typeof(SystemException), AllowDerivedTypes = true)]
public void MyTest()
{
    func();
}
</pre></td>
<td><pre lang="csharp">
public void MyTest()
{
    Action act = () => func();

    act.ShouldThrow<InvalidOperationException>();
}
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
[ExpectedException(typeof(InvalidOperationException))]
public void MyTest()
{
    try
    {
        func();
    }
    catch (InvalidOperationException ex)
    {
        ex.Message.Should().Be(errorMessage);
        throw;
    }
}
</pre></td>
<td><pre lang="csharp">
public void MyTest()
{
    Action act = () => func();

    act.ShouldThrowExactly<InvalidOperationException>()
        .WithMessage(errorMessage);
}
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
public void MyTest()
{
    Action act = () => func();

    act.ShouldThrowExactly<InvalidOperationException>()
        .Which.Message.Should().Contain("expectedMessage");
}
</pre></td>
<td><pre lang="csharp">
public void MyTest()
{
    Action act = () => func();

    act.ShouldThrowExactly<InvalidOperationException>()
        .WithMessage("*expectedMessage*"); // wildcards
}
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

# Best Practices

## Numerics / Comparable
<table><tr>
<td><pre lang="csharp">
actual.Should().BeGreaterThan(0);
</pre></td>
<td><pre lang="csharp">
actual.Should().BePositive();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().BeLessThan(0);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeNegative();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(lower <= actual && actual <= upper).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().BeInRange(lower, upper);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## Types
<table><tr>
<td><pre lang="csharp">
actual.GetType().Should().Be(typeof(T));
</pre></td>
<td><pre lang="csharp">
actual.Should().BeOfType&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.GetType().Should().NotBe(typeof(T));
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeOfType&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(actual is T).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().BeAssignableTo&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(actual as T).Should().NotBeNull();
</pre></td>
<td><pre lang="csharp">
actual.Should().BeAssignableTo&lt;T&gt;();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## Nullables
<table><tr>
<td><pre lang="csharp">
actuak.HasValue.Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveValue();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.HasValue.Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotHaveValue();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## Enumerables
<table><tr>
<td><pre lang="csharp">
actual.Any().Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Any().Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().BeEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Any(x => x.SomeProperty).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Any(x => x.SomeProperty).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.All(x => x.SomeProperty).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().OnlyContain(x => x.SomeProperty) // now fails on empty collection;
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Contains(expected).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Contains(unexpected).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(unexpected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Any(x => x == unexpected).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(unexpected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().Be(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCount(k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeGreaterThan(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCount(c => c > k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeGreaterOrEqualTo(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCount(c => c >= k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeLessThan(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCount(c => c < k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeLessOrEqualTo(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCount(c => c <= k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().HaveCount(1);
</pre></td>
<td><pre lang="csharp">
actual.Should().ContainSingle();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().HaveCount(0);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().HaveCount(expected.Count());
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveSameCount(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Where(x => x.SomeProperty).Should().NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Where(x => x.SomeProperty).Should().BeEmpty();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Where(x => x.SomeProperty).Should().HaveCount(1);
</pre></td>
<td><pre lang="csharp">
actual.Should().ContainSingle(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().OnlyContain(e => !e.SomeProperty);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().NotBeNull().And.NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.ElementAt(k).Should().Be(expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveElementAt(k, expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Skip(k).First().Should().Be(expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveElementAt(k, expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.OrderBy(x => x.SomeProperty).Should().Equal(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeInAscendingOrder(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.OrderByDescending(x => x.SomeProperty).Should().Equal(actual);
</pre></td>
<td><pre lang="csharp">
actual.Should().BeInDescendingOrder(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Select(e1 => e1.SomeProperty).Should().Equal(expected.Select(e2 => e2.SomeProperty));
</pre></td>
<td><pre lang="csharp">
actual.Should().Equal(expected, (e1, e2) => e1.SomeProperty == e2.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Intersect(expected).Should().BeEmpty();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotIntersectWith(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Intersect(expected).Should().NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
actual.Should().IntersectWith(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Select(x => x.SomeProperty).Should().NotContainNulls();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContain(x => x.SomeProperty == null); // Debatable
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().HaveSameCount(actual.Distinct());
</pre></td>
<td><pre lang="csharp">
actual.Should().OnlyHaveUniqueItems();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.FirstOrDefault().Should().BeNull(); // From the assertion it is unclear what is being tested.
</pre></td>
<td><pre lang="csharp">
actual.Should().BeEmpty(); // The enumerable is empty.
</pre></td>
<td><pre lang="csharp">
actual.First().Should().BeNull(); // The first element is null.
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
<td>
</td>
</tr></table>

## Lists
<table><tr>
<td><pre lang="csharp">
actual[k].Should().Be(expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveElementAt(k, expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## Dictionaries
<table><tr>
<td><pre lang="csharp">
actual.ContainsKey(expected).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().ContainKey(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.ContainsKey(expected).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContainKey(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.ContainsValue(expected).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().ContainValue(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.ContainsValue(expected).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContainValue(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().ContainKey(expectedKey).And.ContainValue(expectedValue);
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(expectedKey, expectedValue);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().ContainKey(expected.Key).And.ContainValue(expected.Value);
</pre></td>
<td><pre lang="csharp">
actual.Should().Contain(expected);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## Strings
<table><tr>
<td><pre lang="csharp">
actual.StartsWith(expectedPrefix).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().StartWith(expectedPrefix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.EndsWith(expectedSuffix).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().EndWith(expectedSuffix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().NotBeNull().And.NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.IsNullOrEmpty(actual).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().BeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.IsNullOrEmpty(actual).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.IsNullOrWhiteSpace(actual).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().BeNullOrWhiteSpace();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.IsNullOrWhiteSpace(actual).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeNullOrWhiteSpace();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Should().HaveCount(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveLength(k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>
