namespace CarParkBooking.Infrastructure.Reservations;

public sealed record ReservationRecord(int ReservationId, int ParkingSpaceId, int CustomerId, DateTime DateFromUtc,
    DateTime DateToUtc, string Status, decimal Cost);
