using Budget.Application.Repositories;
using Budget.Domain.Entities;

namespace Budget.Application.Services;

public class TransactionService : ITransactionService
{
    #region Fields

    private readonly IUnitOfWork _unitOfWork;

    #endregion
    #region Constructors

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion
    #region Methods

    public async Task CreateAsync(TransactionEntity transaction)
    {
        await _unitOfWork.Transactions.CreateAsync(transaction);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(TransactionEntity transaction)
    {
        await _unitOfWork.Transactions.DeleteAsync(transaction);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<TransactionEntity>> ReturnAsync()
    {
        return await _unitOfWork.Transactions.ReturnAsync();
    }

    public async Task<TransactionEntity?> ReturnAsync(Guid id)
    {
        return await _unitOfWork.Transactions.ReturnAsync(id);
    }

    public async Task UpdateAsync(TransactionEntity transaction)
    {
        await _unitOfWork.Transactions.UpdateAsync(transaction);
        await _unitOfWork.SaveAsync();
    }

    #endregion
}
