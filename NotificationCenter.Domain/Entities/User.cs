namespace NotificationCenter.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}