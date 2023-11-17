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
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly MarketDbContext _context;

        public GenericRepository(MarketDbContext context)
        {
            _context = context;
        }



        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {

                return (IReadOnlyList<T>)await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductCategory)
                .ToListAsync();
           
            }
            else if (typeof(T) == typeof(ProductBrand))
            {
                   return (IReadOnlyList<T>)await _context.ProductBrands.ToListAsync();
            }
            else if (typeof(T) == typeof(ProductCategory))
            {
                return (IReadOnlyList<T>)await _context.ProductCategories.ToListAsync();

            }
            else
            {
                return null;
            }

        }

        public async Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);
        }
    }
}
