namespace CarParkBooking.Common.Linq
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> With<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue value)
            where TKey : notnull
        {
            dictionary.Add(key, value);
            return dictionary;
        }
    }
}
