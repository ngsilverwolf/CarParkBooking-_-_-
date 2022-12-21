namespace CarParkBooking.Domain;

public sealed class Reservation
{
    public Reservation(int customerId, int parkingSpaceId, DateTime dateFromUtc, DateTime dateToUtc, Status status, decimal cost)
    {
        if (dateFromUtc > dateToUtc)
            throw new ArgumentOutOfRangeException("Well something isn't correct...");

        CustomerId = customerId;
        ParkingSpaceId = parkingSpaceId;
        DateFromUtc = dateFromUtc;
        DateToUtc = dateToUtc;
        Status = status;
        Cost = cost;
    }


    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ParkingSpaceId { get; set; }

    public DateTime DateFromUtc { get; set; }

    public DateTime DateToUtc { get; set; }

    public Status Status { get; set; }

    public decimal Cost { get; set; }

}