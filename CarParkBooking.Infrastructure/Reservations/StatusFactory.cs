using CarParkBooking.Domain;

namespace CarParkBooking.Infrastructure.Reservations;

class StatusFactory : IStatusFactory
{
    public Status Create(string status)
    {
        return Status.All.First(x => x.ToString() == status);
    }
}