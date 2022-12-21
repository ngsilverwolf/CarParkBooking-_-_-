namespace CarParkBooking.Infrastructure.Persistence
{
    internal sealed class DatabaseConnector : IDatabaseConnector
    {
        private readonly ISqlHelper _sqlHelper;
        private readonly IDatabaseCommandExecutor _commandExecutor;

        public DatabaseConnector(ISqlHelper sqlHelper,
            IDatabaseCommandExecutor commandExecutor)
        {
            _sqlHelper = sqlHelper ?? throw new ArgumentNullException(nameof(sqlHelper));
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
        }

        public IStoredProcedureCommand StoredProcedure(string storedProcedureName)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(storedProcedureName));

            return new StoredProcedureCommand(
                _commandExecutor,
                storedProcedureName,
                null,
                _sqlHelper);
        }
    }
}
