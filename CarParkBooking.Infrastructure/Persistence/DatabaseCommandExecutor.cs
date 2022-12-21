using System.Data;
using System.Data.Common;
using Dapper;

namespace CarParkBooking.Infrastructure.Persistence
{
    internal sealed class DatabaseCommandExecutor : IDatabaseCommandExecutor
    {

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection,
            CommandDefinition command)
        {
            if (connection is null) throw new ArgumentNullException(nameof(connection));

            return ExecuteCommandAsync(command, connection.QueryAsync<T>);
        }

        public Task<T> QuerySingleAsync<T>(IDbConnection connection,
            CommandDefinition command)
        {
            if (connection is null) throw new ArgumentNullException(nameof(connection));

            return ExecuteCommandAsync(command, connection.QuerySingleAsync<T>);
        }

        public Task<int> ExecuteAsync(IDbConnection connection, CommandDefinition command)
        {
            if (connection is null) throw new ArgumentNullException(nameof(connection));

            return ExecuteCommandAsync(command, connection.ExecuteAsync);
        }

        private static async Task<T> ExecuteCommandAsync<T>(CommandDefinition command,
            Func<CommandDefinition, Task<T>> executor)
        {
            try
            {
                return await executor(command).ConfigureAwait(false);
            }
            catch (DbException exception)
            {
                throw exception;
            }
            catch (InvalidOperationException exception)
            {
                throw exception;
            }
            catch (TaskCanceledException exception) when (!exception.CancellationToken.IsCancellationRequested)
            {
                throw exception;
            }
        }

        private static string GetErrorMessage(string reason, CommandDefinition command, Exception exception) =>
            $"{reason} SQL Command ({command.CommandType}) '{command.CommandText}' " +
            $"with {command}.{exception}";
    }
}
