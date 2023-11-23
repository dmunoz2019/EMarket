using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductForCountingSpecifications : BaseSpecification<Product>
    {

        public ProductForCountingSpecifications(ProductSpecificationParams productParams)
            : base( x =>
                        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&

            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                                   (!productParams.BrandId.HasValue || x.ProductCategoryId == productParams.BrandId)
                       )
        {
           

        }

      
       
    }
}
