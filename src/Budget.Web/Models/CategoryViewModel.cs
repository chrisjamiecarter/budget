using System.ComponentModel.DataAnnotations;
using Budget.Domain.Entities;

namespace Budget.Web.Models;

public class CategoryViewModel
{
    #region Constructors

    public CategoryViewModel()
    {

    }

    public CategoryViewModel(CategoryEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name ?? "";
        Transactions = [];
    }

    #endregion
    #region Properties

    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public List<TransactionViewModel>? Transactions { get; set; }

    #endregion
    #region Methods

    public CategoryEntity MapToDomain()
    {
        return new CategoryEntity
        {
            Id = this.Id,
            Name = this.Name,
        };
    }

    #endregion

}
