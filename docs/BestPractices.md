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
Assert.IsTrue failed.
</td>
<td>
Expected True, but found False.
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
Assert.IsFalse failed.
</td>
<td>
Expected False, but found True.
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
Assert.IsNull failed.
</td>
<td>
Expected object to be <null>, but found System.Object (HashCode=51904525).
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
Assert.IsNotNull failed.
</td>
<td>
Expected object not to be <null>.
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
Assert.AreEqual failed. Expected:<SomeProperty: 2, OtherProperty: expected>. Actual:<SomeProperty: 1, OtherProperty: actual>.
</td>
<td>
Expected object to be SomeProperty: 2, OtherProperty: expected, but found SomeProperty: 1, OtherProperty: actual.
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
Assert.AreEqual failed. Expected a difference no greater than <0.5> between expected value <2> and actual value <1.25>.
</td>
<td>
Expected value 1.25 to approximate 2.0 +/- 0.5, but it differed by 0.75.
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
Assert.AreNotEqual failed. Expected any value except:<SomeProperty: 1, OtherProperty: expected>. Actual:<SomeProperty: 1, OtherProperty: expected>.
</td>
<td>
Did not expect object to be equal to SomeProperty: 1, OtherProperty: expected.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
Assert.AreNotEqual(expected, actual, delta);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeApproximately(expected, delta); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Assert.AreNotEqual failed. Expected a difference greater than <0.5> between expected value <2> and actual value <2>.
</td>
<td>
Expected value 2.0 to not approximate 2.0 +/- 0.5, but it only differed by 0.0.
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
Assert.AreSame failed.
</td>
<td>
Expected object to refer to 
SomeProperty: 1, OtherProperty: actual, but found 
SomeProperty: 1, OtherProperty: actual.
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
Assert.AreNotSame failed.
</td>
<td>
Did not expect reference to object SomeProperty: 1, OtherProperty: actual.
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
Assert.IsInstanceOfType failed.  Expected type:<UnitTests2.MyIdenticalClass>. Actual type:<UnitTests2.MyClass>.
</td>
<td>
Expected type to be UnitTests2.MyIdenticalClass, but found UnitTests2.MyClass.
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
Assert.IsNotInstanceOfType failed. Wrong Type:<UnitTests2.MyClass>. Actual type:<UnitTests2.MyClass>.
</td>
<td>
Expected type not to be [UnitTests2.MyClass, UnitTests2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null], but it is.
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
Assert.IsTrue failed.
</td>
<td>
Expected value to be 2, but found 1.
</td>
<td>
Expected object to refer to 
SomeProperty: 2, OtherProperty: expected, but found 
SomeProperty: 1, OtherProperty: actual.
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
Assert.IsFalse failed.
</td>
<td>
Did not expect 1.
</td>
<td>
Did not expect reference to object 
SomeProperty: 1, OtherProperty: expected.
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
Assert.IsTrue failed.
</td>
<td>
Did not expect 1.
</td>
<td>
Did not expect reference to object 
SomeProperty: 1, OtherProperty: expected.
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
Assert.IsFalse failed.
</td>
<td>
Expected value to be 2, but found 1.
</td>
<td>
Expected object to refer to 
SomeProperty: 2, OtherProperty: expected, but found 
SomeProperty: 1, OtherProperty: actual.
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
Assert.IsTrue failed.
</td>
<td>
Expected a value greater than 2, but found 1.
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
Assert.IsFalse failed.
</td>
<td>
Expected a value less or equal to 1, but found 2.
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
Assert.IsTrue failed.
</td>
<td>
Expected a value greater or equal to 2, but found 1.
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
Assert.IsFalse failed.
</td>
<td>
Expected a value less than 1, but found 2.
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
Assert.IsTrue failed.
</td>
<td>
Expected a value less than 1, but found 2.
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
Assert.IsFalse failed.
</td>
<td>
Expected a value greater or equal to 2, but found 1.
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
Assert.IsTrue failed.
</td>
<td>
Expected a value less or equal to 1, but found 2.
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
Assert.IsFalse failed.
</td>
<td>
Expected a value greater than 2, but found 1.
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
StringAssert.Contains failed. String 'SomeLongString' does not contain string 'Short'. .
</td>
<td>
Expected string "SomeLongString" to contain "Short".
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
StringAssert.StartsWith failed. String 'LongString' does not start with string 'Short'. .
</td>
<td>
Expected string to start with 
"Short", but 
"LongString" differs near "Lon" (index 0).
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
StringAssert.EndsWith failed. String 'StringLong' does not end with string 'Short'. .
</td>
<td>
Expected string "StringLong" to end with "Short".
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
StringAssert.Matches failed. String 'SomeLong' does not match pattern '.*String.*'. .
</td>
<td>
Expected string to match regex 
".*String.*", but 
"SomeLong" does not match.
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
StringAssert.DoesNotMatch failed. String 'SomeStringLong' matches pattern '.*String.*'. .
</td>
<td>
Did not expect string to match regex 
".*String.*", but 
"SomeStringLong" matches.
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
CollectionAssert.AllItemsAreUnique failed. Duplicate item found:<SomeProperty: 1, OtherProperty: item>.
</td>
<td>
Expected collection to only have unique items, but item SomeProperty: 1, OtherProperty: item is not unique.
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
CollectionAssert.AreEqual failed. (Element at index 0 do not match.)
</td>
<td>
Expected collection to be equal to {SomeProperty: 1, OtherProperty: item}, but {SomeProperty: 1, OtherProperty: different} differs at index 0.
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
CollectionAssert.AreNotEqual failed. (Both collection contain same elements.)
</td>
<td>
Did not expect collections {SomeProperty: 1, OtherProperty: item} and {SomeProperty: 1, OtherProperty: item} to be equal.
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
CollectionAssert.AreEquivalent failed. The expected collection contains 1 occurrence(s) of <SomeProperty: 2, OtherProperty: other>. The actual collection contains 0 occurrence(s).
</td>
<td>
Expected collection {SomeProperty: 1, OtherProperty: item, SomeProperty: 2, OtherProperty: item} to be equivalent to {SomeProperty: 1, OtherProperty: item, SomeProperty: 2, OtherProperty: other}, but it misses {SomeProperty: 2, OtherProperty: other}.
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
CollectionAssert.AreNotEquivalent failed. Both collections contain the same elements.
</td>
<td>
Expected collection {SomeProperty: 1, OtherProperty: item, SomeProperty: 2, OtherProperty: other} not be equivalent with collection {SomeProperty: 1, OtherProperty: item, SomeProperty: 2, OtherProperty: other}.
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
CollectionAssert.Contains failed.
</td>
<td>
Expected collection {SomeProperty: 2, OtherProperty: other} to contain SomeProperty: 1, OtherProperty: item.
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
CollectionAssert.DoesNotContain failed.
</td>
<td>
Expected collection {SomeProperty: 1, OtherProperty: item} to not contain SomeProperty: 1, OtherProperty: item.
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
CollectionAssert.IsSubsetOf failed.
</td>
<td>
Expected collection to be a subset of {SomeProperty: 1, OtherProperty: item}, but items {SomeProperty: 2, OtherProperty: other} are not part of the superset.
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
CollectionAssert.IsNotSubsetOf failed.
</td>
<td>
Did not expect collection {SomeProperty: 1, OtherProperty: item} to be a subset of {SomeProperty: 1, OtherProperty: item}.
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
CollectionAssert.AllItemsAreNotNull failed.
</td>
<td>
Expected collection not to contain nulls, but found one at index 0.
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
CollectionAssert.AllItemsAreInstancesOfType failed. Element at index 0 is not of expected type. Expected type:<UnitTests2.MyIdenticalClass>. Actual type:<UnitTests2.MyClass>.
</td>
<td>
Expected collection to contain only items of type UnitTests2.MyIdenticalClass, but item SomeProperty: 1, OtherProperty: item at index 0 is of type UnitTests2.MyClass.
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
Test method threw exception System.InvalidCastException, but exception System.ArgumentNullException was expected. Exception System.InvalidCastException: Specified cast is not valid.
</td>
<td>
Expected a <System.ArgumentNullException> to be thrown, but found a <System.InvalidCastException>: System.InvalidCastException with message "Specified cast is not valid."
     at UnitTests2.ExceptionTests.<>c.<Snippet01_New>b__1_0() in C:\Path\To\UnitTests\ExceptionTests.cs:line 31
     at UnitTests2.ExceptionTests.<>c__DisplayClass1_0.<Snippet01_New>b__1() in C:\Path\To\UnitTests\ExceptionTests.cs:line 34
     at FluentAssertions.Specialized.ActionAssertions.InvokeSubjectWithInterception()
.
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
Test method threw exception System.InvalidCastException, but exception System.ArgumentException or a type derived from it was expected. Exception System.InvalidCastException: Specified cast is not valid.
</td>
<td>
Expected a <System.ArgumentException> to be thrown, but found a <System.InvalidCastException>: System.InvalidCastException with message "Specified cast is not valid."
     at UnitTests2.ExceptionTests.<>c.<Snippet02_New>b__3_0() in C:\Path\To\UnitTests\ExceptionTests.cs:line 57
     at UnitTests2.ExceptionTests.<>c__DisplayClass3_0.<Snippet02_New>b__1() in C:\Path\To\UnitTests\ExceptionTests.cs:line 60
     at FluentAssertions.Specialized.ActionAssertions.InvokeSubjectWithInterception()
.
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
Expected string to be 
"expectedMessage" with a length of 15, but 
"actualMessage" has a length of 13.
</td>
<td>
Expected exception message to match the equivalent of 
"expectedMessage", but 
"actualMessage" does not.
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
Expected string "Problems, errorCode2 and more Problems" to contain "errorCode1".
</td>
<td>
Expected exception message to match the equivalent of 
"*errorCode1*", but 
"Problems, errorCode2 and more Problems" does not.
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
Expected a value greater than 0, but found -1.
</td>
<td>
Expected positive value, but found -1
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
Expected a value less than 0, but found 1.
</td>
<td>
Expected negative value, but found 1
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
Expected True, but found False.
</td>
<td>
Expected value to be between 1 and 5, but found 6.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(lower <= actual && actual <= upper).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeInRange(lower, upper); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected False, but found True.
</td>
<td>
Expected value to not be between 1 and 5, but found 4.
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
Expected type to be UnitTests2.MyClass, but found UnitTests2.MyIdenticalClass.
</td>
<td>
Expected type to be UnitTests2.MyClass, but found UnitTests2.MyIdenticalClass.
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
Expected type not to be [UnitTests2.MyClass, UnitTests2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null], but it is.
</td>
<td>
Expected type not to be [UnitTests2.MyClass, UnitTests2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null], but it is.
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
Expected True, but found False.
</td>
<td>
Expected object to be assignable to UnitTests2.MyClass, but UnitTests2.MyIdenticalClass is not.
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
Expected object not to be <null>.
</td>
<td>
Expected object to be assignable to UnitTests2.MyClass, but UnitTests2.MyIdenticalClass is not.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(actual is T).Should().BeFalse();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeAssignableTo&lt;T&gt;(); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected False, but found True.
</td>
<td>
Expected object to not be assignable to UnitTests2.MyClass, but UnitTests2.MyClass is.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
(actual as T).Should().BeNull();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeAssignableTo&lt;T&gt;(); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected object to be <null>, but found SomeProperty: 1, OtherProperty: actual.
</td>
<td>
Expected object to not be assignable to UnitTests2.MyClass, but UnitTests2.MyClass is.
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
Expected True, but found False.
</td>
<td>
Expected a value.
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
Expected False, but found True.
</td>
<td>
Did not expect a value, but found 1.
</td>
</tr></table>


## Collections
<table><tr>
<td><pre lang="csharp">
actual.Any().Should().BeTrue();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotBeEmpty();
</pre></td>
</tr><tr>
<td>
Expected True, but found False.
</td>
<td>
Expected collection not to be empty.
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
Expected False, but found True.
</td>
<td>
Expected collection to be empty, but found {SomeProperty: 0, OtherProperty: }.
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
Expected True, but found False.
</td>
<td>
Collection {SomeProperty: 0, OtherProperty: Actual} should have an item matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass5_0).expectedValue).
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
Expected False, but found True.
</td>
<td>
Expected Collection {SomeProperty: 0, OtherProperty: Unexpected} to not have any items matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass7_0).unexpectedValue).
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
Expected True, but found False.
</td>
<td>
Expected collection to contain only items matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass9_0).expectedValue), but {SomeProperty: 0, OtherProperty: Actual} do(es) not match.
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
Expected True, but found False.
</td>
<td>
Expected collection {"Actual"} to contain "Expected".
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
Expected False, but found True.
</td>
<td>
Expected collection {"Expected"} to not contain "Expected".
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
Expected False, but found True.
</td>
<td>
Expected collection {"Unexpected"} to not contain "Unexpected".
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
Expected value to be 2, but found 3.
</td>
<td>
Expected collection to contain 2 item(s), but found 3.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeGreaterThan(k);
</pre></td>
actual.Should().HaveCountGreaterThan(k); // Will be available in Fluent Assertions 5.0
<td><pre lang="csharp">
</pre></td>
</tr><tr>
<td>
Expected a value greater than 4, but found 3.
</td>
<td>
Expected collection to contain more than 4 item(s), but found 3.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeGreaterOrEqualTo(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCountGreaterOrEqualTo(k); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected a value greater or equal to 4, but found 3.
</td>
<td>
Expected collection to contain at least 4 item(s), but found 3.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeLessThan(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCountLessThan(k); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected a value less than 2, but found 3.
</td>
<td>
Expected collection to contain fewer than 2 item(s), but found 3.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().BeLessOrEqualTo(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveCountLessOrEqualTo(k); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected a value less or equal to 2, but found 3.
</td>
<td>
Expected collection to contain at most 2 item(s), but found 3.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().NotBe(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().NotHaveCount(k); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Did not expect 3.
</td>
<td>
Expected collection to not contain 3 item(s), but found 3.
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
Expected collection to contain 1 item(s), but found 3.
</td>
<td>
Expected collection to contain a single item, but found {"", "", ""}.
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
Expected collection to contain 0 item(s), but found 3.
</td>
<td>
Expected collection to be empty, but found {"", "", ""}.
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
Expected collection to contain 2 item(s), but found 3.
</td>
<td>
Expected collection to have 2 item(s), but found 3.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Count().Should().NotBe(unexpected.Count());
</pre></td>
<td><pre lang="csharp">
actual.Should().NotHaveSameCount(unexpected); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Did not expect 2.
</td>
<td>
Expected collection to not have 2 item(s), but found 2.
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
Expected collection not to be empty.
</td>
<td>
Collection {SomeProperty: 0, OtherProperty: Actual} should have an item matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass39_0).expectedValue).
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
Expected collection to be empty, but found {SomeProperty: 0, OtherProperty: Expected}.
</td>
<td>
Expected Collection {SomeProperty: 0, OtherProperty: Expected} to not have any items matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass41_0).expectedValue).
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
Expected collection to contain 1 item(s), but found 0.
</td>
<td>
Expected collection to contain a single item matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass43_0).expectedValue), but no such item was found.
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
Expected collection to contain only items matching (x.OtherProperty != value(UnitTests2.CollectionTests+<>c__DisplayClass44_0).unexpectedValue), but {SomeProperty: 0, OtherProperty: Unexpected} do(es) not match.
</td>
<td>
Expected Collection {SomeProperty: 0, OtherProperty: Unexpected} to not have any items matching (x.OtherProperty == value(UnitTests2.CollectionTests+<>c__DisplayClass45_0).unexpectedValue).
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
Expected collection not to be empty.
</td>
<td>
Expected collection not to be empty.
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
Expected string to be 
"Expected" with a length of 8, but 
"Unexpected" has a length of 10.
</td>
<td>
Expected "Expected" at index 0, but found "Unexpected".
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual[k].Should().Be(expected);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveElementAt(k, expected);
</pre></td>
</tr><tr>
<td>
Expected string to be 
"Expected" with a length of 8, but 
"Unexpected" has a length of 10.
</td>
<td>
Expected "Expected" at index 0, but found "Unexpected".
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
Expected string to be 
"Expected" with a length of 8, but 
"Unexpected" has a length of 10.
</td>
<td>
Expected "Expected" at index 1, but found "Unexpected".
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
Expected collection to be equal to {SomeProperty: 2, OtherProperty: , SomeProperty: 1, OtherProperty: }, but {SomeProperty: 1, OtherProperty: , SomeProperty: 2, OtherProperty: } differs at index 0.
</td>
<td>
Expected collection {SomeProperty: 2, OtherProperty: , SomeProperty: 1, OtherProperty: } to be ordered" by SomeProperty" and result in {SomeProperty: 1, OtherProperty: , SomeProperty: 2, OtherProperty: }.
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
Expected collection to be equal to {SomeProperty: 1, OtherProperty: , SomeProperty: 2, OtherProperty: }, but {SomeProperty: 2, OtherProperty: , SomeProperty: 1, OtherProperty: } differs at index 0.
</td>
<td>
Expected collection {SomeProperty: 1, OtherProperty: , SomeProperty: 2, OtherProperty: } to be ordered" by SomeProperty" and result in {SomeProperty: 2, OtherProperty: , SomeProperty: 1, OtherProperty: }.
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
Expected collection to be equal to {"Expected", "Expected"}, but {"Actual", "Actual"} differs at index 0.
</td>
<td>
Expected collection to be equal to {SomeProperty: 1, OtherProperty: Expected, SomeProperty: 2, OtherProperty: Expected}, but {SomeProperty: 1, OtherProperty: Actual, SomeProperty: 2, OtherProperty: Actual} differs at index 0.
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
Expected collection to be empty, but found {SomeProperty: 1, OtherProperty: Expected}.
</td>
<td>
Did not expect collection to intersect with {SomeProperty: 1, OtherProperty: Expected}, but found the following shared items {SomeProperty: 1, OtherProperty: Expected}.
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
Expected collection not to be empty.
</td>
<td>
Expected collection to intersect with {SomeProperty: 1, OtherProperty: Expected}, but {SomeProperty: 1, OtherProperty: Actual} does not contain any shared items.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Select(x => x.SomeProperty).Should().NotContainNulls();
</pre></td>
<td><pre lang="csharp">
actual.Should().NotContainNulls(e => e.OtherProperty); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected collection not to contain nulls, but found one at index 0.
</td>
<td>
Expected collection not to contain <null>s on e.OtherProperty, but found {SomeProperty: 1, OtherProperty: }.
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
Expected collection to have 1 item(s), but found 2.
</td>
<td>
Expected collection to only have unique items, but item SomeProperty: 1, OtherProperty:  is not unique.
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Select(x => x.SomeProperty).Should().OnlyHaveUniqueItems();
</pre></td>
<td><pre lang="csharp">
actual.Should().OnlyHaveUniqueItems(x => x.SomeProperty); // Will be available in Fluent Assertions 5.0
</pre></td>
</tr><tr>
<td>
Expected collection to only have unique items, but item 1 is not unique.
</td>
<td>
Expected collection to only have unique items on x.SomeProperty, but item SomeProperty: 1, OtherProperty:  is not unique.
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
actual.Should().HaveElementAt(0, null); // The first element is null.
</pre></td>
</tr><tr>
<td>
Expected string to be <null>, but found "".
</td>
<td>
Expected collection to be empty, but found {""}.
</td>
<td>
Expected <null> at index 0, but found "".
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
Expected True, but found False.
</td>
<td>
Expected dictionary {[0, ]} to contain key 1.
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
Expected False, but found True.
</td>
<td>
Dictionary {[0, ]} should not contain key 0, but found it anyhow.
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
Expected True, but found False.
</td>
<td>
Expected dictionary {[0, ]} to contain value "expected".
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
Expected False, but found True.
</td>
<td>
Dictionary {[0, expected]} should not contain value "expected", but found it anyhow.
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
Expected dictionary {[0, ]} to contain value "expected".
</td>
<td>
Expected dictionary to contain value "expected" at key 0, but found "".
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
Expected dictionary {[0, ]} to contain value "expected".
</td>
<td>
Expected dictionary to contain value "expected" at key 0, but found "".
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
Expected True, but found False.
</td>
<td>
Expected string to start with 
"Expected", but 
"ActualString" differs near "Act" (index 0).
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
Expected True, but found False.
</td>
<td>
Expected string "ActualString" to end with "Expected".
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
Did not expect empty string.
</td>
<td>
Expected string not to be <null> or empty, but found "".
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
Expected True, but found False.
</td>
<td>
Expected string to be <null> or empty, but found "Actual".
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
Expected False, but found True.
</td>
<td>
Expected string not to be <null> or empty, but found "".
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
Expected True, but found False.
</td>
<td>
Expected string to be <null> or whitespace, but found "Actual".
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
Expected False, but found True.
</td>
<td>
Expected string not to be <null> or whitespace, but found " ".
</td>
</tr></table>

<table><tr>
<td><pre lang="csharp">
actual.Length.Should().Be(k);
</pre></td>
<td><pre lang="csharp">
actual.Should().HaveLength(k);
</pre></td>
</tr><tr>
<td>
Expected value to be 4, but found 6.
</td>
<td>
Expected string with length 4, but found string "Actual" with length 6.
</td>
</tr></table>
