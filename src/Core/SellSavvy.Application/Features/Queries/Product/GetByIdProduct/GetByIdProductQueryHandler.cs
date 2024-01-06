using System;
using MediatR;
using SellSavvy.Application.Repositories.Product;

namespace SellSavvy.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _readRepository;

        public GetByIdProductQueryHandler(IProductReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out Guid productId))
            {
                return new GetByIdProductQueryResponse
                {
                    Name = null,
                    Description = null,
                    Price = null
                };
            }

            var product = await _readRepository.GetByIdAsync(productId, false);

            return new GetByIdProductQueryResponse
            {
                Name = product?.Name,
                Description = product?.Description,
                Price = product?.Price
            };
        }
    }
}
