using System;
using FluentValidation;
using SellSavvy.Domain.Common;

namespace SellSavvy.Domain.Validators
{
	public class EntityBaseValidator:AbstractValidator<EntityBase<Guid>>
	{
		public EntityBaseValidator()
		{

            //Entity Base
            RuleFor(c => c.Id).NotNull();
            RuleFor(c => c.CreatedByUserId).Null();
            RuleFor(c => c.CreatedOn).Null();
            RuleFor(c => c.ModifiedByUserId).NotNull();
            RuleFor(c => c.LastModifiedOn).Null();
            RuleFor(c => c.IsDeleted).Null();
            RuleFor(c => c.DeletedByUserId).Null();
            RuleFor(c => c.DeletedOn);
        }
	}
}

