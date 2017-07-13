# MSTest

## Assert
```csharp
Assert.IsTrue(theBoolean) => theBoolean.Should().BeTrue();
```

```csharp
Assert.IsFalse(theBoolean) => theBoolean.Should().BeFalse();
```

```csharp
Assert.IsNull(theObject) => theObject.Should().BeNull()
```

```csharp
Assert.IsNotNull(theObject) => theObject.Should().NotBeNull()
```

```csharp
Assert.AreEqual(otherObject, theObject) => theObject.Should().Be(otherObject)
```

```csharp
Assert.AreEqual(otherNumeric, theNumeric, delta) => theNumeric.Should().BeApproximately(otherNumeric, delta)
```

```csharp
Assert.AreNotEqual(otherObject, theObject) => theObject.Should().NotBe(otherObject)
```

```csharp
Assert.AreSame(otherObject, theObject) => theObject.Should().BeSameAs(otherObject)
```

```csharp
Assert.AreNotSame(otherObject, theObject) => theObject.Should().NotBeSameAs(otherObject)
```

```csharp
Assert.IsInstanceOfType(theObject, typeof(T)) => theObject.Should().BeOfType<T>()
```

```csharp
Assert.IsNotInstanceOfType(theObject, typeof(T)) => theObject.Should().NotBeOfType<T>()
```

```csharp
Assert.IsTrue(theObject == otherObject) 
    => theObject.Should().Be(otherObject) // functionally equal
    => theObject.Should().BeSameAs(otherObject) // refer to the exact same object in memory
```

```csharp
Assert.IsFalse(theObject == otherObject)
     => theObject.Should().NotBe(otherObject) // functionally unequal
     => theObject.Should().NotBeSameAs(otherObject) // refer to the different object in memory
```

```csharp
Assert.IsTrue(theObject != otherObject)
     => theObject.Should().NotBe(otherObject) // functionally unequal
     => theObject.Should().NotBeSameAs(otherObject) // refer to the different object in memory
```

```csharp
Assert.IsFalse(theObject != otherObject)
    => theObject.Should().Be(otherObject) // functionally equal
    => theObject.Should().BeSameAs(otherObject) // refer to the exact same object in memory
```

```csharp
Assert.IsTrue(theObject > otherObject) => theObject.Should().BeGreaterThan(otherObject)
```

```csharp
Assert.IsFalse(theObject > otherObject) => theObject.Should().BeLessOrEqualTo(otherObject)
```

```csharp
Assert.IsTrue(theObject >= otherObject) => theObject.Should().BeGreaterOrEqualTo(otherObject)
```

```csharp
Assert.IsFalse(theObject >= otherObject) => theObject.Should().BeLessThan(otherObject)
```

```csharp
Assert.IsTrue(theObject < otherObject) => theObject.Should().BeLessThan(otherObject)
```

```csharp
Assert.IsFalse(theObject < otherObject) => theObject.Should().BeGreaterOrEqualTo(otherObject)
```

```csharp
Assert.IsTrue(theObject <= otherObject) => theObject.Should().BeLessOrEqualTo(otherObject)
```

```csharp
Assert.IsFalse(theObject <= otherObject) => theObject.Should().BeGreaterThan(otherObject)
```


## StringAssert
```csharp
StringAssert.Contains(theString, substr) => theString.Should().Contain(substr)
```

```csharp
StringAssert.StartWith(theString, prefix) => theString.Should().StartWith(prefix)
```

```csharp
StringAssert.EndsWith(theString, suffix) => theString.Should().EndWith(suffix)
```

```csharp
StringAssert.Matches(theString, pattern) => theString.Should().MatchRegex(pattern)
```

```csharp
StringAssert.DoesNotMatch(theString, pattern) => theString.Should().NotMatchRegex(pattern)
```


## CollectionAssert
```csharp
CollectionAssert.AllItemsAreUnique(theCollection) => theCollection.Should().OnlyHaveUniqueItems()
```

```csharp
CollectionAssert.AreEqual(otherCollection, theCollection) => theCollection.Should().Equal(otherCollection)
```

```csharp
CollectionAssert.AreNotEqual(otherCollection, theCollection) => theCollection.Should().NotEqual(otherCollection)
```

```csharp
CollectionAssert.AreEquivalent(otherCollection, theCollection) => theCollection.Should().BeEquivalentTo(otherCollection)
```

```csharp
CollectionAssert.AreNotEquivalent(otherCollection, theCollection) => theCollection.Should().NotBeEquivalentTo(otherCollection)
```

```csharp
CollectionAssert.Contains(theCollection, element) => theCollection.Should().Contain(element)
```

```csharp
CollectionAssert.DoesNotContain(theCollection, element) => theCollection.Should().NotContain(element)
```

```csharp
CollectionAssert.IsSubsetOf(subset, superset) => subset.Should().BeSubset(superset)
```

```csharp
CollectionAssert.IsNotSubsetOf(subset, superset) => subset.Should().NotBeSubset(superset)
```

```csharp
CollectionAssert.AllItemsAreNotNull(theCollection) => theCollection.Should().NotContainNulls()
```

```csharp
CollectionAssert.AllItemsAreInstancesOfType(theCollection, typeof(T)) => theCollection.ContainItemsAssignableTo<T>()
```


# Best Practices

## Numerics / Comparable
```csharp
theInt.Should().BeGreaterThan(0) => theInt.Should().BePositive()
```

```csharp
theInt.Should().BeLessThan(0) => theInt.Should().BeNegative()
```

```csharp
(lower <= theInt && theInt <= upper).Should().BeTrue() => theInt.Should().BeInRange(lower, upper)
```


## Types
```csharp
theObject.GetType().Should().Be(typeof(T)) => theObject.Should().BeOfType<T>()
```

```csharp
theObject.GetType().Should().NotBe(typeof(T)) => theObject.Should().NotBeOfType<T>()
```

```csharp
(theObject is T).Should().BeTrue() => theObject.Should().BeAssignableTo<T>()
```

```csharp
(theObject as T).Should().NotBeNull() => theObject.Should().BeAssignableTo<T>()
```


## Nullables
```csharp
(struct?).HasValue().Should().BeTrue() => (struct?).Should().HaveValue()
```

```csharp
(struct?).HasValue().Should().BeFalse() => (struct?).Should().NotHaveValue()
```


## Enumerables
```csharp
theEnumerable.Any().Should().BeTrue() => theEnumerable.Should().NotBeEmpty()
```

```csharp
theEnumerable.Any().Should().BeFalse() => theEnumerable.Should().BeEmpty()
```

```csharp
theEnumerable.Any(x => x.SomeProperty).Should().BeTrue() => theEnumerable.Should().Contain(x => x.SomeProperty)
```

```csharp
theEnumerable.Any(x => x.SomeProperty).Should().BeFalse() => theEnumerable.Should().NotContain(x => x.SomeProperty)
```

```csharp
theEnumerable.All(x => x.SomeProperty).Should().BeTrue() => theEnumerable.Should().OnlyContain(x => x.SomeProperty) // now fails on empty collection
```

```csharp
theEnumerable.Contains(e).Should().BeTrue() => theEnumerable.Should().Contain(e)
```

```csharp
theEnumerable.Contains(e).Should().BeFalse() => theEnumerable.Should().NotContain(e)
```

```csharp
theEnumerable.Any(e).Should().BeFalse() => theEnumerable.Should().NotContain(e)
```

```csharp
theEnumerable.Count().Should().Be(k) => theEnumerable.Should().HaveCount(k)
```

```csharp
theEnumerable.Count().Should().BeGreaterThan(k) => theEnumerable.Should().HaveCount(c => c > k)
```

```csharp
theEnumerable.Count().Should().BeGreaterOrEqualTo(k) => theEnumerable.Should().HaveCount(c => c >= k)
```

```csharp
theEnumerable.Count().Should().BeLessThan(k) => theEnumerable.Should().HaveCount(c => c < k)
```

```csharp
theEnumerable.Count().Should().BeLessOrEqualTo(k) => theEnumerable.Should().HaveCount(c => c <= k)
```

```csharp
theEnumerable.Count().Should().HaveCount(1) => theEnumerable.Should().ContainSingle()
```

```csharp
theEnumerable.Count().Should().HaveCount(0) => theEnumerable.Should().BeEmpty()
```

```csharp
theEnumerable.Contains(element).Should().BeTrue() => theEnumerable.Should().Contain(element)
```

```csharp
theEnumerable.Contains(element).Should().BeFalse() => theEnumerable.Should().NotContain(element)
```

```csharp
theEnumerable.Where(x => x.SomeProperty).Should().NotBeEmpty() => theEnumerable.Should().Contain(x => x.SomeProperty)
```

```csharp
theEnumerable.Where(x => x.SomeProperty).Should().BeEmpty() => theEnumerable.Should().NotContain(x => x.SomeProperty)
```

```csharp
theEnumerable.Where(x => x.SomeProperty).Should().HavingCount(1) => theEnumerable.Should().ContainSingle(x => x.SomeProperty)
```

```csharp
theEnumerable.Should().OnlyContain(e => !e.SomeProperty) => theEnumerable.Should().NotContain(x => x.SomeProperty)
```

```csharp
theEnumerable.Should().NotBeNull().And.NotBeEmpty() => theEnumerable.Should().NotBeNullOrEmpty()
```

```csharp
theEnumerable.ElementAt(k).Should().Be(element) => theEnumerable.Should().HaveElementAt(k, element)
```

```csharp
theEnumerable.Skip(k).First().Should().Be(element) => theEnumerable.Should().HaveElementAt(k + 1, element)
```

```csharp
theEnumerable.OrderBy(x => x.SomeProperty).Should().Equal(theEnumerable) => theEnumerable.Should().BeInAscendingOrder(x => x.SomeProperty)
```

```csharp
theEnumerable.OrderByDescending(x => x.SomeProperty).Should().Equal(theEnumerable) => theEnumerable.Should().BeInDescendingOrder(x => x.SomeProperty)
```

```csharp
theEnumerable.Select(e1 => e1.SomeProperty).Equal(otherEnumerable.Select(e2 => e2.SomeProperty)) => theEnumerable.Should().Equal(otherEnumerable, (e1, e2) => e1.SomeProperty == e2.SomeProperty)
```

```csharp
theEnumerable.Intersect(otherEnumerable).Should().BeEmpty() => theEnumerable.Should().NotIntersectWith(otherEnumerable)
```

```csharp
theEnumerable.Intersect(otherEnumerable).Should().NotBeEmpty() => theEnumerable.Should().IntersectWith(otherEnumerable)
```

```csharp
theEnumerable.Select(x => x.SomeProperty).Should().NotContainNulls() => theEnumerable.Select(e).Should().NotContain(x => x.SomeProperty == null) // Debatable
```


## Lists
```csharp
theList[k].Should().Be(element) => theList.Should().HaveElementAt(k, element)
```


## Dictionaries
```csharp
theDictionary.ContainsKey(key).Should().BeTrue() => theDictionary.Should().ContainKey(key)
```

```csharp
theDictionary.ContainsKey(key).Should().BeFalse() => theDictionary.Should().NotContainKey(key)
```

```csharp
theDictionary.ContainsValue(value).Should().BeTrue() => theDictionary.Should().ContainValue(value)
```

```csharp
theDictionary.ContainsValue(value).Should().BeFalse() => theDictionary.Should().NotContainValue(value)
```

```csharp
theDictionary.Should().ContainKey(key).And.ContainValue(value) => theDictionary.Should().Contain(key, value)
```

```csharp
theDictionary.Should().ContainKey(KeyValuePair.Key).And.ContainValue(KeyValuePair.value) => theDictionary.Should().Contain(KeyValuePair)
```


## Strings
```csharp
theString.StartsWith(prefix).Should().BeTrue() => theString.Should().StartWith(prefix)
```

```csharp
theString.EndsWith(suffix).Should().BeTrue() => theString.Should().EndWith(suffix)
```

```csharp
theString.Should().NotBeNull().And.NotBeEmpty() => theString.Should().NotBeNullOrEmpty()
```

```csharp
string.NullOrEmpty(theString).Should().BeTrue() => theString.Should().BeNullOrEmpty()
```

```csharp
string.NullOrEmpty(theString).Should().BeFalse() => theString.Should().NotBeNullOrEmpty()
```

```csharp
string.NullOrWhiteSpace(theString).Should().BeTrue() => theString.Should().BeNullOrWhiteSpace()
```

```csharp
string.NullOrWhiteSpace(theString).Should().BeFalse() => theString.Should().NotBeNullOrWhiteSpace()
```

```csharp
theString.Should().HaveCount(k) => theString.Should().HaveLength(k)
```
