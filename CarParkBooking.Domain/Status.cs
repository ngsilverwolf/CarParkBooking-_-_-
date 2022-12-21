namespace CarParkBooking.Domain;

public class Status
{
    public static Status Cancelled => new("Cancelled");
    public static Status Added => new("Added");
    public static Status Confirmed => new("Confirmed");

    public static IEnumerable<Status> All
    {
        get
        {
            yield return Cancelled;
            yield return Added;
            yield return Confirmed;
        }
    }

    private readonly string _name;

    private Status(string name)
    {
        _name = name;
    }

    public override string ToString() => _name;
    public bool IsActive() => this != Cancelled;

}