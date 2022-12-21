using AutoFixture.Xunit2;
using CarParkBooking.Common.Linq;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CarParkBooking.Domain.UnitTests
{
    public class ParkingSpaceTests
    {
        [Theory]
        [AutoData]
        public void GivenCarParkIsBookedUp_ReturnFalse(DateTime dateFromUtc, [MaxLength(150)] int randomAmountOfDays)
        {

            var reservation = new Entity<Reservation>(123, new Reservation(123, 123, dateFromUtc, dateFromUtc.AddYears(1), Status.Added, 10.00m));
            var parkingSpace = new ParkingSpace(421, "test", reservation.ToSequence().ToReadOnlyCollection());


            var tstDayFrom = dateFromUtc.AddDays(randomAmountOfDays);
            var tstDayTo = tstDayFrom.AddDays(randomAmountOfDays);

            var result = parkingSpace.IsAvailable(tstDayFrom, tstDayTo);


            result.Should().BeFalse();


        }

        [Theory]
        [AutoData]
        public void GivenCarParkIsAvailable_ReturnTrue(DateTime dateFromUtc, [MaxLength(150)] int randomAmountOfDays)
        {

            var reservation = new Entity<Reservation>(123, new Reservation(123, 123, dateFromUtc, dateFromUtc.AddYears(1), Status.Added, 10.00m));
            var parkingSpace = new ParkingSpace(421, "test", reservation.ToSequence().ToReadOnlyCollection());

            var tstDayTo = dateFromUtc.AddDays(-randomAmountOfDays);
            var tstDayFrom = tstDayTo.AddDays(-randomAmountOfDays);


            var result = parkingSpace.IsAvailable(tstDayFrom, tstDayTo);


            result.Should().BeTrue();


        }
    }
}