using MediatR;
using SellSavvy.Application.Features.Queries.Product.GetAllProduct;

namespace SellSavvy.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;

    }
}
