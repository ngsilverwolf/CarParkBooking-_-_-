using AutoFixture;
using CarParkBooking.Domain;

namespace CarParkBooking.Tests.Common.AutoFixture;

internal sealed class ReservationCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Entity<Reservation>>(composer => composer
            .FromFactory((Reservation reservation) => new Entity<Reservation>(
                id: fixture.Create<int>(),
                reservation)));

        fixture.Customize<Reservation>(composer => composer
            .FromFactory((DateTime dateFromUtc, int daysAfter) => new Reservation(
                customerId: fixture.Create<int>(),
                parkingSpaceId: fixture.Create<int>(),
                dateFromUtc: dateFromUtc,
                dateToUtc: dateFromUtc.AddDays(daysAfter),
                status: fixture.Create<Status>(),
                cost: fixture.Create<decimal>()
                )));
    }
}
