using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SellSavvy.Domain.Entities;
using SellSavvy.Persistence.Contexts;
using System;
namespace SellSavvy.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly SellSavvyIdentityContext _context;

        public ProductController(SellSavvyIdentityContext context)
        {
            _context = context;
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

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return CreatedAtRoute("GetProductById", new { id = newProduct.Id }, newProduct);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product updatedProduct)
        {
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

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            Product deletingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (deletingProduct == null)
            {
                return NotFound();
            }

            _context.Products.Remove(deletingProduct);
            _context.SaveChanges();

            return NoContent();


        }
    }
}
