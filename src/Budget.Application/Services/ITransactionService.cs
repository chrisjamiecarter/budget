using Budget.Domain.Entities;

namespace Budget.Application.Services;
public interface ITransactionService
{
    Task CreateAsync(TransactionEntity transaction);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<TransactionEntity>> ReturnAsync();
    Task<TransactionEntity?> ReturnAsync(Guid id);
    Task UpdateAsync(TransactionEntity transaction);
}