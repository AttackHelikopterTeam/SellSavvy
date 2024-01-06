using System;
namespace SellSavvy.Application.Repositories.Product
{
	public interface IProductReadRepository : IReadRepository<Domain.Entities.Product, Guid>
    {
	}
}

