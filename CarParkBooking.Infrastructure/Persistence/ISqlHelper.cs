using System.Data;

namespace CarParkBooking.Infrastructure.Persistence;

public interface ISqlHelper
{
    public IDbConnection GetConnection();
}