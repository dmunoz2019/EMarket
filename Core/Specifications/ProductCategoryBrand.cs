using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public  class ProductCategoryBrand : BaseSpecification<Product>
    {
        public ProductCategoryBrand() { 
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductCategory);

        }

        public ProductCategoryBrand(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductCategory);

        }   

    }
}
