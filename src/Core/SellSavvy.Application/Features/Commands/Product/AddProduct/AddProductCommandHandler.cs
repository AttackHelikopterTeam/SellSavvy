using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SellSavvy.Application.Features.Commands.Product.AddProduct;
using SellSavvy.Application.Products.Commands;
using SellSavvy.Application.Repositories.Product;
using SellSavvy.Domain.Entities;

namespace SellSavvy.Application.Products.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
    {
        private readonly IProductWriteRepository _writeRepository;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(IProductWriteRepository writeRepository, ILogger<AddProductCommandHandler> logger)
        {
            _writeRepository = writeRepository;
            _logger = logger;
        }

        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // AddProductCommand'den gelen bilgileri kullanarak bir Product oluştur
                var newProduct = new Product
                {
                    Name = request.Name,
                    Image = request.Image,
                    Description = request.Description,
                    Price = request.Price,
                    ProductState = request.ProductState,
                    SellerId = request.SellerId,
                    // Diğer özellikleri de set edebilirsiniz.
                };

                // ProductCategories ve ProductPersons listelerini ekleyebilirsiniz.
                newProduct.ProductCategories = request.ProductCategories;
                newProduct.ProductPersons = request.ProductPersons;

                // Oluşturulan ürünü veritabanına ekleyin
                await _writeRepository.AddAsync(newProduct);

                // Veritabanındaki değişiklikleri kaydedin
                await _writeRepository.SaveChangesAsync();

                _logger.LogInformation("Ürün başarıyla eklendi.");

                // Başarı durumunu ve mesajı içeren bir AddProductCommandResponse nesnesini döndürün
                return new AddProductCommandResponse { Success = true, Message = "Ürün başarıyla eklendi." };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ürün eklenirken bir hata oluştu: {ex.Message}");

                // Hata durumunu ve mesajı içeren bir AddProductCommandResponse nesnesini döndürün
                return new AddProductCommandResponse { Success = false, Message = "Ürün eklenirken bir hata oluştu." };
            }
        }
    }
}