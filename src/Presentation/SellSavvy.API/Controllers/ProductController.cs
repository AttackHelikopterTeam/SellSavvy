using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SellSavvy.Domain.Entities;
using SellSavvy.Persistence.Contexts;
using System;
using FluentValidation;
using SellSavvy.Application.Models.PostModels;
using SellSavvy.API.Services;

namespace SellSavvy.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private IValidator<ProductPostModel> _validator;

        private readonly SellSavvyIdentityContext _context;
        private readonly RequestCountService _requestCountService;

        public ProductController(SellSavvyIdentityContext context, IValidator<ProductPostModel> validator, RequestCountService requestCountService)
        {
            _context = context;
            _validator = validator;
            _requestCountService = requestCountService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {

            List<Product> products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(Guid id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _requestCountService.RequestCount++; //requestCount

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductPostModel newProduct)
        {

            var result =  _validator.Validate(newProduct);


            if (!ModelState.IsValid)
            {
                return BadRequest(result.Errors);
            }
            Product product = new Product()
            {

                Id = Guid.NewGuid(),
                    Name = newProduct.Name,
                    Image = newProduct.Image,
                    Description = newProduct.Description,
                    Price = newProduct.Price,
                    ProductState = newProduct.ProductState,
                    SellerId = newProduct.SellerId
                
        };
            _context.Products.Add(product);
            _context.SaveChanges();
            _requestCountService.RequestCount++; //requestCount


            return CreatedAtRoute("GetProductById", new { id = product.Id }, newProduct);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductPostModel updatedProduct)
        {
            
            var result =  _validator.Validate(updatedProduct);
         
            if (!ModelState.IsValid)
            {
                return BadRequest(result.Errors);
            }
           Product existingProduct = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Image = updatedProduct.Image;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.ProductState = updatedProduct.ProductState;
            existingProduct.SellerId = updatedProduct.SellerId;

            _context.SaveChanges();
            _requestCountService.RequestCount++; //requestCount


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            Product deletingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (deletingProduct == null)
            {
                return NotFound();
            }

            _context.Products.Remove(deletingProduct);
            _context.SaveChanges();
            _requestCountService.RequestCount++; //requestCount


            return NoContent();


        }
    }
}
