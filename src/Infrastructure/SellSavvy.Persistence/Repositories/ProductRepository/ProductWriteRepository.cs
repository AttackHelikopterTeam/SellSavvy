using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Application.Repositories.Product;
using SellSavvy.Domain.Entities;
using SellSavvy.Persistence.Contexts;

namespace SellSavvy.Persistence.Repositories.ProductRepository
{
	public class ProductWriteRepository : WriteRepository<Domain.Entities.Product, Guid>, IProductWriteRepository
    {
        public ProductWriteRepository(SellSavvyIdentityContext context) : base(context)
        {
        }

        public async Task<Product> GetById(Guid id, bool tracking)
        {
            var entity = await Table.FindAsync(id);
            if (entity != null && !tracking)
                Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}

