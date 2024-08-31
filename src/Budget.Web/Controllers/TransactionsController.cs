using Budget.Application.Services;
using Budget.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.Web.Controllers;

public class TransactionsController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;

    public TransactionsController(ICategoryService categoryService, ITransactionService transactionService)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
    }

    // GET: TransactionModels
    public async Task<IActionResult> Index()
    {
        var entities = await _transactionService.ReturnAsync();
        var transactions = entities.Select(x => new TransactionDto(x));
        return View(transactions);
    }

    // GET: TransactionModels/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _transactionService.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var transaction = new TransactionDto(entity);
        return View(transaction);
    }

    // GET: TransactionModels/Create
    public async Task<IActionResult> Create()
    {
        var categories = await GetCategoriesAsync();

        ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
        return View();
    }

    // POST: TransactionModels/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Date,Amount,CategoryId")] TransactionDto transaction)
    {
        if (ModelState.IsValid)
        {
            var category = await _categoryService.ReturnAsync(transaction.CategoryId);
            if (category is null)
            {
                return NotFound();
            }
            transaction.Id = Guid.NewGuid();
            transaction.Category = new CategoryDto(category);
            await _transactionService.CreateAsync(transaction.MapToDomain());
            return RedirectToAction(nameof(Index));
        }

        var categories = await GetCategoriesAsync();
        ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", transaction.CategoryId);
        return View(transaction);
    }

    // GET: TransactionModels/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _transactionService.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var categories = await GetCategoriesAsync();
        ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", entity.Category!.Id);
        return View(entity);
    }

    // POST: TransactionModels/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Date,Amount,CategoryId")] TransactionDto transaction)
    {
        if (id != transaction.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var category = await _categoryService.ReturnAsync(transaction.CategoryId);
            if (category is null)
            {
                return NotFound();
            }
            transaction.Category = new CategoryDto(category);
            await _transactionService.UpdateAsync(transaction.MapToDomain());
            return RedirectToAction(nameof(Index));
        }

        var categories = await GetCategoriesAsync();
        ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", transaction.CategoryId);
        return View(transaction);
    }

    // GET: TransactionModels/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var entity = await _transactionService.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var transaction = new TransactionDto(entity);
        return View(transaction);
    }

    // POST: TransactionModels/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _transactionService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var entities = await _categoryService.ReturnAsync();
        return entities.Select(x => new CategoryDto(x));
    }
}
