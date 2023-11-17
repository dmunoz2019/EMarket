using API.Dtos;
using AutoMapper;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repo;

        private readonly IMapper _mapper;



        public ProductController(IGenericRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetAllWithSpec(new ProductCategoryBrand());
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {

            var product = await _repo.GetByIdWithSpec(new ProductCategoryBrand(id));
             return _mapper.Map<Product, ProductDTO>(product);
           
        }

    }


}
