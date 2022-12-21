using CarParkBooking.Application.Parking;
using CarParkBooking.Application.Reservations;
using Microsoft.Extensions.DependencyInjection;
namespace CarParkBooking.Application.Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IParkingService, ParkingService>();
        services.AddTransient<IReservationService, ReservationService>();

        return services;
    }
}
