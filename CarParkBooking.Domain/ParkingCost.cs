namespace CarParkBooking.Domain;

public class ParkingCost
{
    public DateTime DateFromUtc { get; }
    public DateTime DateToUtc { get; }
    public decimal PricePerDay { get; }

    public ParkingCost(DateTime dateFromUtc, DateTime dateToUtc, decimal pricePerDay)
    {
        DateFromUtc = dateFromUtc;
        DateToUtc = dateToUtc;
        PricePerDay = pricePerDay;
    }

    public decimal CalculateCost(DateTime dateFromUtc, DateTime dateToUtc)
    {
        if (dateFromUtc >= DateToUtc)
            throw new ArgumentOutOfRangeException(nameof(dateFromUtc), $"provided date:{dateFromUtc} must be before DateToUtc");

        if (dateFromUtc <= DateFromUtc)
        {
            if (dateToUtc >= DateToUtc)
                return CalculateAndFormat(DateToUtc, DateFromUtc);

            return CalculateAndFormat(dateToUtc, DateFromUtc);
        }

        if (dateToUtc >= DateToUtc)
            return CalculateAndFormat(DateToUtc, dateFromUtc);

        return CalculateAndFormat(dateToUtc, dateFromUtc);
    }

    private decimal CalculateAndFormat(DateTime dateToUtc, DateTime dateFromUtc) =>
        Math.Round((decimal)(dateToUtc - dateFromUtc).TotalDays * PricePerDay, 2);
}