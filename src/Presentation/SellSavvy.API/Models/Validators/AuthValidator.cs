using System;
using FluentValidation;
using SellSavvy.API.Models.PostViewModels;

namespace SellSavvy.API.Models.Validators
{
	public class AuthValidator:AbstractValidator<AuthPostModel>
	{
		public AuthValidator()
		{
			
		}
	}
}

