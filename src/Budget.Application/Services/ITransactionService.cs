using System.Linq.Expressions;
using Budget.Domain.Entities;

namespace Budget.Application.Services;
public interface ITransactionService
{
    Task CreateAsync(TransactionEntity transaction);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<TransactionEntity>> ReturnAsync(
        Expression<Func<TransactionEntity, bool>>? filter = null, 
        Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>>? orderBy = null,
        string includeProperties = "");
    Task<TransactionEntity?> ReturnAsync(Guid id);
    Task UpdateAsync(TransactionEntity transaction);
}