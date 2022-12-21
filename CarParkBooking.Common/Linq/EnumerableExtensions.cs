namespace CarParkBooking.Common.Linq;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ToSequence<T>(this T value)
    {
        yield return value;
    }

    public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(
        this IEnumerable<T> source)
    {
        return source != null ? (IReadOnlyCollection<T>)source.ToList<T>() : throw new ArgumentNullException(nameof(source));
    }

    public static async Task<IReadOnlyCollection<T>> ToReadOnlyCollectionAsync<T>(
        this Task<IEnumerable<T>> task)
    {
        return task != null
            ? (await task).ToReadOnlyCollection()
            : throw new ArgumentNullException(nameof(task));
    }

    public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, Task<TResult>> selectorAsync)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (selectorAsync is null) throw new ArgumentNullException(nameof(selectorAsync));

        var results = new List<TResult>();

        foreach (var item in source)
        {
            results.Add(await selectorAsync(item).ConfigureAwait(false));
        }

        return results;
    }
}
