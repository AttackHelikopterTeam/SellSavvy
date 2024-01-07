using System;
using MediatR;
using Microsoft.Extensions.Logging;
using SellSavvy.Application.Repositories.Product;

namespace SellSavvy.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductWriteRepository _writeRepository;

        public UpdateProductCommandHandler(IProductWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _writeRepository.GetById(request.ProductId, tracking: true);

                if (existingProduct == null)
                {

                    return new UpdateProductCommandResponse { StatusCode = 404, Success = false, Message = "Güncellenecek ürün bulunamadı." };
                }

                existingProduct.Name = request.Name;
                existingProduct.Image = request.Image;
                existingProduct.Description = request.Description;
                existingProduct.Price = request.Price;
                existingProduct.ProductState = request.ProductState;
                existingProduct.SellerId = request.SellerId;

                existingProduct.ProductCategories = request.ProductCategories;
                existingProduct.ProductPersons = request.ProductPersons;

                _writeRepository.UpdateAsync(existingProduct);

                await _writeRepository.SaveChangesAsync();

                return new UpdateProductCommandResponse { StatusCode = 200, Success = true, Message = "Ürün başarıyla güncellendi." };
            }
            catch (Exception ex)
            {

                return new UpdateProductCommandResponse { StatusCode = 500, Success = false, Message = "Ürün güncellenirken bir hata oluştu." };
            }
        }
    }
}

