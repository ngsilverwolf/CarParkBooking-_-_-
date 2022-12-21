using CarParkBooking.Domain;

namespace CarParkBooking.Infrastructure.Parking;

internal interface IParkingSpaceMapper
{
    public Task<ParkingSpace> Map(ParkingRecord parkingRecord, CancellationToken cancellationToken);
}