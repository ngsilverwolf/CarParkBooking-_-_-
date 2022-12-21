using CarParkBooking.Domain;

namespace CarParkBooking.Application.Parking;

public interface IParkingService
{
    Task<decimal> GetPriceAsync(DateTime dateFromUtc, DateTime dateToUtc, CancellationToken cancellationToken);
    Task<IEnumerable<ParkingSpace>> GetAvailableSpacesAsync(DateTime dateFromUtc, DateTime dateToUtc,
        CancellationToken cancellationToken);

    Task<ParkingSpace> GetAvailableParkingSpaceAsync(int spaceId, DateTime dateFromUtc,
        DateTime dateToUtc, CancellationToken cancellationToken);
}