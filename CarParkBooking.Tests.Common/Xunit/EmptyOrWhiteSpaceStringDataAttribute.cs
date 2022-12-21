using System.Reflection;
using Xunit.Sdk;

namespace CarParkBooking.Tests.Common.Xunit;

public sealed class EmptyOrWhiteSpaceStringDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { string.Empty };
        yield return new object[] { new string(' ', 1) };
        yield return new object[] { new string(' ', 10) };
    }
}
