using Budget.Application.Repositories;
using Budget.Infrastructure.Contexts;
using Budget.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budget.Infrastructure.Installers;

/// <summary>
/// Installs all dependencies for the application project.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("Budget") ?? throw new InvalidOperationException("Connection string 'Budget' not found");

        services.AddDbContext<BudgetDataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
