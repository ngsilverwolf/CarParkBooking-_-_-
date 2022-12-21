using CarParkBooking.Application.Parking;
using CarParkBooking.Common.Linq;
using CarParkBooking.Domain;
using CarParkBooking.Infrastructure.Persistence;

namespace CarParkBooking.Infrastructure.Parking;

internal class ParkingRepository : IParkingRepository

{
    private readonly IDatabaseConnector _databaseConnector;
    private readonly IParkingSpaceMapper _parkingSpaceMapper;

    public ParkingRepository(IDatabaseConnector databaseConnector, IParkingSpaceMapper parkingSpaceMapper)
    {
        _databaseConnector = databaseConnector;
        _parkingSpaceMapper = parkingSpaceMapper;
    }

    public async Task<IReadOnlyCollection<ParkingSpace>> GetAllParkingSpacesAsync(CancellationToken cancellationToken)
    {
        var result = await _databaseConnector
           .StoredProcedure("GetParkingSpaces")
               .GetManyAsync<ParkingRecord>(cancellationToken)
               .ConfigureAwait(false);

        return await result
            .SelectAsync(space => _parkingSpaceMapper.Map(space, cancellationToken))
            .ToReadOnlyCollectionAsync();
    }

    public async Task<IReadOnlyCollection<ParkingCost>> GetParkingCostsAsync(DateTime dateFromUtc, DateTime dateToUtc,
        CancellationToken cancellationToken)
    {
        var result = await _databaseConnector
            .StoredProcedure("GetParkingCosts")
            .WithParameter("DateFromUtc", dateFromUtc)
            .WithParameter("DateToUtc", dateToUtc)
            .GetManyAsync<ParkingCostRecord>(cancellationToken)
            .ConfigureAwait(false);

        return result.Select(cost => new ParkingCost(cost.DateFromUtc, cost.DateToUtc, cost.PricePerDay))
            .ToReadOnlyCollection();

    }
}