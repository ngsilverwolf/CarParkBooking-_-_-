using System.Data;
using Dapper;

namespace CarParkBooking.Infrastructure.Persistence
{
    internal interface IDatabaseCommandExecutor
    {

        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, CommandDefinition command);

        Task<T> QuerySingleAsync<T>(IDbConnection connection, CommandDefinition command);

        Task<int> ExecuteAsync(IDbConnection connection, CommandDefinition command);
    }
}
