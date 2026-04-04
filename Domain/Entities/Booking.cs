namespace Domain.Entities;

public class Booking : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid GymClassId { get; private set; }
    public DateTime BookedAt { get; private set; }

    public GymClass? GymClass { get; private set; }

    private Booking() { }

    public static Booking Create(Guid userId, Guid gymClassId)
    {
        return new Booking
        {
            UserId = userId,
            GymClassId = gymClassId,
            BookedAt = DateTime.UtcNow
        };
    }
}
