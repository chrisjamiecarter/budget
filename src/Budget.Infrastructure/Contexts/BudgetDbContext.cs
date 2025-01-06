using Budget.Infrastructure.Entities;
using Budget.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Contexts;

/// <summary>
/// Represents the Entity Framework Core database context for the Budget data store.
/// </summary>
internal class BudgetDbContext : IdentityDbContext<BudgetUserEntity>
{
    #region Constructors

    public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options) { }

    #endregion
    #region Properties

    public DbSet<CategoryModel> Category { get; set; }

    public DbSet<TransactionModel> Transaction { get; set; }

    #endregion
    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
