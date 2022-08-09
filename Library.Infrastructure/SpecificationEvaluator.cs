using Library.Core.Entities;
using Library.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec)
    {
        var query = inputQuery;
            
        if (spec.Includes != null)
        {
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
       
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.Take > 0)
        {
            query = query.Take(spec.Take);
        }

        return query;
    }
}