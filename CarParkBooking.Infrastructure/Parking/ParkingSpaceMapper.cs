using CarParkBooking.Application.Reservations;
using CarParkBooking.Domain;

namespace CarParkBooking.Infrastructure.Parking
{
    internal class ParkingSpaceMapper : IParkingSpaceMapper
    {
        private readonly IReservationRepository _reservationRepository;

        public ParkingSpaceMapper(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ParkingSpace> Map(ParkingRecord parkingRecord, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository
                .GetReservationsAsync(parkingRecord.ParkingSpaceId, cancellationToken);
            return new ParkingSpace(
                parkingRecord.ParkingSpaceId,
                parkingRecord.Name,
                reservations);
        }
    }
}
