using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductCategoryRepostitory
    {
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task<IReadOnlyList<ProductCategory>> GetProductCategorysAsync();
    }
}
