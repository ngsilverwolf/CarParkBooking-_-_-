using CarParkBooking.Application.Customers;
using CarParkBooking.Infrastructure.Persistence;

namespace CarParkBooking.Infrastructure.Customer;

internal class CustomerRepository : ICustomerRepository

{
    private readonly IDatabaseConnector _databaseConnector;

    public CustomerRepository(IDatabaseConnector databaseConnector)
    {
        _databaseConnector = databaseConnector;
    }

    public async Task<Domain.Customer> GetCustomer(int customerId, CancellationToken cancellationToken)
    {
        var result = await _databaseConnector
           .StoredProcedure("GetCustomer")
           .WithParameter("CustomerId", customerId)
               .GetSingleAsync<CustomerRecord>(cancellationToken)
               .ConfigureAwait(false);

        return new Domain.Customer(result.CustomerId, result.FirstName, result.LastName);
    }

}