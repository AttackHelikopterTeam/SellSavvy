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


namespace SellSavvy.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        SellSavvyIdentityContext _context;
        public CategoryController(SellSavvyIdentityContext context)
        {
            _context = context;
        }


        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<List<Category>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            
            List<Category> category = _context.Categories.Where(x => x.IsDeleted == false).ToList();
           
            //LogToDatabase("called by id");
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
            
            return Ok(category);


        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] CategoryPostModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Category category = new()
            {
                Id = model.Id,
                Name = model.Name,
                //CreatedByUserId = which admin
                CreatedOn = DateTimeOffset.UtcNow,


            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            //LogToDatabase("added by id");
            return CreatedAtRoute("GetById", new { id = model.Id }, model);

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] CategoryPostModel updatedCategory)
        {
           
            Category existingBrand = _context.Categories.FirstOrDefault(s => s.Id == updatedCategory.Id);

            existingBrand.Name = updatedCategory.Name;
           
            _context.SaveChanges();
            //LogToDatabase("updated by id");
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

