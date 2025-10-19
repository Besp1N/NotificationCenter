using NotificationCenter.Application.Abstractions;

namespace NotificationCenter.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => 
        dbContext.SaveChangesAsync(ct);
}