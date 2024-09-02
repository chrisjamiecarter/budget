namespace Budget.Web.Models;

public class BudgetViewModel
{
    #region Properties

    public List<CategoryViewModel>? Categories { get; set; }

    public List<TransactionViewModel>? Transactions { get; set; }

    public CategoryViewModel? Category { get; set; }

    public TransactionViewModel? Transaction { get; set; }

    #endregion
}
