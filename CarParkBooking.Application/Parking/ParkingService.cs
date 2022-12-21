using CarParkBooking.Domain;

namespace CarParkBooking.Application.Parking
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;

        public ParkingService(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<decimal> GetPriceAsync(DateTime dateFromUtc, DateTime dateToUtc, CancellationToken cancellationToken)
        {
            var parkingCosts = await _parkingRepository.GetParkingCostsAsync(dateFromUtc, dateToUtc, cancellationToken);
            return parkingCosts.Sum(cost => cost.CalculateCost(dateFromUtc, dateToUtc));
        }

        public async Task<IEnumerable<ParkingSpace>> GetAvailableSpacesAsync(DateTime dateFromUtc, DateTime dateToUtc,
            CancellationToken cancellationToken)
        {
            var parkingSpaces = await _parkingRepository
                .GetAllParkingSpacesAsync(cancellationToken);
            return parkingSpaces.Where(space => space.IsAvailable(dateFromUtc, dateToUtc));
        }

        public async Task<ParkingSpace> GetAvailableParkingSpaceAsync(int spaceId, DateTime dateFromUtc,
            DateTime dateToUtc, CancellationToken cancellationToken)
        {
            var availableSpaces = await GetAvailableSpacesAsync(dateFromUtc, dateToUtc, cancellationToken);
            var specifiedSpace = availableSpaces.First(space => space.Id == spaceId);
            if (specifiedSpace is null)
                throw new NotImplementedException("😮 whoopsie looks like you forgot to handle this better");

            return specifiedSpace;
        }
    }
}