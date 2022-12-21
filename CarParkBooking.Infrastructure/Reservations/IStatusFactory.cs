using CarParkBooking.Domain;

namespace CarParkBooking.Infrastructure.Reservations;

internal interface IStatusFactory
{
    Status Create(string status);
}