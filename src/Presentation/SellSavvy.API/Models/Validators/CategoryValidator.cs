using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using SellSavvy.API.Models.PostModels;
using SellSavvy.Domain.Entities;

namespace SellSavvy.API.Validators
{
	public class CategoryPostValidator:AbstractValidator<CategoryPostModel>
	{
		public CategoryPostValidator()
		{
            RuleFor(x => x.Name)
       .NotEmpty().WithMessage("Please write a name")
       .MaximumLength(255).WithMessage("Name cannot exceed 255 characters");

        }
    }
}

