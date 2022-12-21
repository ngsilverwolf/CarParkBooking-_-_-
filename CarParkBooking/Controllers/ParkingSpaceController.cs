using CarParkBooking.API.Controllers.CreateBooking;
using CarParkBooking.API.Controllers.GetAvailableSpace;
using CarParkBooking.Application.Parking;
using CarParkBooking.Application.Reservations;
using CarParkBooking.Common.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CarParkBooking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingSpaceController : ControllerBase
    {

        private readonly ILogger<ParkingSpaceController> _logger;
        private readonly IParkingService _parkingService;
        private readonly IReservationService _reservationService;

        public ParkingSpaceController(ILogger<ParkingSpaceController> logger, IParkingService parkingService,
            IReservationService reservationService)
        {
            _logger = logger;
            _parkingService = parkingService;
            _reservationService = reservationService;
        }

        [HttpGet("GetAvailableSpaces")]
        [ProducesResponseType(typeof(GetAvailableSpaceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableSpaces(DateTime dateFromUtc, DateTime dateToUtc,
            CancellationToken cancellationToken)
        {
            var availableSpaces = await _parkingService.GetAvailableSpacesAsync(dateFromUtc, dateToUtc, cancellationToken);

            return Ok(new GetAvailableSpaceResponse(availableSpaces
                .Select(space => new GetAvailableSpaceResponse.SpaceResponse(space.Id))
                .ToArray()));
        }

        [HttpGet("GetPrice")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrice(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
        {
            var result = await _parkingService.GetPriceAsync(dateFrom, dateTo, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{customerId}/Reserve")]
        [ProducesResponseType(typeof(CreateBookingResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBooking(int customerId, int spaceId, DateTime dateFromUtc, DateTime dateToUtc,
            CancellationToken cancellationToken)
        {
            var reservation = await _reservationService.ReserveParkingSpaceAsync(customerId, spaceId, dateFromUtc,
                dateToUtc, cancellationToken);

            return Ok(new CreateBookingResponse(
                reservation.Id,
                reservation.Value.CustomerId,
                reservation.Value.ParkingSpaceId,
                reservation.Value.DateFromUtc,
                reservation.Value.DateToUtc,
                reservation.Value.Status.ToString(),
                reservation.Value.Cost.ToCurrency()));

        }

        [HttpPatch("{reservationId}/Cancel")]
        public async Task<IActionResult> CancelBooking(int reservationId, int customerId)
        {
            return Ok();
        }

        [HttpPut("{reservationId}/Amend")]
        public async Task<IActionResult> AmendBooking(int reservationId, int customerId, DateTime dateFromUtc, DateTime dateToUtc)
        {
            return Ok();
        }
    }
}