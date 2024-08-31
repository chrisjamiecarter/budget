using System.Linq.Expressions;
using Budget.Application.Repositories;
using Budget.Domain.Entities;
using Budget.Infrastructure.Contexts;
using Budget.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget.Infrastructure.Repositories;

internal class TransactionRepository : ITransactionRepository
{
    #region Fields

    private static readonly char[] _separator = [','];
    private readonly BudgetDataContext _dataContext;

    #endregion
    #region Constructors

    public TransactionRepository(BudgetDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    #endregion
    #region Methods

    public async Task CreateAsync(TransactionEntity entity)
    {
        var model = new TransactionModel
        {
            Id = Guid.NewGuid(),
            Name = entity.Name ?? "",
            Date = entity.Date,
            Amount = entity.Amount,
            CategoryId = entity.Category!.Id,
        };
        await _dataContext.Transaction.AddAsync(model);
    }

    public async Task DeleteAsync(object id)
    {
        var model = await _dataContext.Transaction.FindAsync(id);
        if (model is not null)
        {
            _dataContext.Transaction.Remove(model);
        }
    }

    public async Task<IEnumerable<TransactionEntity>> ReturnAsync(
        Expression<Func<TransactionEntity, bool>>? filter = null, 
        Func<IQueryable<TransactionEntity>, IOrderedQueryable<TransactionEntity>>? orderBy = null, 
        string includeProperties = "")
    {
        IQueryable<TransactionModel> query = _dataContext.Transaction;

        // Map the filter expression from the Entity (Domain) to the Model (Infrastructure).
        if (filter is not null)
        {
            var parameter = Expression.Parameter(typeof(TransactionModel), "x");
            var body = Expression.Invoke(filter, Expression.Convert(parameter, typeof(TransactionEntity)));
            var lambda = Expression.Lambda<Func<TransactionModel, bool>>(body, parameter);

            query = query.Where(lambda);
        }

        foreach (var includeProperty in includeProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        var mappedQuery = query.Select(x => x.MapToDomain());

        return orderBy is null
            ? await mappedQuery.ToListAsync()
            : await orderBy(mappedQuery).ToListAsync();
    }

    public async Task<TransactionEntity?> ReturnAsync(object id)
    {
        var model = await _dataContext.Transaction.FindAsync(id);
        return model?.MapToDomain() ?? null;
    }

    public async Task UpdateAsync(TransactionEntity entity)
    {
        var model = await _dataContext.Transaction.FindAsync(entity.Id);
        if (model is not null)
        {
            model.Name = entity.Name ?? "";
            _dataContext.Transaction.Update(model);
        }        
    }

    #endregion
}
