using SellSavvy.Domain.Common;
using SellSavvy.Domain.Enum;

namespace SellSavvy.Domain.Entities;

public class Product : EntityBase<Guid>
{
    public string? Name { get; set; }
    public string? Image { get; set; }

    public string? Description { get; set; }
    public decimal? Price { get; set; }

    public ProductState ProductState { get; set; }

    public string? SellerId { get; set; }


    public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public List<ProductPerson> ProductPersons { get; set; } = new List<ProductPerson>();

}