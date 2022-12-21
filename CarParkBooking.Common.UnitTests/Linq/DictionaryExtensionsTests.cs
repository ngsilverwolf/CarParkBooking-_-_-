using AutoFixture.Xunit2;
using CarParkBooking.Common.Linq;
using FluentAssertions;
using Xunit;

namespace CarParkBooking.Common.UnitTests.Linq;

public abstract class DictionaryExtensionsTests
{
    public sealed class With : DictionaryExtensionsTests
    {
        [Theory]
        [AutoData]
        public void ThenKeyIsAddedToDictionary(IDictionary<string, int> source, string key, int value)
        {
            source.With(key, value)
                .Should().Equal(source)
                .And.Contain(key, value);
        }
    }
}
