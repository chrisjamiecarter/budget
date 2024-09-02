using Budget.Application.Services;
using Budget.Domain.Entities;
using Budget.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Web.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        var entities = await _service.ReturnAsync();

        var categories = entities.Select(x => new CategoryDto(x));

        var viewModel = new CategoryViewModel
        {
            Categories = categories.ToList(),
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

        var entity = await _service.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var dto = new CategoryDto(entity);
        return View(dto);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Category")] CategoryViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var category = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Category!.Name
            };

            await _service.CreateAsync(category);
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

        var entity = await _service.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var dto = new CategoryDto(entity);
        return View(dto);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CategoryDto dto)
    {
        if (id != dto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(dto.MapToDomain());
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

        var entity = await _service.ReturnAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        var dto = new CategoryDto(entity);
        return View(dto);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
