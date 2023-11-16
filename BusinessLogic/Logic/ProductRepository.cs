using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class ProductRepository : IProductRepostitory
    {
        Task<Product> IProductRepostitory.GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Product>> IProductRepostitory.GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
