using System;
namespace SellSavvy.Application.Features.Queries.Product.GetByIdProduct
{
	public class GetByIdProductQueryResponse
	{
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}

