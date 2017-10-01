---
title: Examples
layout: page
---

<link href="/assets/css/syntax.css" rel="stylesheet" >

Fluent Assertions is a set of .NET extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style test.  This enables, simple intuitive syntax like in the examples below.


To verify that a string begins, ends and contains a particular phrase.

```c#
string actual = "ABCDEFGHI";
actual.Should().StartWith("AB").And.EndWith("HI").And.Contain("EF").And.HaveLength(9);
```

To verify that a collection contains a specified number of elements and that all elements match a predicate.

```c#
IEnumerable collection = new[] { 1, 2, 3 };
collection.Should().HaveCount(4, "because we thought we put four items in the collection"))
```

The nice thing about the second failing example is that it will throw an exception with the message

> "Expected <4> items because we thought we put four items in the collection, but found <3>."

To verify that a particular business rule is enforced using exceptions.

```c#
var recipe = new RecipeBuilder()
                    .With(new IngredientBuilder().For("Milk").WithQuantity(200, Unit.Milliliters))
                    .Build();
Action action = () => recipe.AddIngredient("Milk", 100, Unit.Spoon);
action
                    .ShouldThrow<RuleViolationException>()
                    .WithMessage("change the unit of an existing ingredient", ComparisonMode.Substring)
                    .And.Violations.Should().Contain(BusinessRule.CannotChangeIngredientQuanity);
```

One neat feature is the ability to chain a specific assertion on top of an assertion that acts on a collection or graph of objects.

```c#
dictionary.Should().ContainValue(myClass).Which.SomeProperty.Should().BeGreaterThan(0);
someObject.Should().BeOfType<Exception>().Which.Message.Should().Be("Other Message");
xDocument.Should().HaveElement("child").Which.Should().BeOfType<XElement>().And.HaveAttribute("attr", "1");
```

This sort of chaining can make your unit tests a lot easier to read.

## MSTest Migration
{:.no_toc}

The examples below show how you might write equivalent MSTest assertions using FluentAssertions including the failure message from each case.
We think this is both a useful migration guide and a convincing argument for switching.
If you see something missing, please consider submitting a pull request.

* TOC
{:toc}

{% include template.html caption="Assert"                    examples=site.data.assert %}
{% include template.html caption="CollectionAssert"          examples=site.data.collectionAssert %}
{% include template.html caption="StringAssert"              examples=site.data.stringAssert %}
{% include template.html caption="Collections"               examples=site.data.collections %}
{% include template.html caption="Comparable and Numerics"   examples=site.data.comparable %}
{% include template.html caption="Dictionaries"              examples=site.data.dictionaries %}
{% include template.html caption="Exceptions"                examples=site.data.expectedException %}
{% include template.html caption="Nullables"                 examples=site.data.nullables %}
{% include template.html caption="Strings"                   examples=site.data.strings %}
{% include template.html caption="Types"                     examples=site.data.types %}