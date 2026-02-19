#pragma warning disable

using System.Runtime.CompilerServices;
using FluentAssertions;
using FluentAssertions.Tests;

[assembly: FluentAssertions.Extensibility.AssertionEngineInitializer(
    typeof(AcceptLicence),
    nameof(AcceptLicence.AcknowledgeSoftWarning))]

namespace FluentAssertions.Tests;

internal static class AcceptLicence
{
    public static void AcknowledgeSoftWarning()
    {
        // Suppress the soft warning about the license requirements for commercial use
        License.Accepted = true;
    }
}
