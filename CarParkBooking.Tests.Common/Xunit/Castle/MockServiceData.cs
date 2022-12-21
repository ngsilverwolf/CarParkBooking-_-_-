using Castle.DynamicProxy;
using System.Reflection;
using Xunit.Sdk;

namespace CarParkBooking.Tests.Common.Xunit.Castle;

public sealed class MockServiceData : DataAttribute
{
    private readonly IProxyGenerator _generator;

    public MockServiceData()
    {
        _generator = new ProxyGenerator();
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return testMethod.GetParameters()
            .Select(CreateProxyType)
            .ToArray();
    }

    private object CreateProxyType(ParameterInfo parameter)
    {
        if (parameter.ParameterType.IsInterface)
            return _generator.CreateInterfaceProxyWithoutTarget(parameter.ParameterType);

        if (parameter.ParameterType.IsAbstract)
            return _generator.CreateClassProxy(parameter.ParameterType);

        throw CannotBeProxied(parameter);
    }

    private static InvalidOperationException CannotBeProxied(ParameterInfo parameter) =>
        new("Failed to generate test case. " +
            "Each parameter must be an interface or an abstract class to be proxied. " +
            $"'{parameter.ParameterType.AssemblyQualifiedName}' is neither.");
}
