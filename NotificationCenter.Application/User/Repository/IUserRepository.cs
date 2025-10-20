namespace NotificationCenter.Application.User.Repository;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);
    Task<Domain.Entities.User?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Entities.User user, CancellationToken ct);
    Task<List<Domain.Entities.User>> GetAllUsersAsync(CancellationToken ct);
}