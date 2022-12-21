using CarParkBooking.Domain;

namespace CarParkBooking.Application.Reservations;

public interface IReservationService
{
    public Task<Entity<Reservation>> ReserveParkingSpaceAsync(int customerId, int spaceId, DateTime dateFromUtc,
        DateTime dateToUtc, CancellationToken cancellationToken);
}