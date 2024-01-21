using System;
namespace SellSavvy.Application.Repositories.Product
{
    public interface IProductWriteRepository : IWriteRepository<Domain.Entities.Product, Guid>
    {
        Task<Domain.Entities.Product> GetById(Guid id, bool tracking);
    }
}

