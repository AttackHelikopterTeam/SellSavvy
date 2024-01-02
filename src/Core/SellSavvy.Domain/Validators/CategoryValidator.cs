using System;
using FluentValidation;
using SellSavvy.Domain.Entities;

namespace SellSavvy.Domain.Validators
{
	public class CategoryValidator:AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(x => x.Name).NotNull().MaximumLength(255);
		}

	}
}

