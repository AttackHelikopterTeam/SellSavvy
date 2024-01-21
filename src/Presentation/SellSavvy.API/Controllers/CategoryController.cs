using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Entities;
using SellSavvy.Persistence.Contexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SellSavvy.API.Models.PostModels;

using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using SellSavvy.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using FluentValidation.AspNetCore;
using SellSavvy.API.Services;

namespace SellSavvy.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        
        private IValidator<CategoryPostModel> _validator;
        SellSavvyIdentityContext _context;
        private RequestCountService _requestCountService;
        public CategoryController(IValidator<CategoryPostModel> validator, SellSavvyIdentityContext context, RequestCountService requestCountService)
        {
            _validator = validator;
            _context = context;
            _requestCountService = requestCountService;
        }


        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryPostModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            
            List<CategoryPostModel> category = _context.Categories.Where(x => x.IsDeleted == false).Select(x => new CategoryPostModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            //LogToDatabase("called by id");
            _requestCountService.RequestCount++; //requestCount

            return Ok(category);

        }

        [HttpGet("id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest("not GUID");
            }
            


            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            _requestCountService.RequestCount++; //requestCount

            return Ok(category);


        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] string model) 
        {

            //var result = await _validator.ValidateAsync(model,token);



            if (!ModelState.IsValid)
            {
               
             //   return BadRequest(result.Errors);
            }
            Category category = new()
            {
                   Id = Guid.NewGuid(),
                Name = model,
                //CreatedByUserId = which admin
                CreatedOn = DateTimeOffset.UtcNow,
                ModifiedByUserId = "84c432a0-e376-436d-8122-15a3106c363f",
                IsDeleted = false

            };

             _context.Categories.Add(category);
            _context.SaveChanges();
            //LogToDatabase("added by id");
            Category existingBrand =  _context.Categories.FirstOrDefault(s => s.Id == category.Id);
            _requestCountService.RequestCount++; //requestCount

            return Ok(existingBrand);

        }

      


        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CategoryPostModel updatedCategory)
        {

            var result =  _validator.Validate(updatedCategory);
            if (!ModelState.IsValid)
            {
                return BadRequest(result.Errors);
            }

            Category existingBrand = _context.Categories.FirstOrDefault(s => s.Id == updatedCategory.Id);

            existingBrand.Name = updatedCategory.Name;

            _context.SaveChangesAsync();
            //LogToDatabase("updated by id");
            _requestCountService.RequestCount++; //requestCount

            return NoContent();

        }

        /*
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromBody] Guid id)
        {
            if (!_validation.validId(id))
            {
                _error.ErrorMessage.Add("There is no data with this id");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }
            Brand deletingBrand = _context.Brands.FirstOrDefault(s => s.Id == id);
            deletingBrand.IsDeleted = true;
            //_context.Brands.Remove(deletingBrand);
            _context.SaveChanges();
            //LogToDatabase("deleted by id");
            return NoContent();
        }
        [HttpDelete("DeleteForce")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteForce([FromBody] Guid id)
        {
            if (!_validation.validId(id))
            {
                _error.ErrorMessage.Add("There is no data with this id");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }
            Brand deletingBrand = _context.Brands.FirstOrDefault(s => s.Id == id);

            _context.Brands.Remove(deletingBrand);
            _context.SaveChanges();
            //LogToDatabase("deleted forcefully by id");
            return NoContent();
        }

        */
    }
}

