---
title: About
layout: page
---

FluentAssertions started because nothing is more annoying than a unit test that fails without clearly explaining why. Usually, you need to set a breakpoint and start up the debugger to be able to figure out what went wrong. Jeremy D. Miller once gave the advice to "keep out of the debugger hell" and we can only agree with that.

That’s why we designed Fluent Assertions to help you in this area. Not only by using clearly named assertion methods, but also by making sure the failure message provides as much information as possible. Consider this example:

```c#
"1234567890".Should().Be("0987654321");
```

This will be reported as:

> Expected string to be
"0987654321", but
"1234567890" differs near "123" (index 0).

The fact that both strings are displayed on a separate line is not a coincidence and happens if any of them is longer than 8 characters. However, if that's not enough, all assertion methods take an optional explanation (the because) that supports formatting placeholders similar to String.Format which you can use to enrich the failure message. For instance, the assertion

```c#
new[] { 1, 2, 3 }.Should().Contain(item => item > 3, "at least {0} item should be larger than 3", 1);
```

will fail with:

> Collection {1, 2, 3} should have an item matching (item > 3) because at least 1 item should be larger than 3.

## Who are we?

We are a bunch of developers working for Aviva Solutions who highly value software quality, in particular

* [Dennis Doomen](https://twitter.com/ddoomen)  

Notable contributors include

* [Adam Voss](https://github.com/adamvoss)

The [Xamarin](https://github.com/onovotny/fluentassertions) version has been built by

* [Oren Novotny](https://twitter.com/onovotny)

The [Fluent Assertions logo](./logo/fluent_assertions.svg) was created by

* [IUsername](https://github.com/IUsername)

If you have any comments or suggestions, please let us know via [twitter](https://twitter.com/search?q=fluentassertions&src=typd), through the [issues](https://github.com/dennisdoomen/FluentAssertions/issues) page, or through [StackOverflow](http://stackoverflow.com/questions/tagged/fluent-assertions).

## Versioning

The version numbers of Fluent Assertions releases comply to the [Semantic Versioning](http://semver.org/) scheme. In other words, release 1.4.0 only adds backwards-compatible functionality and bug fixes compared to 1.3.0. Release 1.4.1 should only include bug fixes. And if we ever introduce breaking changes, the number increased to 2.0.0.

## What do you need to compile the solution?

* Visual Studio 2013 Update 2 or later
* Windows 8.1
* The Windows Phone 8 SDK