using CarParkBooking.Application.Customers;
using CarParkBooking.Application.Parking;
using CarParkBooking.Domain;

namespace CarParkBooking.Application.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IParkingService _parkingService;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(ICustomerRepository customerRepository,
            IParkingService parkingService, IReservationRepository reservationRepository)
        {
            _customerRepository = customerRepository;
            _parkingService = parkingService;
            _reservationRepository = reservationRepository;
        }

        public async Task<Entity<Reservation>> ReserveParkingSpaceAsync(int customerId, int spaceId,
            DateTime dateFromUtc,
            DateTime dateToUtc, CancellationToken cancellationToken)
        {
            var space = await _parkingService
                .GetAvailableParkingSpaceAsync(spaceId, dateFromUtc, dateToUtc, cancellationToken);
            var cost = await _parkingService.GetPriceAsync(dateFromUtc, dateToUtc, cancellationToken);
            var customer = await _customerRepository.GetCustomer(customerId, cancellationToken);

            return await _reservationRepository.CreateReservationAsync(new Reservation(
                customer.Id,
                space.Id,
                dateFromUtc,
                dateToUtc,
                Status.Added,
                cost), cancellationToken);
        }
    }
}