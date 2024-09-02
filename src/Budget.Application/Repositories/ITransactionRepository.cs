using System.Linq.Expressions;
using Budget.Domain.Entities;

namespace Budget.Application.Repositories;

public interface ITransactionRepository
{
    Task CreateAsync(TransactionEntity entity);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<TransactionEntity>> ReturnAsync(
        Expression<Func<TransactionEntity, bool>>? filter = null, 
        Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>>? orderBy = null, 
        string includeProperties = "");
    Task<TransactionEntity?> ReturnAsync(object id);
    Task UpdateAsync(TransactionEntity entity);
}
