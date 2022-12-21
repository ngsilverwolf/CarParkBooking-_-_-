namespace CarParkBooking.API.Controllers.CreateBooking
{
    public sealed record CreateBookingResponse(int Id, int CustomerId, int ParkingSpaceId, DateTime DateFromUtc,
        DateTime DateToUtc, string Status, string Cost);
}