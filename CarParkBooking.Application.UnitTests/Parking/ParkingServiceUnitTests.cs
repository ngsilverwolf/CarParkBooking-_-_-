using CarParkBooking.Application.Parking;
using CarParkBooking.Domain;
using CarParkBooking.Tests.Common.AutoFixture.Xunit2;
using CarParkBooking.Tests.Common.Xunit;
using CarParkBooking.Tests.Common.Xunit.Castle;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarParkBooking.Application.UnitTests.Parking
{
    public class ParkingServiceUnitTests : IDisposable
    {
        private readonly CancellationTokenSource _cancellation;
        private readonly Mock<IParkingRepository> _mockParkingRepository;
        public ParkingServiceUnitTests()
        {
            _cancellation = new CancellationTokenSource();
            _mockParkingRepository = new Mock<IParkingRepository>();
        }

        public void Dispose()
        {
            _cancellation.Dispose();
        }

        public sealed class Constructor : ParkingServiceUnitTests
        {
            [Theory]
            [MockServiceData]
            internal void ThenNoExceptionIsThrown(IParkingRepository parkingRepository)
            {
                FluentActions
                    .Invoking(() => new ParkingService(
                        parkingRepository: parkingRepository))
                    .Should().NotThrow();
            }
        }

        public sealed class GetPriceAsync : ParkingServiceUnitTests
        {
            [Theory]
            [BookingDatesWithParkingCostData]
            internal async Task ThenTotalCostIsReturned(DateTime dateFromUtc, DateTime dateToUtc, IReadOnlyCollection<ParkingCost> parkingCosts)
            {

                _mockParkingRepository
                    .Setup(repository =>
                        repository.GetParkingCostsAsync(dateFromUtc, dateToUtc, _cancellation.Token))
                    .ReturnsAsync(parkingCosts);

                var parkingService = new ParkingService(_mockParkingRepository.Object);

                (await parkingService.GetPriceAsync(dateFromUtc, dateToUtc, _cancellation.Token))
                    .Should().Be(parkingCosts.Sum(cost => cost.CalculateCost(dateFromUtc, dateToUtc)));
            }
        }

        public sealed class GetAvailableSpacesAsync : ParkingServiceUnitTests
        {
            [Theory]
            [CarParkBookingAutoData]
            internal async Task GivenBookingDatesAreOutsideExistingTheReservationsRange_ThenAllSpacesReturned(
                IReadOnlyCollection<ParkingSpace> parkingSpaces, int daysAfter)
            {
                var dateFromUtc = parkingSpaces
                    .Select(space => space.Reservations
                        .Select(reservation => reservation.Value.DateToUtc).Max())
                    .Max()
                    .AddDays(daysAfter);
                var dateToUtc = dateFromUtc.AddDays(daysAfter);

                _mockParkingRepository
                    .Setup(repository =>
                        repository.GetAllParkingSpacesAsync(_cancellation.Token))
                    .ReturnsAsync(parkingSpaces);

                var parkingService = new ParkingService(_mockParkingRepository.Object);

                (await parkingService.GetAvailableSpacesAsync(dateFromUtc, dateToUtc, _cancellation.Token))
                    .Count()
                    .Should().Be(parkingSpaces.Count);
            }
        }

        public sealed class GetAvailableParkingSpaceAsync : ParkingServiceUnitTests
        {
            [Theory]
            [CarParkBookingAutoData]
            internal async Task GivenSpecifiedParkingSpaceIsAvailable_ThenReturnParkingSpace(
                IReadOnlyCollection<ParkingSpace> parkingSpaces, int daysAfter)
            {
                var dateFromUtc = parkingSpaces
                    .Select(space => space.Reservations
                        .Select(reservation => reservation.Value.DateToUtc).Max())
                    .Max()
                    .AddDays(daysAfter);

                var dateToUtc = dateFromUtc.AddDays(daysAfter);

                _mockParkingRepository
                    .Setup(repository =>
                        repository.GetAllParkingSpacesAsync(_cancellation.Token))
                    .ReturnsAsync(parkingSpaces);

                var parkingService = new ParkingService(_mockParkingRepository.Object);
                var spaceId = parkingSpaces.Select(space => space.Id).First();
                (await parkingService.GetAvailableParkingSpaceAsync(spaceId, dateFromUtc, dateToUtc, _cancellation.Token))
                    .Should().Be(parkingSpaces.First());
            }
        }
    }
}