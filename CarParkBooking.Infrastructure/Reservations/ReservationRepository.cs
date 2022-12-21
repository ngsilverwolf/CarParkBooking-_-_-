using CarParkBooking.Application.Reservations;
using CarParkBooking.Common.Linq;
using CarParkBooking.Domain;
using CarParkBooking.Infrastructure.Persistence;

namespace CarParkBooking.Infrastructure.Reservations;

internal class ReservationRepository : IReservationRepository

{
    private readonly IDatabaseConnector _databaseConnector;
    private readonly IStatusFactory _statusFactory;

    public ReservationRepository(IDatabaseConnector databaseConnector, IStatusFactory statusFactory)
    {
        _databaseConnector = databaseConnector;
        _statusFactory = statusFactory;
    }

    public async Task<IReadOnlyCollection<Entity<Reservation>>> GetReservationsAsync(int parkingSpaceId, CancellationToken cancellationToken)
    {
        var result = await _databaseConnector
            .StoredProcedure("GetReservations")
            .WithParameter("ParkingSpaceId", parkingSpaceId)
            .GetManyAsync<ReservationRecord>(cancellationToken)
            .ConfigureAwait(false);

        return result.Select(res => new Entity<Reservation>(
            res.ReservationId,
            new Reservation(
                res.CustomerId,
                res.ParkingSpaceId,
                res.DateFromUtc,
                res.DateToUtc,
                _statusFactory.Create(res.Status),
                res.Cost))).ToReadOnlyCollection();

    }

    public async Task<Entity<Reservation>> CreateReservationAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        var reservationId = await _databaseConnector
            .StoredProcedure("CreateReservation")
            .WithParameter("ParkingSpaceId", reservation.ParkingSpaceId)
            .WithParameter("CustomerId", reservation.CustomerId)
            .WithParameter("DateFromUtc", reservation.DateFromUtc)
            .WithParameter("DateToUtc", reservation.DateToUtc)
            .WithParameter("Status", reservation.Status.ToString())
            .WithParameter("Cost", reservation.Cost)
            .ExecuteCreateAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Entity<Reservation>(reservationId, reservation);

    }
}