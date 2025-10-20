using Microsoft.EntityFrameworkCore;
using NotificationCenter.Application.User.Repository;
using NotificationCenter.Domain.Entities;

namespace NotificationCenter.Infrastructure.Repositories;

internal sealed class UserRepository(AppDbContext db) : IUserRepository
{
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct) =>
        db.Users.AsNoTracking().AnyAsync(u => u.Email == email, ct);

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct) =>
        db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task AddAsync(User user, CancellationToken ct)
    {
        db.Users.Add(user);
        return Task.CompletedTask;
    }

    public async Task<List<User>> GetAllUsersAsync(CancellationToken ct)
    {
        return await db.Users.ToListAsync(ct);
    }
}