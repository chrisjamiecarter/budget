using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Budget.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Budget.Web.Models;

public class TransactionDto
{
    #region Constructors

    public TransactionDto()
    {

    }

    public TransactionDto(TransactionEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name ?? "";
        Date = entity.Date;
        Amount = entity.Amount;
        CategoryId = entity.Category is null ? new Guid() : entity.Category.Id;
        Category = entity.Category is null ? null : new CategoryDto(entity.Category);
    }

    #endregion
    #region Properties

    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [DataType(DataType.Date), Display(Name = "Watched"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [DataType(DataType.Currency), Required]
    public decimal Amount { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryDto? Category { get; set; }

    #endregion
    #region Methods

    public TransactionEntity MapToDomain()
    {
        return new TransactionEntity
        {
            Id = this.Id,
            Name = this.Name,
            Date = this.Date,
            Amount = this.Amount,
            Category = this.Category?.MapToDomain(),
        };
    }

    #endregion
}
