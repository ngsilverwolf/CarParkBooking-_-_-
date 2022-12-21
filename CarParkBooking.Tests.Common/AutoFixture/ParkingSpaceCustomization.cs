using AutoFixture;
using CarParkBooking.Domain;

namespace CarParkBooking.Tests.Common.AutoFixture;

internal sealed class ParkingSpaceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<ParkingSpace>(composer => composer
            .FromFactory(() => new ParkingSpace(
                id: fixture.Create<int>(),
                name: fixture.Create<string>(),
                reservations: fixture.CreateMany<Entity<Reservation>>().ToList())));
    }
}
