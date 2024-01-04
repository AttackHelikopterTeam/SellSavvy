using FluentValidation;
using SellSavvy.Domain.Common;

namespace SellSavvy.Domain.Entities;

public class Category:EntityBase<Guid>
{
    public string? Name { get; set; }

    public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();


}

