using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SellSavvy.Application.Features.Queries.Product.GetAllProduct;
using MediatR;
using SellSavvy.Application.Repositories.Product;
using SellSavvy.Domain.Entities;

namespace SellSavvy.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _readRepository;

        public GetAllProductsQueryHandler(IProductReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = _readRepository
                .GetWhere(product => product.IsDeleted == false)
                .Select(product => new
                {
                    product.Id,
                    product.Name,
                    product.Image,
                    product.Description,
                    product.Price,
                    product.ProductState,
                    product.SellerId
                })
                .Skip(request.Page * request.Size)
                .Take(request.Size);

            return Task.FromResult(new GetAllProductQueryResponse
            {
                Products = products,
                TotalCount = products.Count()
            });
        }
    }

}
