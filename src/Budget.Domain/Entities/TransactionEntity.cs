namespace Budget.Domain.Entities;

public class TransactionEntity
{
    #region Properties

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public CategoryEntity? Category { get; set; }

    #endregion
}
