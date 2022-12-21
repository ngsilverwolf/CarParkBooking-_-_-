using AutoFixture;
using CarParkBooking.Domain;

namespace CarParkBooking.Tests.Common.AutoFixture;

internal sealed class ParkingCostCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<ParkingCost>(composer => composer
            .FromFactory((DateTime dateFromUtc, int daysAfter) => new ParkingCost(
                dateFromUtc: dateFromUtc,
                dateToUtc: dateFromUtc.AddDays(daysAfter),
                pricePerDay: fixture.Create<decimal>())));
    }
}
