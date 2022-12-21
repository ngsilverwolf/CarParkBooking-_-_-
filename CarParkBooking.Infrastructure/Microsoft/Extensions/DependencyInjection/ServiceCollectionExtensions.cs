using CarParkBooking.Application.Customers;
using CarParkBooking.Application.Parking;
using CarParkBooking.Application.Reservations;
using CarParkBooking.Infrastructure.Customer;
using CarParkBooking.Infrastructure.Parking;
using CarParkBooking.Infrastructure.Persistence;
using CarParkBooking.Infrastructure.Reservations;
using Microsoft.Extensions.DependencyInjection;

namespace CarParkBooking.Infrastructure.Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IParkingRepository, ParkingRepository>();
        services.AddTransient<IParkingSpaceMapper, ParkingSpaceMapper>();
        services.AddTransient<IReservationRepository, ReservationRepository>();
        services.AddTransient<IStatusFactory, StatusFactory>();
        return services
            .AddPersistence();
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseCommandExecutor, DatabaseCommandExecutor>();
        services.AddTransient<IDatabaseConnector, DatabaseConnector>();
        services.AddTransient<ISqlHelper, SqlHelper>();

        return services;
    }
}
