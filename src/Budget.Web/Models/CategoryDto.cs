﻿using System.ComponentModel.DataAnnotations;
using Budget.Domain.Entities;

namespace Budget.Web.Models;

public class CategoryDto
{
    #region Constructors

    public CategoryDto()
    {

    }

    public CategoryDto(CategoryEntity entity)
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

    public List<TransactionDto>? Transactions { get; set; }

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
