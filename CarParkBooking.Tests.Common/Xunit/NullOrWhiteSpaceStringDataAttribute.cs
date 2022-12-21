using System.Reflection;
using Xunit.Sdk;

namespace CarParkBooking.Tests.Common.Xunit;

public sealed class NullOrWhiteSpaceStringDataAttribute : DataAttribute
{
    private readonly EmptyOrWhiteSpaceStringDataAttribute _emptyOrWhiteSpaceData;

    public NullOrWhiteSpaceStringDataAttribute()
    {
        _emptyOrWhiteSpaceData = new EmptyOrWhiteSpaceStringDataAttribute();
    }

    public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
    {
        yield return new object?[] { null };

        foreach (var emptyOrNullData in _emptyOrWhiteSpaceData.GetData(testMethod))
            yield return emptyOrNullData;
    }
}
