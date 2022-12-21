using AutoFixture.Xunit2;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CarParkBooking.Domain.UnitTests
{
    public class ParkingCostUnitTests
    {
        [Theory]
        [AutoData]
        public void GivenValidDates_ReturnTotalCost(DateTime dateFromUtc, [MaxLength(150)] int randomAmountOfDays, decimal cost)
        {

            var parkingCost = new ParkingCost(dateFromUtc, dateFromUtc.AddDays(randomAmountOfDays), cost);

            parkingCost.CalculateCost(dateFromUtc, dateFromUtc.AddDays(randomAmountOfDays)).Should().Be(randomAmountOfDays * cost);

        }

        [Theory]
        [AutoData]
        public void GivenInValidDates_ThrowArgumentOutOfRangeException(DateTime dateFromUtc,
            [MaxLength(150)] int randomAmountOfDays, decimal cost)
        {

            var parkingCost = new ParkingCost(dateFromUtc, dateFromUtc.AddDays(randomAmountOfDays), cost);

            var invalidDateFrom = dateFromUtc.AddDays(randomAmountOfDays + 5);

            FluentActions
                .Invoking(() => parkingCost.CalculateCost(invalidDateFrom, invalidDateFrom.AddDays(randomAmountOfDays)))
                .Should().ThrowExactly<ArgumentOutOfRangeException>()
                .WithParameterName("dateFromUtc")
                .WithMessage($"provided date:{invalidDateFrom} must be before DateToUtc *");

        }
    }
}