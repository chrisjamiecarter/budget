﻿using Budget.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Budget.Application.Installers;

/// <summary>
/// Installs all dependencies for the application project.
/// </summary>
public static class Installer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
