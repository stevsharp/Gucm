using Common.Domain.Models;
using Common.Domain.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gucm.Data.EntityFramework
{
    public class SpecificationEvaluator<T> where T : EntityBase
    {
        /// <summary>
        /// How to use 
        /// AddInclude(b => b.Items); with specification
        /// </summary>
        /// <param name="inputQuery"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ExpressionSpecification<T> specification)
        {
            var query = inputQuery;
            // Add where condition first
            query = query.Where(specification.ToExpression());
            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));
            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));
            return query;
        }
    }
}
