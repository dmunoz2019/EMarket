using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _repo;

        public ProductBrandController(IGenericRepository<ProductBrand> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _repo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id)
        {
            var brand = await _repo.GetByIdAsync(id);
            if (brand == null) return NotFound("Brand not found.");
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<ProductBrand>> CreateBrand([FromBody] ProductBrand brand)
        {
            await _repo.AddAsync(brand);
            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrand(int id, [FromBody] ProductBrand brandToUpdate)
        {
            var brand = await _repo.GetByIdAsync(id);
            if (brand == null) return NotFound("Brand not found.");

            // Map the updated fields (you can use AutoMapper or do it manually)
            brand.Name = brandToUpdate.Name;
            // ... other fields

            await _repo.UpdateAsync(brand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _repo.GetByIdAsync(id);
            if (brand == null) return NotFound("Brand not found.");

            await _repo.DeleteAsync(brand);
            return NoContent();
        }
    }
}
