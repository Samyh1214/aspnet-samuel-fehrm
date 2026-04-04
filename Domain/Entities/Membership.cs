namespace Domain.Entities;

public class Membership : BaseEntity
{
    public string Type { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive => DateTime.UtcNow <= EndDate;

    public Guid UserId { get; private set; }

    private Membership() { } // Krävs av EF Core

    public static Membership Create(string type, DateTime startDate, DateTime endDate, Guid userId)
    {
        return new Membership
        {
            Type = type,
            StartDate = startDate,
            EndDate = endDate,
            UserId = userId
        };
    }
}
