using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SpecificationEvaluator<T> where T : Base
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
            // modify the IQueryable using the specification's criteria expression
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Includes all expression
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }



    }
}
