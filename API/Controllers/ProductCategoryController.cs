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
        public async Task<ActionResult<List<ProductCategory>>> GetProducts()
        {
            var categories = await _repo.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProduct(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            return Ok(category);
        }

    }


}
