using System.Data;
using System.Data.SqlClient;

namespace CarParkBooking.Infrastructure.Persistence
{
    public class SqlHelper : ISqlHelper
    {
        public IDbConnection GetConnection() =>
            new SqlConnection(
                "Server = (localdb)\\CarPark; Database = CarPark; Trusted_Connection = True; MultipleActiveResultSets = true");
    }
}
