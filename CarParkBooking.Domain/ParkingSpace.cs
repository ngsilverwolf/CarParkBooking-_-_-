using CarParkBooking.Common.Generic;

namespace CarParkBooking.Domain;

public sealed class ParkingSpace
{
    public ParkingSpace(int id, string name, IReadOnlyCollection<Entity<Reservation>> reservations)
    {
        Id = id;
        Name = name;
        Reservations = reservations;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<Entity<Reservation>> Reservations { get; set; }

    public bool IsAvailable(DateTime dateFromUtc, DateTime dateToUtc)
    {
        var takenReservations = Reservations.Where(res => res.Value.Status.IsActive());
        return !takenReservations
            .Any(res =>
                dateFromUtc.IsWithInRange(res.Value.DateFromUtc, res.Value.DateToUtc) ||
                dateToUtc.IsWithInRange(res.Value.DateFromUtc, res.Value.DateToUtc));
    }
}
