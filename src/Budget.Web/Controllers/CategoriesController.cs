using Budget.Application.Services;
using Budget.Domain.Entities;
using Budget.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Web.Controllers;

public class CategoriesController : Controller
{
    #region Fields

    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;

    #endregion
    #region Constructors

    public CategoriesController(ICategoryService categoryService, ITransactionService transactionService)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
    }

    #endregion
    #region Methods

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        var categoryEntities = await _categoryService.ReturnAsync(orderBy: o => o.OrderBy(k => k.Name));
        var transactionEntites = await _transactionService.ReturnAsync(orderBy: o => o.OrderBy(k => k.Date), includeProperties: "Category");

        var categories = categoryEntities.Select(x => new CategoryViewModel(x));
        var transactions = transactionEntites.Select(x => new TransactionViewModel(x));

        var viewModel = new BudgetViewModel
        {
            Categories = categories.ToList(),
            Transactions = transactions.ToList(),
            Category = new CategoryViewModel(),
            Transaction = new TransactionViewModel(categories)
        };

        return View(viewModel);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _categoryService.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var category = new CategoryViewModel(entity);
        return Ok(category);
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Category")] BudgetViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var category = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Category!.Name
            };

            await _categoryService.CreateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _categoryService.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var dto = new CategoryViewModel(entity);
        return View(dto);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CategoryViewModel dto)
    {
        if (id != dto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _categoryService.UpdateAsync(dto.MapToDomain());
            return RedirectToAction(nameof(Index));
        }

        return View(dto);
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _categoryService.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var dto = new CategoryViewModel(entity);
        return View(dto);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> IsDuplicateCategoryName([Bind(Prefix = "Category.Name")] string name)
    {
        var categories = await _categoryService.ReturnAsync();

        return categories.Any(c => c.Name!.Equals(name, StringComparison.CurrentCultureIgnoreCase)) 
            ? Json("A Categeory with that Name already exists.") 
            : Json(true);
    }
    #endregion
}
