using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationCenter.Application.Abstractions;
using NotificationCenter.Application.User.Repository;
using NotificationCenter.Infrastructure.Repositories;
using NotificationCenter.Infrastructure.Repositories.UnitOfWork;

namespace NotificationCenter.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, string connectionString)
    {
        var cs = config.GetConnectionString("Postgres")
                 ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");


        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}