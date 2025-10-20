namespace NotificationCenter.Application.User.DTOs;

public class UserDto
{
    public required Guid Guid { get; init; }
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}