using Budget.Application.Repositories;
using Budget.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Budget.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    #region Fields

    private readonly BudgetDataContext _dataContext;

    #endregion
    #region Constructors

    public UnitOfWork(BudgetDataContext dataContext, ICategoryRepository categoryRepository, ITransactionRepository transactionRepository)
    {
        _dataContext = dataContext;
        Categories = categoryRepository;
        Transactions = transactionRepository;
    }

    #endregion
    #region Properties

    public ICategoryRepository Categories { get; set; }

    public ITransactionRepository Transactions { get; set; }

    #endregion
    #region Methods

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    #endregion
}
