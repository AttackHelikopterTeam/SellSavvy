using System;
namespace SellSavvy.Persistence.Repositories.ProductRepository
{
	public class ProductReadRepository
	{
        public async Task<Domain.Entities.Product> GetById(Guid id, bool tracking)
        {
            return new Domain.Entities.Product();
        }
    }
}

