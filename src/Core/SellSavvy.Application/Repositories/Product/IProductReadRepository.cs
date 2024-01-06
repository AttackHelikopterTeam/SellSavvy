using System;
using System.Threading.Tasks;

namespace SellSavvy.Application.Repositories.Product
{
    public interface IProductReadRepository : IReadRepository<Domain.Entities.Product, Guid>
    {
        Task<Domain.Entities.Product> GetByIdAsync(Guid id, bool tracking = true);
    }
}
