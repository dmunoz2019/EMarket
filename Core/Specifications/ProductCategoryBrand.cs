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
        public ProductCategoryBrand(ProductSpecificationParams productParams)
            : base( x => (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                        (!productParams.BrandId.HasValue || x.ProductCategoryId == productParams.BrandId)
            ) 
        { 
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductCategory);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);


            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderBy(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

        }

        public ProductCategoryBrand(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductCategory);

        }   

    }
}
