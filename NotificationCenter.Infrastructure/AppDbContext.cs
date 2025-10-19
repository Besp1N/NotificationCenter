using Microsoft.EntityFrameworkCore;
using NotificationCenter.Domain.Entities;
using NotificationCenter.Infrastructure.Configurations;

namespace NotificationCenter.Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

}