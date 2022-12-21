namespace CarParkBooking.Infrastructure.Parking;

public sealed record ParkingCostRecord(int CostId, DateTime DateFromUtc, DateTime DateToUtc, decimal PricePerDay);