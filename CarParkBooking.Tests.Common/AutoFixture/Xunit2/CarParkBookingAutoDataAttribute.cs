using AutoFixture;
using AutoFixture.Xunit2;

namespace CarParkBooking.Tests.Common.AutoFixture.Xunit2;

public sealed class CarParkBookingAutoDataAttribute : AutoDataAttribute
{
    public CarParkBookingAutoDataAttribute() : base(() => new Fixture().Customize(new CarParkBookingCustomization()))
    {
    }
}
