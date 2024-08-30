using Budget.Application.Repositories;
using Budget.Infrastructure.Contexts;
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
        return await _dataContext.SaveChangesAsync();
    }

    #endregion
}
