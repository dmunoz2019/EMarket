using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductCategoryController : BaseApiController
    {
        private readonly IGenericRepository<ProductCategory> _repo;

        public ProductCategoryController(IGenericRepository<ProductCategory> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> GetCategories()
        {
            var categories = await _repo.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetCategory(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategory>> CreateCategory([FromBody] ProductCategory category)
        {
            await _repo.AddAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] ProductCategory categoryToUpdate)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound("Category not found.");

            // Map the updated fields (you can use AutoMapper or do it manually)
            category.Name = categoryToUpdate.Name;
            // ... other fields

            await _repo.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound("Category not found.");

            await _repo.DeleteAsync(category);
            return NoContent();
        }
    }
}
