using AutoFixture;
using CarParkBooking.Domain;
using CarParkBooking.Tests.Common.AutoFixture;
using System.Reflection;
using Xunit.Sdk;

namespace CarParkBooking.Tests.Common.Xunit;

public sealed class BookingDatesWithParkingCostDataAttribute : DataAttribute
{
    private readonly IFixture _fixture;

    public BookingDatesWithParkingCostDataAttribute()
    {
        _fixture = new Fixture().Customize(new CarParkBookingCustomization());
    }
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var parkingCosts = _fixture.CreateMany<ParkingCost>().ToList();

        var dateFromUtc = parkingCosts.Select(x => x.DateFromUtc).Min();
        var dateToUtc = parkingCosts.Select(x => x.DateToUtc).Max();

        yield return new object[] { dateFromUtc, dateToUtc, parkingCosts };
    }
}
