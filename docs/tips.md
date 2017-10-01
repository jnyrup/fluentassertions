---
title: Tips
layout: page
---

The examples below show how you might improve your existing assertions to both get more readable assertions and much more informative failure messages.

General tips:
* If your assertion ends with `Should().BeTrue()`, there is most likely a better way to write it.
* By having `Should()` as early as possible in the assertion, we are able to include more information in the failure messages.

If you see something missing, please consider submitting a pull request.

## Table of Contents ##
* TOC
{:toc}


{% include template.html header1="Inferior" header2="Better" caption="Collections"               examples=site.data.tips.collections %}
{% include template.html header1="Inferior" header2="Better" caption="Comparable and Numerics"   examples=site.data.tips.comparable %}
{% include template.html header1="Inferior" header2="Better" caption="Dictionaries"              examples=site.data.tips.dictionaries %}
{% include template.html header1="Inferior" header2="Better" caption="Exceptions"                examples=site.data.tips.exceptions %}
{% include template.html header1="Inferior" header2="Better" caption="Nullables"                 examples=site.data.tips.nullables %}
{% include template.html header1="Inferior" header2="Better" caption="Strings"                   examples=site.data.tips.strings %}
{% include template.html header1="Inferior" header2="Better" caption="Types"                     examples=site.data.tips.types %}