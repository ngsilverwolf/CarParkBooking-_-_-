namespace CarParkBooking.Common.Generic;

public static class DecimalExtensions
{
    public static string ToCurrency(this decimal value)
    {
        return $"{value:C}"; ;
    }
}
