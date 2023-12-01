using API.Dtos;
using API.Errors;
using AutoMapper;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductSpecificationParams productSpecificationParams)
        {
            var spec = new ProductCategoryBrand(productSpecificationParams);

            // Ensure asynchronous execution
            var products = await _repo.GetAllWithSpecAsync(spec); // Assuming you have an async version of this method

            var countSpec = new ProductForCountingSpecifications(productSpecificationParams);

            // Async execution for counting
            var totalItems = await _repo.CountAsync(countSpec);

            var totalPages = (int)Math.Ceiling((double)totalItems / productSpecificationParams.PageSize);

            // Mapping operation
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productSpecificationParams.PageIndex, productSpecificationParams.PageSize, totalItems, data, totalPages));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _repo.GetByIdWithSpec(new ProductCategoryBrand(id));
            if (product == null) return NotFound(new CodeErrorResponse(404, "El producto No existe"));
            return _mapper.Map<Product, ProductDTO>(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product.ProductBrandId == 0) product.ProductBrandId = 0;
            if (product.ProductCategoryId == 0) product.ProductCategoryId = 0;

            await _repo.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id ,
                name = product.Name,
                description = product.Description,
                price = product.Price,
                pictureUrl = product.PictureUrl,
                productBrandId = product.ProductBrandId,
                productCategoryId = product.ProductCategoryId
            
            }, product);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound(new CodeErrorResponse(404, "El producto No existe"));

            UpdateEntityWithNonNullValues(product, updatedProduct);

            await _repo.UpdateAsync(product);
            return NoContent();
        }

        private void UpdateEntityWithNonNullValues(Product original, Product updated)
        {
            if (updated.Name != null)
                original.Name = updated.Name;

            if (updated.Description != null)
                original.Description = updated.Description;

            if (updated.PictureUrl != null)
                original.PictureUrl = updated.PictureUrl;

            if (updated.Price != default(decimal))
                original.Price = updated.Price;

            if (updated.Stock != default(int))
                original.Stock = updated.Stock;

            if (updated.ProductBrandId != default(int))
                original.ProductBrandId = updated.ProductBrandId;

            if (updated.ProductCategoryId != default(int))
                original.ProductCategoryId = updated.ProductCategoryId;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound(new CodeErrorResponse(404, "El producto No existe"));

            await _repo.DeleteAsync(product);
            return NoContent();
        }
    }
}
