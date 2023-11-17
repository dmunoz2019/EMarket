using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
   
    public class ProductCategoryRepository : IProductCategoryRepostitory
    {
        private readonly MarketDbContext _context;
        public ProductCategoryRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductCategory>> GetProductCategorysAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }
        


    }
}
