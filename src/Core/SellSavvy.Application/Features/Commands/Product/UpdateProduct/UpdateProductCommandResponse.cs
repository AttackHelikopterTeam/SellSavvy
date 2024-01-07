using System;
namespace SellSavvy.Application.Features.Commands.Product.UpdateProduct
{
	public class UpdateProductCommandResponse
	{
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}

