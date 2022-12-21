using CarParkBooking.Domain;

namespace CarParkBooking.Application.Parking
{
    public interface IParkingRepository
    {
        public Task<IReadOnlyCollection<ParkingSpace>> GetAllParkingSpacesAsync(CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ParkingCost>> GetParkingCostsAsync(DateTime dateFromUtc, DateTime dateToUtc,
            CancellationToken cancellationToken);
    }
}