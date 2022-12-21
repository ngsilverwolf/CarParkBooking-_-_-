using CarParkBooking.Common.Linq;
using Dapper;
using System.Data;

namespace CarParkBooking.Infrastructure.Persistence
{
    internal sealed class StoredProcedureCommand : IStoredProcedureCommand
    {
        private readonly IDatabaseCommandExecutor _commandExecutor;
        private readonly ISqlHelper _sqlHelper;
        private readonly string _storedProcedureName;
        private readonly IDbTransaction? _transaction;

        private readonly IDictionary<string, object?> _parameters;
        private readonly string? _connectionName;
        private readonly TimeSpan? _timeout;

        public StoredProcedureCommand(IDatabaseCommandExecutor commandExecutor, string storedProcedureName,
            IDbTransaction? transaction, ISqlHelper sqlHelper)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(storedProcedureName));

            _parameters = new Dictionary<string, object?>();
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
            _storedProcedureName = storedProcedureName;
            _transaction = transaction;
            _sqlHelper = sqlHelper;
        }

        private StoredProcedureCommand(StoredProcedureCommand command,
            string? connectionName, IDictionary<string, object?> parameters, TimeSpan? timeout, ISqlHelper sqlHelper)
        {
            _commandExecutor = command._commandExecutor;

            _storedProcedureName = command._storedProcedureName;
            _transaction = command._transaction;

            _connectionName = connectionName;
            _parameters = parameters;
            _timeout = timeout;
            _sqlHelper = sqlHelper;
        }
        
        public IStoredProcedureCommand WithParameter(string name, object? value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            return new StoredProcedureCommand(this, _connectionName, _parameters.With(name, value), _timeout, _sqlHelper);
        }

        public Task<IReadOnlyCollection<T>> GetManyAsync<T>(CancellationToken cancellationToken) =>
            ExecuteAsync(_commandExecutor.QueryAsync<T>, cancellationToken).ToReadOnlyCollectionAsync();

        public Task<T> GetSingleAsync<T>(CancellationToken cancellationToken) =>
            ExecuteAsync(_commandExecutor.QuerySingleAsync<T>, cancellationToken);

        public Task<int> ExecuteCreateAsync(CancellationToken cancellationToken) =>
            ExecuteAsync(_commandExecutor.QuerySingleAsync<int>, cancellationToken);

        public Task ExecuteAsync(CancellationToken cancellationToken) =>
            ExecuteAsync(_commandExecutor.ExecuteAsync, cancellationToken);

        private async Task<T> ExecuteAsync<T>(Func<IDbConnection, CommandDefinition,
            Task<T>> executorAsync, CancellationToken cancellationToken)
        {
            var command = GetCommandDefinition(cancellationToken);

            if (_transaction?.Connection is not null)
            {
                return await executorAsync(_transaction.Connection, command).ConfigureAwait(false);
            }

            using var connection = _sqlHelper.GetConnection();

            return await executorAsync(connection, command).ConfigureAwait(false);
        }

        private CommandDefinition GetCommandDefinition(CancellationToken cancellationToken) =>
            new(
                _storedProcedureName,
                _parameters.Any()
                    ? new DynamicParameters(_parameters)
                    : null,
                _transaction,
                _timeout?.Seconds,
                CommandType.StoredProcedure,
                CommandFlags.Buffered,
                cancellationToken);
    }
}
