using Budget.Application.Repositories;
using Budget.Infrastructure.Contexts;
using Budget.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budget.Infrastructure.Installers;

/// <summary>
/// Registers dependencies and seeds data for the Infrastructure layer.
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

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<BudgetDataContext>();
        context.Database.Migrate();

        // TODO:
        var categoryRepository = serviceProvider.GetRequiredService<ICategoryRepository>();
        var transactionRepository = serviceProvider.GetRequiredService<ITransactionRepository>();
        //SeederService.SeedDatabase(repository);

        return serviceProvider;
    }
}
