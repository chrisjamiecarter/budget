﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Budget.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Budget.Web.Models;

public class TransactionViewModel
{
    #region Constructors

    public TransactionViewModel()
    {

    }

    public TransactionViewModel(IEnumerable<CategoryViewModel> categories)
    {
        Categories = categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
    }

    public TransactionViewModel(TransactionEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name ?? "";
        Date = entity.Date;
        Amount = entity.Amount;
        CategoryId = entity.Category is null ? new Guid() : entity.Category.Id;
        Category = entity.Category is null ? null : new CategoryViewModel(entity.Category);
    }

    public TransactionViewModel(TransactionEntity entity, IEnumerable<CategoryViewModel> categories)
    {
        Id = entity.Id;
        Name = entity.Name ?? "";
        Date = entity.Date;
        Amount = entity.Amount;
        CategoryId = entity.Category is null ? new Guid() : entity.Category.Id;
        Category = entity.Category is null ? null : new CategoryViewModel(entity.Category);
        
        Categories = categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

    }

    #endregion
    #region Properties

    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; } = DateTime.Now.Date;

    [DataType(DataType.Currency), Range(0, (double) decimal.MaxValue), Required]
    public decimal Amount { get; set; }

    [Display(Name = "Category")]
    public Guid CategoryId { get; set; }

    public CategoryViewModel? Category { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = [];

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
