# Refactorings
This is a collection of refactorings to improve the usage of Fluent Assertions.

If your favourite refactoring is missing, please consider to submit it.

# MSTest

## Assert
<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(theBoolean);
</pre></td>
<td><pre lang="csharp">
theBoolean.Should().BeTrue();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(theBoolean);
</pre></td>
<td><pre lang="csharp">
theBoolean.Should().BeFalse();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsNull(theObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeNull();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsNotNull(theObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBeNull();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreEqual(otherObject, theObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().Be(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreEqual(otherNumeric, theNumeric, delta);
</pre></td>
<td><pre lang="csharp">
theNumeric.Should().BeApproximately(otherNumeric, delta);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreNotEqual(otherObject, theObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBe(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreSame(otherObject, theObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeSameAs(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreNotSame(otherObject, theObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBeSameAs(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsInstanceOfType(theObject, typeof(T));
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeOfType<T>();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsNotInstanceOfType(theObject, typeof(T));
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBeOfType<T>();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(theObject == otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().Be(otherObject); // functionally equal
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeSameAs(otherObject); // refer to the exact same object in memory
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
Assert.IsFalse(theObject == otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBe(otherObject); // functionally unequal
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBeSameAs(otherObject); // refer to the different object in memory
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
Assert.IsTrue(theObject != otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBe(otherObject); // functionally unequal
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBeSameAs(otherObject); // refer to the different object in memory
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
Assert.IsFalse(theObject != otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().Be(otherObject); // functionally equal
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeSameAs(otherObject) // refer to the exact same object in memory
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
Assert.IsTrue(theObject > otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeGreaterThan(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(theObject > otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeLessOrEqualTo(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(theObject >= otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeGreaterOrEqualTo(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(theObject >= otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeLessThan(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(theObject < otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeLessThan(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(theObject < otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeGreaterOrEqualTo(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsTrue(theObject <= otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeLessOrEqualTo(otherObject);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.IsFalse(theObject <= otherObject);
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeGreaterThan(otherObject);
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
StringAssert.Contains(theString, substr);
</pre></td>
<td><pre lang="csharp">
theString.Should().Contain(substr);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.StartWith(theString, prefix);
</pre></td>
<td><pre lang="csharp">
theString.Should().StartWith(prefix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.EndsWith(theString, suffix);
</pre></td>
<td><pre lang="csharp">
theString.Should().EndWith(suffix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.Matches(theString, pattern);
</pre></td>
<td><pre lang="csharp">
theString.Should().MatchRegex(pattern);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
StringAssert.DoesNotMatch(theString, pattern);
</pre></td>
<td><pre lang="csharp">
theString.Should().NotMatchRegex(pattern);
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
CollectionAssert.AllItemsAreUnique(theCollection);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().OnlyHaveUniqueItems();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreEqual(otherCollection, theCollection);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().Equal(otherCollection);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreNotEqual(otherCollection, theCollection);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().NotEqual(otherCollection);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreEquivalent(otherCollection, theCollection);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().BeEquivalentTo(otherCollection);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AreNotEquivalent(otherCollection, theCollection);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().NotBeEquivalentTo(otherCollection);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.Contains(theCollection, element);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().Contain(element);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.DoesNotContain(theCollection, element);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().NotContain(element);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.IsSubsetOf(subset, superset);
</pre></td>
<td><pre lang="csharp">
subset.Should().BeSubset(superset);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.IsNotSubsetOf(subset, superset);
</pre></td>
<td><pre lang="csharp">
subset.Should().NotBeSubset(superset);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AllItemsAreNotNull(theCollection);
</pre></td>
<td><pre lang="csharp">
theCollection.Should().NotContainNulls();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
CollectionAssert.AllItemsAreInstancesOfType(theCollection, typeof(T));
</pre></td>
<td><pre lang="csharp">
theCollection.ContainItemsAssignableTo<T>();
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
        throw ex;
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
        .Which.Message.Should().Contain(errorMessage);
}
</pre></td>
<td><pre lang="csharp">
public void MyTest()
{
    Action act = () => func();

    act.ShouldThrowExactly<InvalidOperationException>()
        .WithMessage(*errorMessage*); // wildcards
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
theInt.Should().BeGreaterThan(0);
</pre></td>
<td><pre lang="csharp">
theInt.Should().BePositive();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theInt.Should().BeLessThan(0);
</pre></td>
<td><pre lang="csharp">
theInt.Should().BeNegative();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(lower <= theInt && theInt <= upper).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theInt.Should().BeInRange(lower, upper);
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
theObject.GetType().Should().Be(typeof(T));
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeOfType<T>();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theObject.GetType().Should().NotBe(typeof(T));
</pre></td>
<td><pre lang="csharp">
theObject.Should().NotBeOfType<T>();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(theObject is T).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeAssignableTo<T>();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(theObject as T).Should().NotBeNull();
</pre></td>
<td><pre lang="csharp">
theObject.Should().BeAssignableTo<T>();
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
(struct?).HasValue().Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
(struct?).Should().HaveValue();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(struct?).HasValue().Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
(struct?).Should().NotHaveValue();
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
theEnumerable.Any().Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotBeEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Any().Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().BeEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Any(x => x.SomeProperty).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().Contain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Any(x => x.SomeProperty).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotContain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.All(x => x.SomeProperty).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().OnlyContain(x => x.SomeProperty) // now fails on empty collection;
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Contains(e).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().Contain(e);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Contains(e).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotContain(e);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Any(e).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotContain(e);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Count().Should().Be(k);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Count().Should().BeGreaterThan(k);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(c => c > k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Count().Should().BeGreaterOrEqualTo(k);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(c => c >= k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Count().Should().BeLessThan(k);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(c => c < k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Count().Should().BeLessOrEqualTo(k);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(c => c <= k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(1);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().ContainSingle();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Should().HaveCount(0);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().BeEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Contains(element).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().Contain(element);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Contains(element).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotContain(element);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Where(x => x.SomeProperty).Should().NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().Contain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Where(x => x.SomeProperty).Should().BeEmpty();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotContain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Where(x => x.SomeProperty).Should().HaveCount(1);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().ContainSingle(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Should().OnlyContain(e => !e.SomeProperty);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotContain(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Should().NotBeNull().And.NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotBeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.ElementAt(k).Should().Be(element);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveElementAt(k, element);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Skip(k).First().Should().Be(element);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().HaveElementAt(k + 1, element);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.OrderBy(x => x.SomeProperty).Should().Equal(theEnumerable);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().BeInAscendingOrder(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.OrderByDescending(x => x.SomeProperty).Should().Equal(theEnumerable);
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().BeInDescendingOrder(x => x.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Select(e1 => e1.SomeProperty).Equal(otherEnumerable.Select(e2 => e2.SomeProperty)) => theEnumerable.Should().Equal(otherEnumerable, (e1, e2);
</pre></td>
<td><pre lang="csharp">
e1.SomeProperty == e2.SomeProperty);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Intersect(otherEnumerable).Should().BeEmpty();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().NotIntersectWith(otherEnumerable);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Intersect(otherEnumerable).Should().NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Should().IntersectWith(otherEnumerable);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theEnumerable.Select(x => x.SomeProperty).Should().NotContainNulls();
</pre></td>
<td><pre lang="csharp">
theEnumerable.Select(e).Should().NotContain(x => x.SomeProperty == null) // Debatable;
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>


## Lists
<table><tr>
<td><pre lang="csharp">
theList[k].Should().Be(element);
</pre></td>
<td><pre lang="csharp">
theList.Should().HaveElementAt(k, element);
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
theDictionary.ContainsKey(key).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theDictionary.Should().ContainKey(key);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theDictionary.ContainsKey(key).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theDictionary.Should().NotContainKey(key);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theDictionary.ContainsValue(value).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theDictionary.Should().ContainValue(value);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theDictionary.ContainsValue(value).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theDictionary.Should().NotContainValue(value);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theDictionary.Should().ContainKey(key).And.ContainValue(value);
</pre></td>
<td><pre lang="csharp">
theDictionary.Should().Contain(key, value);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theDictionary.Should().ContainKey(KeyValuePair.Key).And.ContainValue(KeyValuePair.value);
</pre></td>
<td><pre lang="csharp">
theDictionary.Should().Contain(KeyValuePair);
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
theString.StartsWith(prefix).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theString.Should().StartWith(prefix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theString.EndsWith(suffix).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theString.Should().EndWith(suffix);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theString.Should().NotBeNull().And.NotBeEmpty();
</pre></td>
<td><pre lang="csharp">
theString.Should().NotBeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.NullOrEmpty(theString).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theString.Should().BeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.NullOrEmpty(theString).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theString.Should().NotBeNullOrEmpty();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.NullOrWhiteSpace(theString).Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
theString.Should().BeNullOrWhiteSpace();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
string.NullOrWhiteSpace(theString).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
theString.Should().NotBeNullOrWhiteSpace();
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
theString.Should().HaveCount(k);
</pre></td>
<td><pre lang="csharp">
theString.Should().HaveLength(k);
</pre></td>
</tr><tr>
<td>
</td>
<td>
</td>
</tr></table>
