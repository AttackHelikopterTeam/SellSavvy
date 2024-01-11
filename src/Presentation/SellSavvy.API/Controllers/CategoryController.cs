﻿using System;
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

namespace SellSavvy.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        
        private IValidator<CategoryPostModel> _validator;
        SellSavvyIdentityContext _context;
        public CategoryController(IValidator<CategoryPostModel> validator, SellSavvyIdentityContext context)
        {
            _validator = validator;
            _context = context;
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
        public async Task<IActionResult> Add([FromBody] CategoryPostModel model,CancellationToken token) 
        {

            var result = await _validator.ValidateAsync(model,token);



            if (!ModelState.IsValid)
            {
               
                return BadRequest(result.Errors);
            }
            Category category = new()
            {
                   Id = model.Id,
                Name = model.Name,
                //CreatedByUserId = which admin
                CreatedOn = DateTimeOffset.UtcNow,
                ModifiedByUserId = "84c432a0-e376-436d-8122-15a3106c363f",
                IsDeleted = false

            };

            await _context.Categories.AddAsync(category,token);
            _context.SaveChangesAsync(token);
            //LogToDatabase("added by id");
            return CreatedAtRoute("GetById", new { id = model.Id }, model);

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CategoryPostModel updatedCategory,CancellationToken token)
        {
            
            var result = await _validator.ValidateAsync(updatedCategory, token);
            if (!ModelState.IsValid)
            {
                return BadRequest(result.Errors);
            }

            Category existingBrand = _context.Categories.FirstOrDefault(s => s.Id == updatedCategory.Id);

            existingBrand.Name = updatedCategory.Name;
           
            _context.SaveChangesAsync(token);
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

