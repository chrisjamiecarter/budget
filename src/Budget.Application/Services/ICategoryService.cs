using Budget.Domain.Entities;

namespace Budget.Application.Services;
public interface ICategoryService
{
    Task CreateAsync(CategoryEntity category);
    Task DeleteAsync(CategoryEntity category);
    Task<IEnumerable<CategoryEntity>> ReturnAsync();
    Task<CategoryEntity?> ReturnAsync(Guid id);
    Task UpdateAsync(CategoryEntity category);
}