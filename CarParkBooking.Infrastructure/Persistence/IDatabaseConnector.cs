namespace CarParkBooking.Infrastructure.Persistence
{
    public interface IDatabaseConnector
    {
        IStoredProcedureCommand StoredProcedure(string storedProcedureName);
    }
}
