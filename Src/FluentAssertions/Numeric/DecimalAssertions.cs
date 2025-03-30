﻿using System;
using System.Diagnostics;
using System.Globalization;
using FluentAssertions.Execution;

namespace FluentAssertions.Numeric;

/// <summary>
/// Contains a number of methods to assert that a <see cref="decimal"/> is in the expected state.
/// </summary>
[DebuggerNonUserCode]
internal class DecimalAssertions : NumericAssertions<decimal>
{
    internal DecimalAssertions(decimal value, AssertionChain assertionChain)
        : base(value, assertionChain)
    {
    }

    private protected override string? CalculateDifferenceForFailureMessage(decimal subject, decimal expected)
    {
        try
        {
            decimal difference = subject - expected;
            return difference != 0 ? difference.ToString(CultureInfo.InvariantCulture) : null;
        }
        catch (OverflowException)
        {
            return null;
        }
    }
}
