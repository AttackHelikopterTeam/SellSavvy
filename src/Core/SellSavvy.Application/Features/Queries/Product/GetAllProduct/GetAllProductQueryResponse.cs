using System.Collections.Generic;
using System.Threading.Tasks;
using SellSavvy.Application.Features.Queries.Product.GetAllProduct;

namespace SellSavvy.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public object Products { get; set; }
    }
}
