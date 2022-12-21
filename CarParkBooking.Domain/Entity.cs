namespace CarParkBooking.Domain
{
    public sealed class Entity<TValue>
    {

        public Entity(int id, TValue value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get; }

        public TValue Value { get; }
    }
}
