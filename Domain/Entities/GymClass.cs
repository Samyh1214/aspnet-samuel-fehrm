namespace Domain.Entities;

public class GymClass : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Instructor { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int MaxCapacity { get; private set; }

    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

    public int AvailableSpots => MaxCapacity - _bookings.Count;
    public bool IsFull => AvailableSpots <= 0;

    private GymClass() { }

    public static GymClass Create(string name, string description, string instructor,
        DateTime startTime, DateTime endTime, int maxCapacity)
    {
        return new GymClass
        {
            Name = name,
            Description = description,
            Instructor = instructor,
            StartTime = startTime,
            EndTime = endTime,
            MaxCapacity = maxCapacity
        };
    }
}
