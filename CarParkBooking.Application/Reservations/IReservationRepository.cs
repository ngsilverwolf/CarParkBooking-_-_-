using CarParkBooking.Domain;

namespace CarParkBooking.Application.Reservations
{
    public interface IReservationRepository
    {
        public Task<IReadOnlyCollection<Entity<Reservation>>> GetReservationsAsync(int parkingSpaceId, CancellationToken cancellationToken);
        public Task<Entity<Reservation>> CreateReservationAsync(Reservation reservation, CancellationToken cancellationToken);
    }
}