namespace CarParkBooking.Infrastructure.Persistence
{
    public interface IStoredProcedureCommand
    {
        IStoredProcedureCommand WithParameter(string name, object? value);
        Task<IReadOnlyCollection<T>> GetManyAsync<T>(CancellationToken cancellationToken);
        Task<T> GetSingleAsync<T>(CancellationToken cancellationToken);
        Task<int> ExecuteCreateAsync(CancellationToken cancellationToken);
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
