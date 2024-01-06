using System;
using MediatR;
using SellSavvy.Application.Products.Commands;
using SellSavvy.Domain.Entities;
using SellSavvy.Domain.Enum;

namespace SellSavvy.Application.Features.Commands.Product.AddProduct
{
    public class AddProductCommandRequest : IRequest<AddProductCommandResponse>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductState ProductState { get; set; }
        public string SellerId { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductPerson> ProductPersons { get; set; }
    }
}
