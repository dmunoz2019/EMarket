using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<List<ProductBrand>>> GetProducts()
        {
            var brands = await _repo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var brand  = await _repo.GetByIdAsync(id);
            return Ok(brand);
        }

    }


}
