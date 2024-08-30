using Budget.Application.Repositories;
using Budget.Domain.Entities;

namespace Budget.Application.Services;

public class CategoryService : ICategoryService
{
    #region Fields

    private readonly IUnitOfWork _unitOfWork;

    #endregion
    #region Constructors

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion
    #region Methods

    public async Task CreateAsync(CategoryEntity category)
    {
        await _unitOfWork.Categories.CreateAsync(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(CategoryEntity category)
    {
        await _unitOfWork.Categories.DeleteAsync(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<CategoryEntity>> ReturnAsync()
    {
        return await _unitOfWork.Categories.ReturnAsync();
    }

    public async Task<CategoryEntity?> ReturnAsync(Guid id)
    {
        return await _unitOfWork.Categories.ReturnAsync(id);
    }

    public async Task UpdateAsync(CategoryEntity category)
    {
        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.SaveAsync();
    }

    #endregion
}
