using AutoFixture.Xunit2;
using CarParkBooking.Common.Linq;
using CarParkBooking.Tests.Common.Xunit;
using FluentAssertions;
using Xunit;

namespace CarParkBooking.Common.UnitTests.Linq;

public abstract class EnumerableExtensionsTests
{
    public sealed class ToSequence : EnumerableExtensionsTests
    {
        [Theory]
        [AutoData]
        [NullOrWhiteSpaceStringData]
        public void ThenSequenceOfSingleElementIsReturned(string value)
        {
            value.ToSequence()
                .Should().ContainSingle()
                .Which.Should().Be(value);
        }
    }
    public sealed class ToReadOnlyCollection : EnumerableExtensionsTests
    {
        [Fact]
        public void ThenSequenceOfSingleElementIsReturned()
        {
            IEnumerable<int> nullSequence = null;

            FluentActions.Invoking(() =>
                    nullSequence.ToReadOnlyCollection())
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("source");
        }
    }
}
