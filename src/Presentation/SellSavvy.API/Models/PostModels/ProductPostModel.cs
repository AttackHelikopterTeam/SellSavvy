using System;
using SellSavvy.Domain.Enum;

namespace SellSavvy.Application.Models.PostModels
{
	public class ProductPostModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductState ProductState { get; set; }
        public string SellerId { get; set; }
    }
}

