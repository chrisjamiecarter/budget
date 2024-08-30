namespace Budget.Application.Repositories;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; set; }
    ITransactionRepository Transactions { get; set; }
    Task<int> SaveAsync();
}