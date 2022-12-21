using CarParkBooking.Domain;

namespace CarParkBooking.Application.Customers
{
    public interface ICustomerRepository
    {
        public Task<Customer> GetCustomer(int customerId, CancellationToken cancellationToken);
    }
}