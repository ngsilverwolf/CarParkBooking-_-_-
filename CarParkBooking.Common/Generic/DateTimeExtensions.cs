namespace CarParkBooking.Common.Generic;

public static class DateTimeExtensions
{
    public static bool IsWithInRange(this DateTime dateTimeUtc,
        DateTime dateFromUtc, DateTime dateToUtc)
    {
        return (dateTimeUtc >= dateFromUtc && dateTimeUtc <= dateToUtc);
    }
}
