using System;
using FluentValidation;
using SellSavvy.Application.Models.PostModels;

namespace SellSavvy.API.Models.Validators
{
	public class ProductPostValidator:AbstractValidator<ProductPostModel>
	{
		public ProductPostValidator()
		{


            RuleFor(p => p.Name)
                .Null()
                .MaximumLength(255);

            RuleFor(p => p.Image)
                .Null()
                .MaximumLength(255);

            RuleFor(p => p.Description)
                 .Null()
                .MaximumLength(1000);

            RuleFor(p => p.Price)
                .Null();

            RuleFor(p => p.ProductState)
                .NotNull();


            RuleFor(p => p.SellerId)
                .Null()
                .MaximumLength(255);
        }
	}
}

