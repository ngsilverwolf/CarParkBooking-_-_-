using AutoFixture;

namespace CarParkBooking.Tests.Common.AutoFixture;

public sealed class CarParkBookingCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture
            .Customize(new ReservationCustomization())
            .Customize(new ParkingCostCustomization())
            .Customize(new ParkingSpaceCustomization());
    }

}
